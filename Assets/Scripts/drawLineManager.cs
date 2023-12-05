using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drawLineManager : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;

    public GameObject CUI;
    public Material currMat;

    public GameObject SizeSlider;

    int currColor = 1;
    int numClick = 0;
    private advancedLineRenderer currLine;
    // Update is called once per frame
    void Update()
    {   
        CheckButtonInputs();
        CheckColors();
    }
    
    private void CheckButtonInputs()
    {
        OVRInput.Update();

        Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        -**

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            //Debug.Log (rightControllerPosition);
            GameObject line = new GameObject ();
            line.AddComponent<MeshFilter> ();
            line.AddComponent<MeshRenderer> (); 
            currLine = line.AddComponent<advancedLineRenderer> ();

            currLine.SetWidth (CheckSize());
            currLine.lineMat = currMat;
            
            numClick = 0;
        }else if(OVRInput.Get(OVRInput.Button.One))
        {

            currLine.AddPoint(rightControllerPosition);
            numClick++;

        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {

            //Debug.Log ("B Pressed");
            currColor+=1;

            if (currColor > 6)
            {
                currColor = 1;
            }
        }

    }


    private void CheckColors() 
    {
        switch (currColor)
            {
                case 1:
                    currMat = mat1;
                break;
                case 2:
                    currMat = mat2;
                break;
                case 3:
                    currMat = mat3;                    
                break;
                case 4:
                    currMat = mat4;
                break;
                case 5:
                    currMat = mat5;
                break;
                case 6:
                   currMat = mat6;             
                break;
            }

        CUI.GetComponent<Image> ().material = currMat;
    }

    private float CheckSize()
    {
        Debug.Log ("Check Size Reached");
        float width = SizeSlider.GetComponent<Slider> ().value;
        return width;
    }
}
