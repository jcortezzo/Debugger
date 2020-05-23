using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public bool melee;
    public float cd;
    public float cdTimer;
    public Vector2 direction;
    public Transform muzzle;
    public Projectile projectile;
    public float spread;
    public float swingSpeed;
    public float swingRadius;
    public bool attacking = false;
    public BoxCollider2D hitbox;
    public float knockback;
    public float hitstun;
    public float weight;
    protected CameraController cam;
    protected const float SHAKE_TIME = 0.05f;
    protected Animator anim;
    //LivingEntity alignment;
    public Alignment alignment;
    protected WeaponHolder weaponHolder;

    // Start is called before the first frame update
    public virtual void Start()
    {
        muzzle = this.transform.GetChild(0);
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
        cam = FindObjectOfType<CameraController>();
        anim = GetComponent<Animator>();
        weaponHolder = GetComponentInParent<WeaponHolder>(); // sketch
        //Debug.Log(weaponHolder);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (cdTimer > 0)
        {
            cdTimer -= Time.deltaTime;
        }
    }

    public abstract void Attack();

    public bool IsCD()
    {
        return cdTimer > 0;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attacking) return;
        //if (collision.gameObject.CompareTag("Enemy"))
        LivingEntity e = collision.gameObject.GetComponent<LivingEntity>();
        Breakable b = collision.gameObject.GetComponent<Breakable>();
        if (e != null &&
            (alignment == Alignment.NEUTRAL || e.alignment != alignment))
        {
            Damage(e);
        }
        else if (b != null)
        {
            b.TakeHit(damage, hitstun);
        }
    }

    public virtual void Damage(LivingEntity e)
    {
        e.health -= this.damage;
        e.hitstun = hitstun;
        e.GetComponent<Rigidbody2D>().AddForce(direction * knockback);
    }

    protected void ShakeCamera()
    {
        this.cam.Shake((transform.position - muzzle.position).normalized, weight, SHAKE_TIME);
    }
}