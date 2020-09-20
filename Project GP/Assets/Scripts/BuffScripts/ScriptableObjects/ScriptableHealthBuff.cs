using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Buffs/HealthBuff")]
    public class ScriptableHealthBuff : ScriptableBuff
    {
        public int HealthIncrease;

        public override TimedBuff InitializeBuff(GameObject obj)
        {
            return new TimedHealthBuff(this, obj);
        }
    }
}
