using UnityEngine;

public class MicrophoneCapture : MonoBehaviour
{
    //Record to audio clip using microphone
    //Reference: https://docs.unity3d.com/ScriptReference/Microphone.html
    private byte[] speech;
    private bool recording;
    private AudioSource audioSource;

    public GameObject customer;
    void Start()
    {
        if (Microphone.devices.Length <= 0)
        {
            Debug.Log("Microphone not detected");
        }
        else
        {
            audioSource = this.GetComponent<AudioSource>();
        }
    }

    public void StartCapture()
    {
        if (!recording)
        {
            string b=customer.GetComponent<Speak>().TaskOnClick();
            //Debug.Log(b);
            if (b == "GreetNowButton")
            {
                audioSource.clip = Microphone.Start(null, true, 10, 24000);
            }
            else
            {
                audioSource.clip = Microphone.Start(null, true, 5, 24000);
            }
            recording = true;
            Debug.Log("You are speaking!"); //check if voice is recorded
        }
    }

    public void StopCapture()
    {
        if (recording)
        {
            Microphone.End(null);
            recording = false;
            speech = WavUtility.FromAudioClip(audioSource.clip); //Audio clip to byte[]
            if (speech != null)
                StartCoroutine(this.GetComponent<Dialogflow>().Request(speech)); //Dialogflow Request
        }

    }

    
}