using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorDisplay : MonoBehaviour
{
    public Material cDisplayMat;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer> ().material = cDisplayMat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
