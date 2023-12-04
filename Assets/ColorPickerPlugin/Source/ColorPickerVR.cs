using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Color picker for VR.
/// Assign the script to the target VR Controller. The ray's direction is set to local-forward.
/// </summary>
[AddComponentMenu("Matej Vanco/Color Picker/Color Picker VR")]
public class ColorPickerVR : MonoBehaviour
{
    [Space]
    public bool ColorPickerActivated = true;

    [Header("* Pointer Settings")]
    [Tooltip("Pointer object that will visualize the picked color")] public MeshRenderer Pointer; // Color pointer
    [Tooltip("Additional Y offset of the picked color pointer")] public float pointerYOffset = 0.15f;
    [Tooltip("Line between ray origin and ray hit")] public LineRenderer PointerLine; // Color pointer line [for visualization]
    [Space]
    public LayerMask allowedLayers = ~0; // Allowed raycast layers

    [Tooltip("Include alpha channel of the picked color")]
    public bool includeAlphaChannel = true;
    [Header("* Picking Input")]
    [Tooltip("Mostly used for external source - enable/disable the variable for picking the color")] public bool pickerInput = false; // Picker Input

    [Header("Picking Color Event")]
    public UnityEvent Event; // Event if user is picks a color

    private bool pointEntered = false; // Overlaping with specific point

    /// <summary>
    /// Main picking result - get this variable for final picking result (readonly)
    /// </summary>
    public Color ColorPickerResult { private set; get; }

    private void OnPickingColor(Renderer hitObjRender, Vector3 hitpointCoords)
    {
        if (hitObjRender.material.mainTexture == null)
            return;
        if (!hitObjRender.material.mainTexture.isReadable)
        {
            Debug.LogError("Color Picker VR: main texture on the raycasted object is not set to 'Readable'! Select the texture and enable the 'Read/Write Enabled' property.");
            return;
        }

        Texture2D tex = ((Texture2D)hitObjRender.material.mainTexture);
        var mPos = hitpointCoords;
        mPos.x *= tex.width;
        mPos.y *= tex.height;

        ColorPickerResult = tex.GetPixel((int)mPos.x, (int)mPos.y);

        if (!includeAlphaChannel)
        {
            Color c = ColorPickerResult;
            c.a = 1.0f;
            ColorPickerResult = c;
        }

        Pointer.material.color = ColorPickerResult;
        PointerLine.startColor = ColorPickerResult;
        PointerLine.endColor = ColorPickerResult;

        Event?.Invoke();
    }

    private void Update()
    {
        if (!ColorPickerActivated) return;

        Ray r = new Ray(transform.position, transform.forward);
        bool physics = Physics.Raycast(r, out RaycastHit h, Mathf.Infinity, allowedLayers);
        pointEntered = physics && h.collider && h.collider.GetComponent<MeshCollider>() && h.collider.GetComponent<Renderer>() && h.collider.GetComponent<Renderer>().material && h.collider.GetComponent<Renderer>().material.mainTexture != null;

        if (!pointEntered)
        {
            if (Pointer.enabled)
                Pointer.enabled = false;
            if (PointerLine.enabled)
                PointerLine.enabled = false;
            return;
        }

        if (!PointerLine.enabled)
            PointerLine.enabled = true;

        PointerLine.SetPosition(0, r.origin);
        PointerLine.SetPosition(1, h.point);

        if (!pickerInput)
        {
            if (Pointer.enabled)
                Pointer.enabled = false;
            return;
        }

        if (!Pointer.enabled)
            Pointer.enabled = true;
        Pointer.transform.position = h.point + (Vector3.up * pointerYOffset);

        OnPickingColor(h.collider.GetComponent<Renderer>(), h.textureCoord);
    }

    /// <summary>
    /// Call this method to enable/ disable picking input
    /// </summary>
    public void PUBLIC_VRInputRequest(bool InputDown)
    {
        pickerInput = InputDown;
    }

    /// <summary>
    /// Active/ Disable color picker system by its activation
    /// </summary>
    public void PUBLIC_ActiveDisable()
    {
        if (ColorPickerActivated)
        {
            if (Pointer.enabled)
                Pointer.enabled = false;
            if (PointerLine.enabled)
                PointerLine.enabled = false;
            ColorPickerActivated = false;
        }
        else
            ColorPickerActivated = true;
    }

    /// <summary>
    /// Active/ Disable color picker system by boolean
    /// </summary>
    public void PUBLIC_ActiveDisable(bool Active)
    {
        ColorPickerActivated = Active;
        if (ColorPickerActivated)
            return;
        if (Pointer.enabled)
            Pointer.enabled = false;
        if (PointerLine.enabled)
            PointerLine.enabled = false;
    }

    /// <summary>
    /// Get picked color
    /// </summary>
    /// <returns>returns final picked color</returns>
    public Color PUBLIC_GetColor()
    {
        return ColorPickerResult;
    }
}
