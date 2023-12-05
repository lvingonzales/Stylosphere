using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderDisplay : MonoBehaviour
{
    private void Start() {
        GetComponent<Slider> ().value = 0.1f;
    }
    public GameObject SizeLine;
    // Update is called once per frame
    void Update()
    {
        float width = GetComponent<Slider> ().value;
        SizeLine.GetComponent<LineRenderer> ().startWidth = width;
        SizeLine.GetComponent<LineRenderer> ().endWidth = width;
    }
}
