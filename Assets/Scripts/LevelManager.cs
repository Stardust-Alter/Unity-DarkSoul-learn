using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _Instance;

    public GameObject reload;


    void Awake()
    {
        _Instance = this;
    }

    public void Lose()
    {
        reload.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }


    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

   

}
