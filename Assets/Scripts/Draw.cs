using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class Draw : MonoBehaviour
{
    /// <summary>
    /// 地球半径
    /// </summary>
    public float R = 1;
    /// <summary>
    /// 纬线圈数(不包括南极点和北极点)
    /// </summary>
    public int latNum = 9;
    /// <summary>
    /// 经线数
    /// </summary>
    public int lonNum = 18;
    /// <summary>
    /// 一条纬线圈分段
    /// </summary>
    public int latSegment = 36;
    /// <summary>
    /// 一条经线（半圆）分段
    /// </summary>
    public int lonSegment = 36;
    /// <summary>
    /// 网格颜色
    /// </summary>
    public Color color = Color.white;
    /// <summary>
    /// 显示度数
    /// </summary>
    private Object latlonText3D;
    private List<List<Vector3>> latLines = new List<List<Vector3>>();
    private List<List<Vector3>> lonLines = new List<List<Vector3>>();
    private List<List<Vector3>> posLines= new List<List<Vector3>>();
    /// <summary>
    /// 里面包含了shader
    /// </summary>
    static Material lineMaterial;

    public GameObject cube;
    void Start()
    {
        float latSpan = Mathf.PI / (latNum + 1);//纬线间隔度数
        float lonSpan = Mathf.PI * 2 / lonNum;//经线间隔度数
        float anglePerLatSeg = Mathf.PI * 2 / latSegment;//一条纬线每一段对应的度数
        float anglePerLonSeg = Mathf.PI / lonSegment;//一条经线每一段对应的度数
        latlonText3D = Resources.Load("LatLonText3D", typeof(GameObject));
        //// 纬度度数
        //for (int r = 0; r < latNum + 2; r++)
        //{
        //    GameObject obj = Instantiate(latlonText3D, this.transform) as GameObject;
        //    obj.transform.position = new Vector3(R * Mathf.Sin(latSpan * r), R * Mathf.Cos(latSpan * r), 0);
        //    obj.GetComponentInChildren<TextMeshProUGUI>().text = (int)(Mathf.Rad2Deg * (latSpan * r)) - 90 + "°";
        //    //obj.GetComponentInChildren<TextMeshProUGUI>().fontSize = 100;
        //    obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(6, 1);
        //    obj.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        //}
        //////经度度数
        //for (int c = 0; c < lonNum; c++)
        //{
        //    GameObject obj = Instantiate(latlonText3D, this.transform) as GameObject;
        //    obj.transform.position = new Vector3(R * Mathf.Cos(lonSpan * c), 0, R * Mathf.Sin(lonSpan * c));
        //    obj.GetComponentInChildren<TextMeshProUGUI>().text = (int)(Mathf.Rad2Deg * (lonSpan * c)) + "°";
        //    //obj.GetComponentInChildren<TextMeshProUGUI>().fontSize = 100;
        //    obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        //    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(6, 1);
        //    obj.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
        //}

        
        List<Vector3> verticleR= new List<Vector3>();   //R:x轴
        verticleR.Add(cube.transform.localPosition);
        verticleR.Add(cube.transform.localPosition + new Vector3(2, 0, 0));

        posLines.Add(verticleR);

        List<Vector3> verticlesB = new List<Vector3>();   //B：y轴
        verticlesB.Add(cube.transform.localPosition);
        verticlesB.Add(cube.transform.localPosition + new Vector3(0, 2, 0));

        posLines.Add(verticlesB);

        List<Vector3> verticlesG = new List<Vector3>();   //g:z轴
        verticlesG.Add(cube.transform.localPosition);
        verticlesG.Add(cube.transform.localPosition + new Vector3(0, 0, 2));
        posLines.Add(verticlesG);

        // 绘制纬线圈
        for (int r = 0; r < latNum; r++)
        {
            //顶点
            List<Vector3> vertices = new List<Vector3>();
            for (int n = 0; n < latSegment + 1; n++)
            {
                Vector3 v;
                v.x = R * Mathf.Sin(latSpan * (r + 1)) * Mathf.Cos(anglePerLatSeg * n);
                v.y = R * Mathf.Cos(latSpan * (r + 1));
                v.z = R * Mathf.Sin(latSpan * (r + 1)) * Mathf.Sin(anglePerLatSeg * n);
                vertices.Add(v);
            }
            latLines.Add(vertices);
        }

        // 绘制经线圈
        for (int c = 0; c < lonNum; c++)
        {
            //顶点
            List<Vector3> vertices = new List<Vector3>();
            for (int n = 0; n < lonSegment + 1; n++)
            {
                Vector3 v;
                v.x = R * Mathf.Sin(anglePerLonSeg * n) * Mathf.Cos(lonSpan * c);
                v.y = R * Mathf.Cos(anglePerLonSeg * n);
                v.z = R * Mathf.Sin(anglePerLonSeg * n) * Mathf.Sin(lonSpan * c);
                vertices.Add(v);
            }
            lonLines.Add(vertices);
        }
        this.AddComponent<SphereCollider>().radius = R;

    }

    public void OnRenderObject()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);
        // Draw lines
        foreach (List<Vector3> vertices in latLines)
        {
            GL.Begin(GL.LINE_STRIP);
            GL.Color(color);
            //GL.Color(new Color(0, 0.5f, 1, 0.5F));
            foreach (Vector3 ver in vertices)
            {
                GL.Vertex3(ver.x, ver.y, ver.z);
            }
            GL.End();
        }
        foreach (List<Vector3> vertices in lonLines)
        {
            GL.Begin(GL.LINE_STRIP);
            GL.Color(color);
            //GL.Color(new Color(0, 0.5f, 1, 1.0F));
            foreach (Vector3 ver in vertices)
            {
                GL.Vertex3(ver.x, ver.y, ver.z);
            }
            GL.End();
        }
        foreach(List<Vector3> vertices in posLines)
        {
            GL.Begin(GL.LINE_STRIP);
            GL.Color(Color.red);
            //GL.Color(new Color(0, 0.5f, 1, 1.0F));
            foreach (Vector3 ver in vertices)
            {
                GL.Vertex3(ver.x, ver.y, ver.z);
            }
            GL.End();
        }
        /*for (int i = 0; i < lineCount; ++i)
        {
            float a = i / (float)lineCount;
            float angle = a * Mathf.PI * 2;
            // Vertex colors change from red to green
            GL.Color(new Color(a, 1 - a, 0, 0.8F));
            // One vertex at transform position
            //GL.Vertex3(0, 0, 0);
            // Another vertex at edge of circle
            GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        }*/
        GL.PopMatrix();
    }


    void OnPostRender()
    {
        // Set your materials
        GL.PushMatrix();
        // yourMaterial.SetPass( );
        // Draw your stuff
        GL.PopMatrix();

    }

    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

}

