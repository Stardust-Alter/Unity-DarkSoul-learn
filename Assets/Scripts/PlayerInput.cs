using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("===== key setting =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA = "left shift";   //run按键
    public string keyB = "j";          //跳跃按键
    public string keyC = "k";          //攻击按键
    public string keyD = "l";          //防御按键

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;

    public MyBotton bottonRun = new MyBotton();
    public MyBotton bottonJump = new MyBotton();
    public MyBotton bottonAttack = new MyBotton();
    //public MyBotton bottonDefense = new MyBotton();
    public MyBotton bottonLockOn = new MyBotton();
    public MyBotton bottonRoll = new MyBotton();

    [Header("===== output signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;



    //按压触发信号
    public bool run;
    public bool defense;

    //一次触发信号（跳）
    public bool jump;

    //一次触发信号（攻击）
    public bool attack;

    public bool roll;
    public bool lockOn;

    [Header("===== Mouse Settings =====")]
    public float mouseSensitivityX = 1.0f;     //鼠标灵敏度X
    public float mouseSensitivityY = 1.0f;     //鼠标灵敏度Y

    [Header("===== others =====")]
    public bool inputEnabled = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;


    // Update is called once per frame
    void Update()
    {
        //Jup=(Input.GetKey(keyJUp)?1.0f:0)-(Input.GetKey(keyJDown)?1.0f:0);
        //Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);

        bottonRun.Tick(Input.GetButton("Run"));
        bottonJump.Tick(Input.GetButton("Jump"));
        bottonAttack.Tick(Input.GetButton("Attack"));
        //bottonDefense.Tick(Input.GetButton("Defense"));
        bottonLockOn.Tick(Input.GetButton("Lock"));
        bottonRoll.Tick(Input.GetButton("Roll"));



        Jup = Input.GetAxis("Mouse Y")* mouseSensitivityX * 3f;
        Jright = Input.GetAxis("Mouse X")* mouseSensitivityY * 2.5f;

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);             //1
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);       //键位输入转化成前后左右信号

        if (inputEnabled == false)   //设置控制开关
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);                           //1
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);              //线性插值获得Dup，Dright

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));                   //1
        float Dright2 = tempDAxis.x;                                                    //2
        float Dup2 = tempDAxis.y;                                                       //将Dup，Dright转化成正确的值                


        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;


        run = (bottonRun.IsPressing&&!bottonRun.IsDelaying)||bottonRun.IsExtending;
        jump = bottonJump.OnPressed;
        roll = bottonRoll.OnPressed;
        lockOn = bottonLockOn.OnPressed;
        //jump = bottonRun.OnPressed&&bottonRun.IsExtending;    黑魂设定，跑动结束后才能跳
        attack = bottonAttack.OnPressed;
        //defense = bottonDefense.IsPressing;
    }



    //正方形x,y值转换成圆形
    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }


}
