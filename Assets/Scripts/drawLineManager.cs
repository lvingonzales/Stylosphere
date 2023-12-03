using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLineManager : MonoBehaviour
{
    void Update()
    {
        CheckButtonInputs();
    }
    int numClick = 0;
    private LineRenderer currLine;
    public Material lmat;
    private void CheckButtonInputs()
    {
        OVRInput.Update();

        Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log ("A Button Pressed");
            GameObject line = new GameObject ();
            currLine = line.AddComponent<LineRenderer> ();

            currLine.lmat = lmat;
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

        // Check for a key press (you can change this to any condition you like)
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            // Change the color to a new color (you can set any color you want)
            SetLineColor(Color.red);
            Debug.Log("Space");
        }

    }
}
