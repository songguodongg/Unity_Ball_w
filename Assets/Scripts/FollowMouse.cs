using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowMouse : MonoBehaviour
{
    public GameObject target;
    public float R = 5;
    // Start is called before the first frame update
    void Start()
    {
        target=FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit, 1000, 1 << 7))
            {
                //FindObjectOfType<RotateControl>().IsClickRes = true;
                //跟随移动
                if (hit.transform.gameObject == this.gameObject)
                {
                    //获取需要移动物体的世界转屏幕坐标
                    //Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
                    ////获取鼠标位置
                    //Vector3 mousePos = Input.mousePosition;
                    ////因为鼠标只有X，Y轴，所以要赋予给鼠标Z轴
                    //mousePos.z = screenPos.z;
                    ////把鼠标的屏幕坐标转换成世界坐标
                    //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                    //worldPos = (worldPos - target.transform.position).normalized * R + target.transform.position;
                    ////控制物体移动
                    //transform.position = worldPos;

                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                                        Input.mousePosition.x,
                                        Input.mousePosition.y,
                                        Camera.main.transform.InverseTransformPoint(transform.position).z));
                    //限制在球面
                    worldPos = (worldPos - target.transform.position).normalized * R + target.transform.position;
                    transform.position = worldPos;

                }






            }
            else
            {
               // FindObjectOfType<RotateControl>().IsClickRes = false;
            }
        }
    }
}
