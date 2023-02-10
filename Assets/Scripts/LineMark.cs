using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineMark : MonoBehaviour
{
    private int i;
    Vector3 RunStart;
    Vector3 RunNext;
    LineRenderer line;
    List<Vector3> vector3s =new List<Vector3>();    
    float initTimeSpan = 0.1f;      //记录点的时间间隔
    float timespan ;

    public bool isClicked;
    public bool isPlay;     //是否启动回放
    public float playSpeed=2f;  //回放速度
    int index = 1;      //

    //大球旋转后，由于记录的点位是物体的世界坐标，球的路径并不会随球的转动变化，
    //应该记录相对于中心点的坐标进行运动
    public Transform center;

    // Use this for initialization
    void Start()
    {
        line = this.GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
        center= FindObjectOfType<Player>().gameObject.transform;
        i = 0;
        RunStart = this.transform.position;
        timespan = initTimeSpan;
        
    }
    
    // Update is called once per frame  
    void Update()
    {
        if(IsClickSelf())
        {
            //借助LineRender绘制路径
            RunNext = this.transform.position;
            timespan -= Time.deltaTime;
            if (timespan < 0)
            {
                if (RunStart != RunNext)
                {
                    i++;
                    line.positionCount = i;
                    line.SetPosition(i - 1, this.transform.position);
                    vector3s.Add(this.transform.localPosition);
                }

                RunStart = RunNext;
                timespan = initTimeSpan;
            }
            isClicked= true;
        }
        

        if (Input.GetMouseButtonUp(0)&&isClicked)
        {
            FindObjectOfType<ResourceManager>().sPathes[this.gameObject].Clear();
            FindObjectOfType<ResourceManager>().sPathes[this.gameObject].AddRange(vector3s);
           

            Debug.Log("路点数量:"+ vector3s.Count);
            i = 0;
            line.positionCount = i;
            vector3s.Clear();
            isClicked= false;
        }

        
        if(isPlay&& FindObjectOfType<ResourceManager>().sPathes[this.gameObject].Count>1)
        {
           if (Vector3.Distance(this.transform.localPosition, FindObjectOfType<ResourceManager>().sPathes[this.gameObject][index]) > 0.1f)
           {
                //transform.Translate((FindObjectOfType<ResourceManager>().sPathes[this.gameObject][index] - transform.position).normalized * playSpeed * Time.deltaTime);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, FindObjectOfType<ResourceManager>().sPathes[this.gameObject][index], Time.deltaTime * playSpeed);

            }
            else
            {
                if (index < FindObjectOfType<ResourceManager>().sPathes[this.gameObject].Count - 1)
                {
                    index += 1;
                }
                else
                {
                    index = 1;
                    isPlay= false;
                }

            }

        }
        else
        {
            isPlay = false;
        }
    }


    bool IsClickSelf()
    {
        bool isClickSelf = false;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, 1 << 7))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    isClickSelf = true;
                }
            }
            else
            {
                isClickSelf = false;
            }
        }
        
        return isClickSelf;
    }



}