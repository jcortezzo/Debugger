using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    protected override void Start()
    {
        base.Start();
        GetDirectionalInput();
        health = GlobalValues.Instance.playerHealth;
        alignment = Alignment.FRIEND;
    }

    public override void Update()
    {
        //GetDirectionalInput();
        base.Update();
        if (Input.GetKeyDown(KeyCode.R) &&
            (weaponHolder.primary == null ||
            !weaponHolder.primary.attacking))
        {
            weaponHolder.SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.F) &&
            weaponHolder.primary != null &&
            !weaponHolder.primary.attacking)
        {
            weaponHolder.DropWeapon(true);
        }
    }

    public override void GetDirectionalInput()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = this.transform.position.z;
        if (weaponHolder.primary != null && !weaponHolder.primary.attacking)
        {
            direction = (mousePos - this.transform.position).normalized;
            weaponHolder.primary.direction = direction;
        }
        if (weaponHolder.primary == null)
        {
            direction = (mousePos - this.transform.position).normalized;
        }
    }

    public override void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 newVelocity = new Vector2(horizontal, vertical);
        newVelocity.Normalize();

        rb.velocity = newVelocity * speed;
        float e = Mathf.Pow(10, -2);
        anim.SetFloat("speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    public override bool IsAttacking()
    {
        return (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) &&
                weaponHolder.primary != null && !weaponHolder.primary.attacking;
    }

    private void OnDestroy()
    {
        Debug.Log("player instance destroyed");
        Debug.Log("player health " + GlobalValues.Instance.playerHealth);
        health++;
        GlobalValues.Instance.playerHealth = health;
    }

    public bool HasWeaponEquipped()
    {
        return weaponHolder.primary != null;
    }
}