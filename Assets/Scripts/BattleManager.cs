using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    
    private CapsuleCollider defcol;


    float duration = 1f;
    float elapsedTime = 0;

    void Start()
    {
        defcol = GetComponent<CapsuleCollider>();
        defcol.center = Vector3.up;
        defcol.height = 2.0f;
        defcol.radius = 0.25f;
        defcol.isTrigger = true;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Weapon")
        {
            print("进入");
            int damage = -20;
            
            //damage = col.gameObject.GetComponent<DamageInf>().damage;
            am.TryDoDamege(damage);
        }

    }


    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Drug")
        {
            if(elapsedTime<duration)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                am.TryDoDamege(-5);
                elapsedTime = 0;
            }
        }
    }


}
