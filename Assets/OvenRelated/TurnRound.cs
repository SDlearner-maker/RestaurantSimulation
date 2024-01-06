using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRound : MonoBehaviour
{
    public GameObject obj_s;
    Number script;
    bool run;
    int sound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //obj_s=GameObject.Find("Canvas");
        script = obj_s.GetComponent<Number>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.Pause();
    }

    // Update is called once per frame
    void Update()
    {   
        run = script.turn;
        if(run){            
            transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime, Space.World);
            audioSource.UnPause();
        }else{
        audioSource.Pause();}
        //if(sound == 1 ){
        //    audioSource.Play
        //}
    }
}
