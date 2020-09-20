using ScriptableObjects;
using UnityEngine;

public class TimedSpeedBuff : TimedBuff
{
    private readonly PlayerController player;

    public TimedSpeedBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        //Getting MovementComponent, replace with your own implementation
        player = obj.GetComponent<PlayerController>();
    }

    protected override void ApplyEffect()
    {
        //Add speed increase to MovementComponent
        ScriptableSpeedBuff speedBuff = (ScriptableSpeedBuff) Buff;
        player.moveSpeed += speedBuff.SpeedIncrease;
    }

    public override void End()
    {
        //Revert speed increase
        ScriptableSpeedBuff speedBuff = (ScriptableSpeedBuff) Buff;
        player.moveSpeed -= speedBuff.SpeedIncrease * EffectStacks;
        EffectStacks = 0;
    }
}
