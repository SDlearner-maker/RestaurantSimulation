using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
public class ProgressScript : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField]
    private VideoPlayer vplayer;
    [SerializeField]
    private Text timestamp;

    public GameObject thumbnail;

    private Image currentlength;
    void Awake()
    {
        vplayer.frame = 0;
        currentlength = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HideThumbnail()
    {
        thumbnail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (vplayer.frameCount > 0) //if not empty video, then number of frames is greater than 0
        {
            currentlength.fillAmount = (float)vplayer.frame / (float)vplayer.frameCount;
            timestamp.text = ((vplayer.time) / 60).ToString("n2");
        }
    }

    //to add dragging controls
    public void OnDrag(PointerEventData drag)
    {
        ControlDrag(drag);
    }

    public void OnPointerDown(PointerEventData point)
    {
        ControlDrag(point);
    }
    public void ControlDrag(PointerEventData data)
    {
        Vector2 pointlocation; //pointer on the progress bar of canvas
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(currentlength.rectTransform, data.position, null, out pointlocation))
        {
            float percentage = (float)Mathf.InverseLerp(currentlength.rectTransform.rect.xMin, currentlength.rectTransform.rect.xMax, pointlocation.x);
            CalcPercentage(percentage);
        }
    }

    public void CalcPercentage(float percentage)
    {
        float currentframe = vplayer.frameCount * percentage;
        vplayer.frame = (long)currentframe;
    }
}


