using ScriptableObjects;
using UnityEngine;

public class TimedShootSpeedBuff : TimedBuff
{
    private readonly Weapon playerWeapon;

    public TimedShootSpeedBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        playerWeapon = obj.GetComponentInChildren<Weapon>();
    }

    protected override void ApplyEffect()
    {
        //Add health increase
        ScriptableShootSpeedBuff shootSpeed = (ScriptableShootSpeedBuff)Buff;
        playerWeapon.shootSpeed += shootSpeed.ShootSpeedIncrease;
    }

    public override void End()
    {
        //Revert health increase
        ScriptableShootSpeedBuff shootSpeed = (ScriptableShootSpeedBuff)Buff;
        playerWeapon.shootSpeed -= shootSpeed.ShootSpeedIncrease * EffectStacks;
        EffectStacks = 0;
    }
}
