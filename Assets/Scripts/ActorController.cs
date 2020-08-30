using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public CameraController cameraController;
    public float walkSpeed = 2.5f;          //移动速度
    public float runMultiplier = 2.0f;    //run时速度倍率
    public float jumpVelocity = 3.0f;       //跳跃速度（力）
    public float rollVelocity = 3.0f;       //滚动速度（力）
    public float jabMul = 3.0f;        //后跳速度（力）

    public bool canAttack = true;

    [Space(10)]
    [Header("===== Friction Settings =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;


    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private ThrowController tc;

    private CapsuleCollider col;
    //private float lerpTarget;
    private Vector3 deltaPos;

    //private bool leftIsShield = true;

    [SerializeField] //使成员在Inspector面板中可见
    private bool lockPlanar = false;
    private bool trackDirection = false;

    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        tc = model.GetComponent<ThrowController>();
    }

    void Start()
    {
        anim = model.GetComponent<Animator>();
    }

    void Update()
    {
        float targetRunMulti = (pi.run) ? 2.0f : 1.0f;         //行走速度

        if (pi.lockOn)            //锁定视角
        {
            print("F");
            cameraController.LockUnlock();
        }

        if(cameraController.lockState==false)          //锁定模式与正常模式下的移动
        {
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.5f));   //线性插值设置动画状态机forward值
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDVecz = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDVecz.z * targetRunMulti);
            anim.SetFloat("right", localDVecz.x * targetRunMulti);
        }


       /* if (CheckState("Ground") || CheckState("block"))
        {
            anim.SetBool("defense", pi.defense);
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
        }
        else
        {
            anim.SetBool("defense", false);
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        }*/



        if(pi.roll&&(CheckState("Ground")||CheckStateTag("attack")))
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }


        //新加的
        if(rigid.velocity.magnitude > 7f)
        {
            anim.SetTrigger("fast");
            canAttack = false;
        }


        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (pi.attack && (CheckState("Ground")||CheckStateTag("attack"))&&canAttack)
        {
            anim.SetTrigger("attack");
        }

        if(cameraController.lockState==false)
        {
            if (pi.Dmag > 0.1f)           //球形向量插值控制转向
            {
                Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
                model.transform.forward = targetForward;
            }
            if (lockPlanar == false)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);  //移动向量
            }
        }
        else
        {
            if(trackDirection==false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }
            if (lockPlanar == false)
            {
                planarVec = pi.Dvec * walkSpeed * targetRunMulti;
            }
        }
    }

    void FixedUpdate()
    {
        rigid.position += deltaPos;
        //rigid.position += planarVec * Time.fixedDeltaTime;         //移动
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }


    public bool CheckState(string stateName,string layerName="Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return result;
    }


    /// <summary>
    /// Message processing block
    /// </summary>




    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }


    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {

        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }

    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity")*jabMul;
        //TODO修改帧数
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
        //lockPlanar = true;
        //lerpTarget = 1.0f;
     
    }

    public void OnAimEnter()
    {
        pi.inputEnabled = false;
        //lockPlanar = true;
        //lerpTarget = 1.0f;

    }



    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        //currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.4f);
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }
    public void OnHitEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }





    public void OnDieEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }


    public void OnBlockEnter()
    {
        pi.inputEnabled = false;
    }


    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC"))
        {
            deltaPos += (deltaPos+(Vector3)_deltaPos)/2.0f;
         }
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
}
