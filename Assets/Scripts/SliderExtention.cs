using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class ExtenEvent:UnityEvent<float> { }
public class SliderExtention : MonoBehaviour, IBeginDragHandler,IEndDragHandler,IPointerClickHandler
{
    private Slider slider;

    public ExtenEvent m_BeginDrag;
    public ExtenEvent m_EndDrag;
    public ExtenEvent m_PointClick;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_BeginDrag.Invoke(slider.value);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_EndDrag.Invoke(slider.value);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_PointClick.Invoke(slider.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        slider=GetComponent<Slider>();





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
