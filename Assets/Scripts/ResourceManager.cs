using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Button addBtn;
    public GameObject sphere;
    public GameObject Resources;
    // Start is called before the first frame update
    void Start()
    {
        addBtn.onClick.AddListener(AddResource);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    GameObject re;
    public void AddResource()
    {
        re = GameObject.Instantiate(sphere, Resources.transform);
        re.transform.localPosition = new Vector3(5, 0, 0);

    }
    public TextMeshProUGUI proUGUI;
    public void SetTextValue(string text)
    {
        proUGUI.text = text;
    }



}
