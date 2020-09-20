using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

public class RobotPetControllerScript : MonoBehaviour
{   
    public enum buffs { speedBuff, healthBuff, shootBuff};

    public buffs buffSelector;
    public BuffableEntity playerBuffs;

    // Start is called before the first frame update
    void Start()
    {

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void applyPassiveBuff()
    {
        if (buffSelector == buffs.speedBuff)
        {
            ScriptableSpeedBuff buff = (ScriptableSpeedBuff)ScriptableObject.CreateInstance(typeof(ScriptableSpeedBuff));
            buff.Duration = Mathf.Infinity;
            buff.SpeedIncrease = 3;
            playerBuffs.AddBuff(buff.InitializeBuff(playerBuffs.gameObject));
        } else if (buffSelector == buffs.healthBuff)
        {
            ScriptableHealthBuff buff = (ScriptableHealthBuff)ScriptableObject.CreateInstance(typeof(ScriptableHealthBuff));
            buff.Duration = Mathf.Infinity;
            buff.HealthIncrease = 10;
            playerBuffs.AddBuff(buff.InitializeBuff(playerBuffs.gameObject));
        } else if (buffSelector == buffs.shootBuff)
        {
            ScriptableShootSpeedBuff buff = (ScriptableShootSpeedBuff)ScriptableObject.CreateInstance(typeof(ScriptableShootSpeedBuff));
            buff.Duration = Mathf.Infinity;
            buff.ShootSpeedIncrease = -0.9f;
            playerBuffs.AddBuff(buff.InitializeBuff(playerBuffs.gameObject));
        }
    }
}

