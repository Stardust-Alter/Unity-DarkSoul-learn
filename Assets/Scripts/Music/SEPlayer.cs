using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    AudioSource audioSource;
    bool startPlay = false;

    public void init(string path)
    {
        audioSource.clip = ResourceInterface.LoadAudio(path);
        audioSource.Play();
        startPlay = true;
    }

    public void init(string path,float gap)
    {
        audioSource.clip = ResourceInterface.LoadAudio(path);
        audioSource.Play();
        startPlay = true;
        audioSource.volume = gap;
    }

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && startPlay)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
}

