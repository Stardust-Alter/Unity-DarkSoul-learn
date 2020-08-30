using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 80.0f;
    public float verticalSpeed = 60.0f;
    public float cameraDampValue=0.05f;            //相机平滑时间
    public Image lockPic;                          //锁定的UI图片

    public bool lockState;
    public bool isAI = false;


    private GameObject playerHandle;
    private GameObject cameraHandle;

    private float tempEulerX;
    private GameObject model;
    private GameObject mycamera;

    private Vector3 cameraDampVelocty;

    private LockTarget lockTarget;         //锁定的敌人

    // Start is called before the first frame update
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        ActorController ac = playerHandle.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;

        if(!isAI)
        {
            mycamera = Camera.main.gameObject;
            if (lockPic != null)
            {
                lockPic.enabled = false;
            }
            Cursor.lockState = CursorLockMode.Locked;               //鼠标隐藏且锁定
        }
        lockState = false;
    }

    void Update()
    {
        if (lockPic == null)
            return;
        if (lockTarget!=null)
        {
            if(!isAI)
            {
                lockPic.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            }




            if(Vector3.Distance(model.transform.position,lockTarget.obj.transform.position)>10.0f)
            {
                LockProcess(null, false, false, isAI);
            }
            else if (lockTarget.am!=null && lockTarget.am.sm.isDie)
            {
                LockProcess(null, false, false, isAI);
            }
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);           //限制旋转角度
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 temForward = lockTarget.obj.transform.position - model.transform.position;
            temForward.y = 0;
            playerHandle.transform.forward = temForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform.position+new Vector3(0,lockTarget.halfHeight*3/4,0));
        }
        // camera.transform.position =Vector.Lerp(transfo rm.position;


        if(!isAI)
        {
            mycamera.transform.position = Vector3.SmoothDamp(mycamera.transform.position, transform.position, ref cameraDampVelocty, cameraDampValue);
            //mycamera.transform.eulerAngles = transform.eulerAngles;
            mycamera.transform.LookAt(cameraHandle.transform);
        }
    }

    public void LockUnlock()
    {

            Vector3 modelOrigin1 = model.transform.position;
            Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
            Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f),model.transform.rotation,LayerMask.GetMask(isAI?"Player":"Enemy"));

            if(cols.Length==0)
            {
                LockProcess(null, false, false, isAI);
            }
            else
            {
                foreach (var col in cols)
                {
                    if(lockTarget!=null&&lockTarget.obj==col.gameObject)
                    {
                        LockProcess(null, false, false, isAI);
                        break;
                    }
                    LockProcess(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                    break;
                }
            }
    }

    private class LockTarget
    {
        public GameObject obj;      //锁定的目标
        public float halfHeight;    //锁定目标的半高
        public ActorManager am;


        public LockTarget(GameObject _obj,float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            am = obj.GetComponent<ActorManager>();
        }
    }

    private void LockProcess(LockTarget _lockTarget,bool _lockPicEnable,bool _lockState,bool _isAI)
    {
        if (lockPic == null)
            return;


        lockTarget = _lockTarget;
        if(!_isAI)
        {
            lockPic.enabled = _lockPicEnable;
        }
        lockState = _lockState;
    }
}
