using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    // Reference to the Line Renderer component
    private LineRenderer lineRenderer;
    public GameObject ObjWithLineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Line Renderer component attached to this GameObject
        lineRenderer = ObjWithLineRenderer.GetComponent<LineRenderer>();

        // Check if Line Renderer is present
        if (lineRenderer == null)
        {
            Debug.LogError("Line Renderer component not found on this GameObject.");
            return;
        }
        else
        {
            Debug.Log("has line renderer");
        }

        // Set the initial color (you can set any color you want)
        //SetLineColor(Color.blue);
    }

    // Function to change the color of the Line Renderer
    public void SetLineColor(Color newColor)
    {
        // Set the color of the Line Renderer
        lineRenderer.startColor = newColor;
        lineRenderer.endColor = newColor;
    }

    public void SetLineColorOnClickTest (string color_string) //2 colors for now only. Can add more
    {
        //Example
        if (color_string == "Blue")
        {
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
        }
        else if (color_string == "Colorful")
        {
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.red;
        }
    }

    // Example of how to change the color during runtime
    void Update()
    {   
        OVRInput.Update();
        // Check for a key press (you can change this to any condition you like)
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            // Change the color to a new color (you can set any color you want)
            SetLineColor(Color.red);
            Debug.Log("Space");
        }
    }
}
