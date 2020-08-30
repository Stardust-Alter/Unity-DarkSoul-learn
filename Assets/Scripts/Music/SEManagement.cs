using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManagement : MonoBehaviour
{
    public static SEManagement _Instance;


    const string audioDir = "Audio/";

    private void Start()
    {
        _Instance = this;



    }
    public void PlaySE(string sePath)
    {
        GameObject go = ResourceInterface.InstantiateGO("SEPlayer", this.transform);
        go.GetComponent<SEPlayer>().init(audioDir + sePath);

    }

    public void PlaySE(string sePath, float gap)
    {
        GameObject go = ResourceInterface.InstantiateGO("SEPlayer", this.transform);
        go.GetComponent<SEPlayer>().init(audioDir + sePath, gap);

    }
}
