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
    private advancedLineRenderer currLine;
    private void CheckButtonInputs()
    {
        OVRInput.Update();

        Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log (rightControllerPosition);
            GameObject line = new GameObject ();
            line.AddComponent<MeshFilter> ();
            line.AddComponent<MeshRenderer> (); 
            currLine = line.AddComponent<advancedLineRenderer> ();

            currLine.SetWidth (.1f);
            
            numClick = 0;
        }else if(OVRInput.Get(OVRInput.Button.One))
        {
            //currLine.positionCount = (numClick + 1);
            //Debug.Log (rightControllerPosition);
            //currLine.SetPosition (numClick, rightControllerPosition);

            currLine.AddPoint(rightControllerPosition);
            numClick++;
        }

    }
}
