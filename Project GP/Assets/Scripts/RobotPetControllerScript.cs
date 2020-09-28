using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Pathfinding;

public class RobotPetControllerScript : MonoBehaviour
{   
    public enum buffs { speedBuff, healthBuff, shootBuff};

    public buffs buffSelector;
    public BuffableEntity playerBuffs;
    AIPath path;
    bool isFacingRight;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        path = GetComponent<AIPath>();
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), true);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (path.desiredVelocity.x > 0.01f && !isFacingRight)
        {
            flip();
        } else if (path.desiredVelocity.x < 0.01f && isFacingRight)
        {
            flip();
        }
        
        if (path.desiredVelocity.x != 0)
        {
            animator.SetBool("isMoving", true);

        } else
        {
            animator.SetBool("isMoving", false);

        }
    }
    private void flip()
    {

        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;


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

