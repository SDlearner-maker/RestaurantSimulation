using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public GameObject script;
    public Animator anim;
    public bool walk;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        walk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (walk)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Left")
        {
            transform.Rotate(0f, -90f, 0.0f);
        }

        if (other.gameObject.name == "Stop")
        {
            walk = false;
            anim.Play("Idle");
			StartCoroutine(StartCaptureAfterTime(0,4));
        }
		
		if (other.gameObject.name == "Order")
        {
            walk = false;
            anim.Play("Idle");
			StartCoroutine(StartCaptureAfterTime(0,4));
        }
    }

    void OnTriggerExit(Collider other)
    {
		
        if (other.gameObject.name == "Left")
        {
            Destroy(other.gameObject);
            StartCoroutine(StartCaptureAfterTime(0,4)); //Start capture after 0s and stop capture after 5s
        }
		
    }

    public IEnumerator StartCaptureAfterTime(float startTime, float Endtime)
    {
        yield return new WaitForSeconds(startTime);

        script.GetComponent<MicrophoneCapture>().StartCapture();
        StartCoroutine(StopCaptureAfterTime(Endtime));
    }

    public IEnumerator StopCaptureAfterTime(float Endtime)
    {
        yield return new WaitForSeconds(Endtime);

        script.GetComponent<MicrophoneCapture>().StopCapture();
    }

}
