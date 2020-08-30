using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public BattleManager bm;
    public ActorController ac;
    public StateManager sm;

    void Awake()
    {
        ac = GetComponent<ActorController>();

        GameObject sensor = transform.Find("Sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        //wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
    }


    private T Bind<T>(GameObject go) where T:IActorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if(tempInstance==null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryDoDamege(int damage)
    {
        if(sm.isImmortal)
        {
            //DO nothing
        }
        else if (sm.isDefense)
        {

        }
        else
        {
            if(sm.HP<=0)
            {

            }
            else
            {
                sm.AddHP(damage);
                SEManagement._Instance.PlaySE("hurt");
                if (sm.HP > 0)
                {
                    Hit();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    public void Blocked()
    {
        ac.IssueTrigger("block");
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;
        if(ac.cameraController.lockState==true)
        {
            ac.cameraController.LockUnlock();          
        }
        ac.cameraController.enabled = false;
        LevelManager._Instance.Lose();
    }
}
