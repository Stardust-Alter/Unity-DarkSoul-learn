using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : IActorManagerInterface
{
    public float HP = 100f;
    public float HPMax = 100f;

    public Image HpImage;

    public bool isWudi;

    [Header("1st order state flags")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;


    [Header("2nd order state flag")]
    public bool isAllowDefense;
    public bool isImmortal;

    void Start()
    {
        HP = HPMax;
    }

    void Update()
    {
        isGround=am.ac.CheckState("Ground");
        isJump = am.ac.CheckState("Jump");
        isFall = am.ac.CheckState("Falling");
        isRoll = am.ac.CheckState("Roll");
        isAttack = am.ac.CheckStateTag("attack");          //AttackL  R
        isJab = am.ac.CheckState("jab");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("block");
        isHit = am.ac.CheckState("hit");
        //isDefense = am.ac.CheckState("defense1h","defense");


        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defense1h", "defense");
        isImmortal = isRoll || isJab;


        if(Input.GetKeyDown("o"))
        {
            isWudi = !isWudi;
        }
    }


    public void AddHP(float value)
    {
        HP += value;
        HP=Mathf.Clamp(HP, 0, HPMax);

        if(isWudi)
        {
            HP -= value;
        }

        if (HpImage != null)
        {
            HpImage.fillAmount = HP / 100.0f;
        }
    }
}
