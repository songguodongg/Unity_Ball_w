using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Button addBtn;   //添加按钮
    public Button playBtn;  //路径回放按钮
    public Button fasterBtn;    //加速
    public Button slowBtn;      //减速

    private GameObject sphere;   //小球预制体
    public GameObject AllBalls; //所有小球的父物体

    public Dictionary<GameObject, List<Vector3>> sPathes;   //生成所有小球的单条路径（多次移动会覆盖）


    public SliderExtention sliderExtention;
    // Start is called before the first frame update
    void Start()
    {
        sphere = Resources.Load<GameObject>("Ball");

        addBtn.onClick.AddListener(AddResource);
        playBtn.onClick.AddListener(Play);
        fasterBtn.onClick.AddListener(Faster);
        slowBtn.onClick.AddListener(Slower);
        sPathes = new Dictionary<GameObject, List<Vector3>>();

        //测试Slide绑定事件
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
        SetTextValue(value.ToString());
    }
    public void ValuePrintPoint(float value)
    {
        Debug.Log("点击" + value);
    }

    public void AddResource()
    {
        GameObject re = GameObject.Instantiate(sphere, AllBalls.transform);
        re.transform.localPosition = new Vector3(5, 0, 0);
        sPathes.Add(re, new List<Vector3>());
    }

    /// <summary>
    /// 路径回放
    /// </summary>
    public void Play()
    {
        foreach (KeyValuePair<GameObject, List<Vector3>> kv in sPathes)
        {
            if (kv.Key.GetComponent<LineMark>().isPlay)     //如果正在回放
            {
                break;
            }
            else
            {
                
                if (kv.Value.Count > 1) //有两个点及以上
                {
                    kv.Key.transform.localPosition = kv.Value[0];   //回到原点

                    kv.Key.GetComponent<LineMark>().isPlay = true;
                }
            }

        }
    }
    /// <summary>
    /// 加速播放
    /// </summary>
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
    /// <summary>
    /// 减速
    /// </summary>
    public void Slower()
    {
        foreach (KeyValuePair<GameObject, List<Vector3>> kv in sPathes)
        {
            if (kv.Key.GetComponent<LineMark>().playSpeed > 0.5)
                kv.Key.GetComponent<LineMark>().playSpeed -= 0.5f;
        }

    }



    public TextMeshProUGUI proUGUI;
    public void SetTextValue(string text)
    {
        proUGUI.text = text;
    }

}
