using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.UI;

public class EnemyBattleManager : MonoBehaviour
{
    public int HPMax = 100;
    public int HP;

    private CapsuleCollider defcol;
    public Animator ani;

    private BehaviorTree bt;

    public Image HpImage;

    void Start()
    {
        defcol = GetComponent<CapsuleCollider>();
        HP = HPMax;
        bt = GetComponentInParent<BehaviorTree>();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Weapon"|| col.tag == "Axe")
        {
            print(col.gameObject.name);
            GetHurt(-5);
            SEManagement._Instance.PlaySE("hit");
        }
        
    }

    public void GetHurt(int value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);
        HpImage.fillAmount = HP / 100.0f;
        if (HP<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        ani.SetTrigger("die");
        bt.DisableBehavior();
    }
}
