using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvent : MonoBehaviour
{
    public GameObject target;
    public GameObject yujincir;              //预警圆
    public GameObject skill_3_hit;
    public Collider weaponCol1;
    public GameObject skill_2_hit;
    public GameObject skill_2_hit_yujin;

    public GameObject UIWin;

    public GameObject[] qius;
    public Vector3[] weizhi;
    void Start()
    {
        weizhi = new Vector3[10];
        for (int i=0;i<qius.Length;i++)
        {
            weizhi[i] = qius[i].transform.position;
        }
    }

    public void Skill_2_yujin()
    {
        skill_2_hit_yujin.transform.position = transform.parent.position;
        skill_2_hit_yujin.SetActive(true);
    }


    public void Skill_2()
    {
        SEManagement._Instance.PlaySE("BossSkill2");
        skill_2_hit_yujin.SetActive(false);
        skill_2_hit.SetActive(true);
        skill_2_hit.transform.position = transform.position;
    }

    public void Skill_2_finish()
    {
        skill_2_hit.SetActive(false);

    }


    public void Skill_3_yujin()
    {        
        yujincir.transform.position=target.transform.position;
        yujincir.SetActive(true);
    }

    public void Skill_3_hit()
    {
        SEManagement._Instance.PlaySE("BossSkill3");
        yujincir.SetActive(false);
        skill_3_hit.transform.position = yujincir.transform.position;
        skill_3_hit.SetActive(true);
    }

    public void Skill_3_finish()
    {
        skill_3_hit.SetActive(false);

    }

    public void Skill_4_hit()
    {
        foreach(var a in qius)
        {
            a.SetActive(true);
            Vector3 b = a.transform.position - transform.gameObject.transform.position;
            b.y = 0;
            a.GetComponent<Rigidbody>().AddForce(b * 80,ForceMode.Acceleration);
        }
    }

    public void Skill_4_hit_fin()
    {
        foreach (var a in qius)
        {
            a.SetActive(false);
            a.transform.position = a.transform.parent.transform.position;
        }
    }



    public void WeaponEnable()
    {
        weaponCol1.enabled = true;
        SEManagement._Instance.PlaySE("BossSkill1");
        print("武器启用");
    }

    public void WeaponDisable()
    {
        weaponCol1.enabled = false;
        print("武器禁止");
    }
    public void Die()
    {
        transform.parent.gameObject.SetActive(false);
        UIWin.SetActive(true);
    }


}
