using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderDisplay : MonoBehaviour
{
    public GameObject SizeLine;
    // Update is called once per frame
    void Update()
    {
        float width = GetComponent<Slider> ().value;
        SizeLine.GetComponent<LineRenderer> ().startWidth = width;
        SizeLine.GetComponent<LineRenderer> ().endWidth = width;
    }
}
