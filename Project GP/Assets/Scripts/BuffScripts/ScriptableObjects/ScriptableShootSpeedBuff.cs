using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Buffs/ShootSpeed")]
    public class ScriptableShootSpeedBuff : ScriptableBuff
    {
        public float ShootSpeedIncrease;

        public override TimedBuff InitializeBuff(GameObject obj)
        {
            return new TimedShootSpeedBuff(this, obj);
        }
    }
}
