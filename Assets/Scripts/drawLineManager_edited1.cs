using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLineManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        CheckButtonInputs();
    }

    int numClick = 0;
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
            lineRenderer = currLine.GetComponent<LineRenderer>();
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.red;    
            //

            currLine.startWidth = 0.1f;
            currLine.endWidth = 0.1f;
            
            numClick = 0;
        }else if(OVRInput.Get(OVRInput.Button.One))
        {
            currLine.positionCount = (numClick + 1);
            Debug.Log ("A is being held");
            currLine.SetPosition (numClick, rightControllerPosition);
            numClick++;
        }

    }
}
