using ScriptableObjects;
using UnityEngine;

public class TimedHealthBuff : TimedBuff
{
    private readonly PlayerController player;

    public TimedHealthBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        player = obj.GetComponent<PlayerController>();
    }

    protected override void ApplyEffect()
    {
        //Add health increase
        ScriptableHealthBuff healthBuff = (ScriptableHealthBuff)Buff;
        Debug.Log(healthBuff.HealthIncrease);
        player.maxHealth += healthBuff.HealthIncrease;
    }

    public override void End()
    {
        //Revert health increase
        ScriptableHealthBuff speedBuff = (ScriptableHealthBuff)Buff;
        player.maxHealth -= speedBuff.HealthIncrease * EffectStacks;
        EffectStacks = 0;
    }
}
