using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
    public float rotationSpeed = 5;
    public float minX=-90;
    public float maxX=90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            if (checkIsCanRotate())
            {
                RotateBigBall();
            }

        }

        



    }

    /// <summary>
    /// 小球正在路径回放或正在移动时不能旋转大球
    /// </summary>
    /// <returns></returns>
    public bool checkIsCanRotate()
    {
        foreach (var item in FindObjectOfType<Manager>().sPathes.Keys)
        {
            LineMark lineMark = item.GetComponent<LineMark>();
            if (lineMark.isPlay|| lineMark.isSelect)
            {
                return false;
            }
        }
        return true;
    }

    private void RotateMethod1()
    {
        float OffsetX = Input.GetAxis("Mouse X");
        float OffsetY = Input.GetAxis("Mouse Y");
        //transform.Rotate(new Vector3(OffsetY, -OffsetX, 0), Space.World);//旋转物体

        //if (Mathf.Abs(OffsetY) > Mathf.Abs(OffsetX))//对比水平和竖直方向谁的位移量更大，来决定旋转哪个方向，避免多个方向位移。
        //     transform.Rotate(new Vector3(OffsetY, 0, 0) * speed, Space.World);
        // else
        //    transform.Rotate(new Vector3(0, -OffsetX, 0) * speed, Space.World);

        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x + OffsetY, this.transform.localEulerAngles.y - OffsetX, this.transform.localEulerAngles.z);
    }

    public float currentHeadRotation = 0f;
    void RotateMethod2()
    {
        
        //transform.position += input * speed * Time.deltaTime;
        //gameObject.GetComponent<CharacterController>().Move(input * speed * Time.deltaTime);
        

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Debug.Log(mouseInput.x + ":" + mouseInput.y); 
        //head.Rotate(Vector3.left, mouseInput.y * rotationSpeed);
        currentHeadRotation = Mathf.Clamp(currentHeadRotation + mouseInput.y * rotationSpeed, minX, maxX);
        this.transform.localRotation = Quaternion.identity;
        this.transform.Rotate(Vector3.left, currentHeadRotation);
        transform.Rotate(new Vector3(0, mouseInput.x, 0) * rotationSpeed, Space.World);

    }

    private float eulX;
    private float eulY;
    void RotateBigBall()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        eulX += mouseInput.y;
        eulY-= mouseInput.x;
        eulX = CheckAngle(eulX, -90, 90);
        Quaternion quaternion = Quaternion.Euler(eulX,eulY,0);
        this.transform.localRotation = quaternion;
    }

    public float CheckAngle(float value,int min,int max)
    {
        if (value < -360)
        {
            value += 360;
        }
        if (value > 360)
        {
            value -= 360;
        }
        return Mathf.Clamp(value, min, max);
    }
}
