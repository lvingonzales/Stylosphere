using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLineManager : MonoBehaviour
{
    // Update is called once per frame

    void Start()
    {
        
    }
    void Update()
    {
        CheckButtonInputs();
    }

    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;
    int numClick = 0;
    int currColor = 1;
    private LineRenderer currLine;

    private void CheckButtonInputs()
    {
        OVRInput.Update();

        Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log ("A Button Pressed");
            GameObject line = new GameObject ();
            currLine = line.AddComponent<LineRenderer> ();

            //added
            //lineRenderer = currLine.GetComponent<LineRenderer>();
            /*currLine.startColor = Color.blue;
            currLine.endColor = Color.red;*/    
            //

            //currLine.GetComponent<LineRenderer>().material = mat1;

            switch (currColor)
            {
            case 1:
                currLine.GetComponent<LineRenderer>().material = mat1;
            break;
            case 2:
                currLine.GetComponent<LineRenderer>().material = mat2;
            break;
            case 3:
                currLine.GetComponent<LineRenderer>().material = mat3;
            break;
            case 4:
                currLine.GetComponent<LineRenderer>().material = mat4;
            break;
            case 5:
                currLine.GetComponent<LineRenderer>().material = mat5;
            break;
            case 6:
                currLine.GetComponent<LineRenderer>().material = mat6;
            break;
            }

            currLine.startWidth = 0.01f;
            currLine.endWidth = 0.01f;
            
            numClick = 0;
        }else if(OVRInput.Get(OVRInput.Button.One))
        {
            currLine.positionCount = (numClick + 1);
            Debug.Log ("A is being held");
            currLine.SetPosition (numClick, rightControllerPosition);
            numClick++;
        } else if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log ("B was pressed");
            currColor+=1;
            if (currColor > 6)
            {
                currColor = 1;
            }
        }

        /*OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log ("B was pressed");
            currColor+=1;
            if (currColor > 6)
            {
                currColor = 1;
            }


        }*/

    }
}
