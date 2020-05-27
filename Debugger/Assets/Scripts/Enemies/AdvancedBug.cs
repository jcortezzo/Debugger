using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedBug : IntermediateBug
{
    [SerializeField] private const float ATTACK_TIME = 2f;
    [SerializeField] private float cooldownTimer;

    protected override void Start()
    {
        base.Start();
        cooldownTimer = 0;
    }

    public override void Update()
    {
        base.Update();
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        } else
        {
            cooldownTimer = ATTACK_TIME;
        }
    }

    public override bool IsAttacking()
    {
        if (base.IsAttacking()) cooldownTimer = ATTACK_TIME;
        return base.IsAttacking() && cooldownTimer > 0;
    }

    public override string ToString()
    {
        return "Advanced Bug";
    }
}
