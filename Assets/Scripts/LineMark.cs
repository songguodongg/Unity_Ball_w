using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineMark : MonoBehaviour
{
    //路线绘制
    private int i;
    Vector3 RunStart;   
    Vector3 RunNext;
    LineRenderer line;
    float initTimeSpan = 0.1f;      //记录点的时间间隔
    float timespan;

    //当前小球的路径点
    List<Vector3> vector3s =new List<Vector3>();    

    public bool isPlay;     //是否正在回放
    public float playSpeed=2f;  //回放速度


    int index = 1;      //路径点序号:0表示第一个点


    private GameObject target;      //目标
    public float R = 5;             //距目标的半径

    public bool isSelect = false;    //是否点中当前物体

    private Manager manager;    

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Listener");

        line = this.GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
       // center= FindObjectOfType<Player>().gameObject.transform;
        i = 0;
        RunStart = this.transform.position;
        timespan = initTimeSpan;


        manager = FindObjectOfType<Manager>();
    }
    
    // Update is called once per frame  
    void Update()
    {
        
        //鼠标选中：跟随+绘制路线
        if (IsClickSelf())
        {
            isSelect = true;
            //跟随鼠标移动
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                                        Input.mousePosition.x,
                                        Input.mousePosition.y,
                                        Camera.main.transform.InverseTransformPoint(transform.position).z));
            //限制在球面
            worldPos = (worldPos - target.transform.position).normalized * R + target.transform.position;
            transform.position = worldPos;


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

                    //大球旋转后，由于记录的点位是物体的世界坐标，球的路径并不会随球的转动变化，
                    //应该记录小球的相对位置进行移动
                    vector3s.Add(this.transform.localPosition);
                }

                RunStart = RunNext;
                timespan = initTimeSpan;
            }


            

        }
        if (Input.GetMouseButtonUp(0)& isSelect)
        {
            manager.sPathes[this.gameObject].Clear();
            manager.sPathes[this.gameObject].AddRange(vector3s);


            Debug.Log("路点数量:" + vector3s.Count);
            i = 0;
            line.positionCount = i;

            vector3s.Clear();
            isSelect = false;
        }
        //鼠标选中+抬起：整理点数据，清除路径，取消选中状态

        //路径回放
        if (isPlay)
        {
           if (Vector3.Distance(this.transform.localPosition, manager.sPathes[this.gameObject][index]) > 0.1f)
           {
                
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, manager.sPathes[this.gameObject][index], Time.deltaTime * playSpeed);

            }
            else  
            {
                if (index < manager.sPathes[this.gameObject].Count - 1) //更新下一路点
                {
                    index += 1;
                }
                else   //回到初始状态,路径回放取消：目前必须到达终点才会恢复
                {
                    index = 1;
                    isPlay= false;
                }

            }

        }
    }


    /// <summary>
    /// Update中需要一直监听
    /// </summary>
    /// <returns></returns>
    public bool IsClickSelf()
    {
        bool IsClickedSelf = false;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, 1 << 7))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    IsClickedSelf = true;
                }
            }
            else
            {
                IsClickedSelf = false;
            }
        }
        return IsClickedSelf;
    }


}