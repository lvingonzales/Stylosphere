using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

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
    public Transform rightController;
    [SerializeField] private InputActionReference rConPos;
    private Stack<GameObject> lineStack = new Stack<GameObject>();
    private List<GameObject> deletedList = new List<GameObject>();
    void Update()
    {   
        OVRInput.Update();
        CheckColors();
        CheckButtonInputs();
    }
    
    private void CheckButtonInputs()
    {
        //Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        Vector3 rightControllerPosition = rightController.transform.position;

        //Vector3 rightControllerPosition = rConPos.action.ReadValue<Vector3>();

        //Debug.Log(rightControllerPosition);

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            //Debug.Log (rightControllerPosition);
            GameObject line = new GameObject ();
            lineStack.Push(line);
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

            Debug.Log ("B Pressed");
            currColor+=1;
            Debug.Log (currColor);

            if (currColor > 6)
            {
                currColor = 1;
            }
            CheckColors();
        }

        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log ("X Pressed");
            if(lineStack.Count > 0)
            {
                Debug.Log ("Reached Delete function");
                GameObject removedObject = lineStack.Pop();

                Destroy(removedObject);

                //deletedList.Add(removedObject);

                //deletedList[deletedList.Count - 1] = null;
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
        //Debug.Log ("Check Size Reached");
        float width = SizeSlider.GetComponent<Slider> ().value;
        return width;
    }
}
