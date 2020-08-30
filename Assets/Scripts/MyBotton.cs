using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBotton
{
    public bool IsPressing = false;    //正在按压
    public bool OnPressed = false;    //刚刚被按住
    public bool OnReleased = false;    //刚刚被释放
    public bool IsExtending = false;    //正在被延续
    public bool IsDelaying = false;     //正在被长按

    public float extendingDuration = 0.2f;
    public float delayingDuration = 0.2f;

    private bool curState = false;
    private bool lastState = false;

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    public void Tick(bool input)
    {

       // StartTimer(extTimer, 1.0f);
        extTimer.Tick();
        delayTimer.Tick();

        curState = input;
        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;

        if (curState!=lastState)
        {
            if(curState==true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }

        lastState = curState;

        if(extTimer.state == MyTimer.STATE.RUN)
        {
            IsExtending = true;
        }
       
        if(delayTimer.state==MyTimer.STATE.RUN)
        {
            IsDelaying = true;
        }
    }


    private void StartTimer(MyTimer timer,float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
}
