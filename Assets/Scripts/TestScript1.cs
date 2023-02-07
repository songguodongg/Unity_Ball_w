using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
   
 

    // Use this for initialization
    private Vector3 desPos;
    private bool isMove; //是否移动
    private Vector3 currentPos;
    private RaycastHit hit;
    public GameObject sphere;

    void Start()
    {
        if (sphere == null)
        {
            Debug.Log("球为空");
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, 1 << 8))
            {
                desPos = hit.point;
                isMove = true;
                Debug.Log("开始移动");
            }
        }
        if (isMove)
        {
            //获取每帧移动时当前的点
            currentPos = Vector3.MoveTowards(transform.position, desPos, 0.1f);
            Debug.Log(currentPos);
            //每帧发射的射线
            Ray rayEveryFrame = new Ray(Camera.main.transform.localPosition, (currentPos - Camera.main.transform.localPosition).normalized);
            //发射射线
            if (Physics.Raycast(rayEveryFrame, out hit, 1000, 1 << 8))
            {
                //求当前点的法线
                Vector3 normal = (transform.position - sphere.transform.position).normalized;
                //次切线
                Vector3 binormal = Vector3.Cross(normal, desPos - sphere.transform.position).normalized;
                //切线
                Vector3 tangent = Vector3.Cross(binormal, normal).normalized;
                transform.parent.position = hit.point;
                transform.parent.up = normal;
                //计算父级物体正方向和切线的夹角
                float angle = Vector3.Angle(transform.parent.forward, tangent);
                //将子物体的方向矫正
                //要分在物体上方点击还是下方点击来判断子物体应该往那边偏移
                if (transform.position.x > sphere.transform.position.x)
                {
                    if (desPos.y > transform.position.y)
                    {
                        transform.localEulerAngles = new Vector3(0, transform.parent.localEulerAngles.y - angle, 0);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0, transform.parent.localEulerAngles.y + angle, 0);
                    }
                }
                else
                {
                    if (desPos.y > transform.position.y)
                    {
                        transform.localEulerAngles = new Vector3(0, transform.parent.localEulerAngles.y + angle, 0);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0, transform.parent.localEulerAngles.y - angle, 0);
                    }
                }

            }
            if (Vector3.Distance(transform.position, desPos) < 0.1f)
            {
                isMove = false;
            }
        }
    }

}
