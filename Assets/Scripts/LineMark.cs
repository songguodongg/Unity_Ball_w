using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMark : MonoBehaviour
{
    private int i;
    Vector3 RunStart;
    Vector3 RunNext;
    LineRenderer line;

    // Use this for initialization
    void Start()
    {
        line = this.GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
                                                  //		//line.SetColors(Color.blue, Color.red);//设置颜色  
                                                  //		//line.SetWidth(0.2f, 0.1f);//设置宽度  
        i = 0;
    }

    // Update is called once per frame  
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, 1 << 7))
            {
                RunNext = this.transform.position;

            if (RunStart != RunNext)
            {
                i++;
                line.positionCount = i;
                line.SetPosition(i - 1, this.transform.position);

            }

            RunStart = RunNext;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            i = 0;
            line.positionCount = i;
        }





        //		if (Input.GetMouseButtonDown(0))  
        //		{  
        //			clone = (GameObject)Instantiate(obs, obs.transform.position, transform.rotation);//克隆一个带有LineRender的物体   
        //			line = clone.GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
        //			line.SetColors(Color.blue, Color.red);//设置颜色  
        //			line.SetWidth(0.2f, 0.1f);//设置宽度  
        //			i = 0;  
        //			print ("GetMouseButtonDown");
        //		}  
        //		if (Input.GetMouseButton(0))  
        //		{  
        //			i++;  
        //			line.SetVertexCount(i);//设置顶点数  
        //			line.SetPosition(i - 1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15)));//设置顶点位置   
        //			print ("GetMouseButton");
        //
        //		}  




    }
}