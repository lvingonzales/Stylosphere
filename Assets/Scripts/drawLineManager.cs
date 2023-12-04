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
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;

    int currColor;
    int numClick = 0;
    private advancedLineRenderer currLine;
    private rContColor contColor;
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
            
            switch (currColor)
            {
                case 1:
                    currLine.lineMat = mat1;
                    contColor.contMat = mat1;
                break;
                case 2:
                    currLine.lineMat = mat2;
                    contColor.contMat = mat2;
                break;
                case 3:
                    currLine.lineMat = mat3;
                    contColor.contMat = mat3;
                break;
                case 4:
                    currLine.lineMat = mat4;
                    contColor.contMat = mat4;
                break;
                case 5:
                    currLine.lineMat = mat5;
                    contColor.contMat = mat5;
                break;
                case 6:
                    currLine.lineMat = mat6;
                    contColor.contMat = mat6;
                break;
                default:
                    currLine.lineMat = mat1;
                    contColor.contMat = mat1;
                break;
            }

            currLine.SetWidth (.1f);
            
            numClick = 0;
        }else if(OVRInput.Get(OVRInput.Button.One))
        {
            //currLine.positionCount = (numClick + 1);
            //Debug.Log (rightControllerPosition);
            //currLine.SetPosition (numClick, rightControllerPosition);

            currLine.AddPoint(rightControllerPosition);
            numClick++;
        } else if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            currColor+=1;

            if (currColor > 6)
            {
                currColor = 1;
            }
        }

    }
}
