using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Button addBtn;
    public Button playBtn;
    public Button fasterBtn;
    public Button slowBtn;

    public GameObject sphere;
    public GameObject Resources;

    public Dictionary<GameObject, List<Vector3>> sPathes;

    public SliderExtention sliderExtention;
    // Start is called before the first frame update
    void Start()
    {
        addBtn.onClick.AddListener(AddResource);
        playBtn.onClick.AddListener(Play);
        fasterBtn.onClick.AddListener(Faster);
        slowBtn.onClick.AddListener(Slower);    
        sPathes = new Dictionary<GameObject, List<Vector3>>();

        sliderExtention.m_BeginDrag.AddListener(ValuePrint);
        sliderExtention.m_EndDrag.AddListener(ValuePrintEnd);
        sliderExtention.m_PointClick.AddListener(ValuePrintPoint);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    public void ValuePrint(float value)
    {
        Debug.Log("开始" + value);
    }
    public void ValuePrintEnd(float value)
    {
        Debug.Log("结束" + value);
    }
    public void ValuePrintPoint(float value)
    {
        Debug.Log("点击" + value);
    }
    public void AddResource()
    {
        GameObject re = GameObject.Instantiate(sphere, Resources.transform);
        re.transform.localPosition = new Vector3(5, 0, 0);
        sPathes.Add(re, new List<Vector3>());
    }

    public void Play()
    {
        foreach (KeyValuePair<GameObject,List<Vector3>> kv in sPathes)
        {
            kv.Key.GetComponent<LineMark>().isPlay=true;
            if (kv.Value.Count > 0)
            {
                kv.Key.transform.position = kv.Value[0];

            }
        }
    }

    public void Faster()
    {
        foreach (KeyValuePair<GameObject, List<Vector3>> kv in sPathes)
        {
            if (kv.Key.GetComponent<LineMark>().playSpeed < 5)
            {
                kv.Key.GetComponent<LineMark>().playSpeed += 0.5f;

            }
        }

    }

    public void Slower()
    {
        foreach (KeyValuePair<GameObject, List<Vector3>> kv in sPathes)
        {
            if(kv.Key.GetComponent<LineMark>().playSpeed > 0.5)
                kv.Key.GetComponent<LineMark>().playSpeed -= 0.5f;
        }

    }



    public TextMeshProUGUI proUGUI;
    public void SetTextValue(string text)
    {
        proUGUI.text = text;
    }



}
