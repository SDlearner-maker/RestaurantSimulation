using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    float speed = 130f;
    public GameObject obj_s;
    Number script;
    bool open;
    bool close;
    bool one;
    public AudioClip closesound;
    AudioSource audioSource;

    private Vector3 position;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        //obj_s = GameObject.Find("Canvas");
        script = obj_s.GetComponent<Number>();
        audioSource = GetComponent<AudioSource>();
        one = true;
    }

    public void ActualStatus(Vector3 p, Quaternion r)
    {
        position = p;
        rotation = r;
    }
    public void CloseDoor(bool doorclose)
    {
        open = script.d_open;
        //close = script.d_close;
        close = doorclose;
        int Kakudo = (int)transform.eulerAngles.y;
        if (close == false && Kakudo >= 0)
        {
            //float step = speed * Time.deltaTime;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0f, 0), step);
            transform.position = position;
            transform.rotation = rotation;
            script.d_open = false;
            script.d_close = true;
            audioSource.PlayOneShot(closesound);
        }
    }

    void Update()
    {
        /*
        open = script.d_open;
        close = script.d_close;
        int Kakudo = (int)transform.eulerAngles.y;
        if(close == false && Kakudo >= 0){
            float step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0f, 0), step);
            if(Kakudo==0){
                script.d_open = false;
                script.d_close = true;
            }
            if(Kakudo==0 && one== true){
                audioSource.PlayOneShot(closesound);
                one=false;
            }*/

    }
}
