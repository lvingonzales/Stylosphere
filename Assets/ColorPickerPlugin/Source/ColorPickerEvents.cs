using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Color picker events.
/// Assign the script to the object with Color Picker VR or PC (depending on the target platform)
/// </summary>
[AddComponentMenu("Matej Vanco/Color Picker/Color Picker Events")]
public class ColorPickerEvents : MonoBehaviour
{
    [Tooltip("Is Color Picker focused on VR or PC/Mobile?")] public bool VR = false;

    private ColorPicker cpPC;
    private ColorPickerVR cpVR;

    private void Awake()
    {
        if(VR)
        {
            if (!GetComponent<ColorPickerVR>())
                Debug.LogError("Color Picker Events: The events are set to VR and the script needs to be attached to object with ColorPickerVR!");
            cpVR = GetComponent<ColorPickerVR>();
        }
        else
        {
            if (!GetComponent<ColorPicker>())
                Debug.LogError("Color Picker Events: The events are set to NON-VR and the script needs to be attached to object with ColorPicker!");
            cpPC = GetComponent<ColorPicker>();
        }
    }

    #region Set-Colors
    /// <summary>
    /// Set color to UI image
    /// </summary>
    public void PUBLIC_SetColor(Image @Image)
    {
        if(VR) @Image.color = cpVR.ColorPickerResult;
        else @Image.color = cpPC.ColorPickerResult;
    }
    /// <summary>
    /// Set color to UI text
    /// </summary>
    public void PUBLIC_SetColor(Text @Text)
    {
        if (VR) @Text.color = cpVR.ColorPickerResult;
        else @Text.color = cpPC.ColorPickerResult;
    }
    /// <summary>
    /// Set color to Mesh Renderer material color
    /// </summary>
    public void PUBLIC_SetColor(MeshRenderer @MeshRenderer)
    {
        if (VR) @MeshRenderer.material.color = cpVR.ColorPickerResult;
        else @MeshRenderer.material.color = cpPC.ColorPickerResult;
    }
    /// <summary>
    /// Set color to Material
    /// </summary>
    public void PUBLIC_SetColor(Material @Material)
    {
        if (VR) Material.color = cpVR.ColorPickerResult;
        else Material.color = cpPC.ColorPickerResult;
    }
    /// <summary>
    /// Set color to TextMesh
    /// </summary>
    public void PUBLIC_SetColor(TextMesh @TextMesh)
    {
        if (VR) TextMesh.color = cpVR.ColorPickerResult;
        else TextMesh.color = cpPC.ColorPickerResult;
    }
    /// <summary>
    /// Set color to Renderer objects with typed Tag
    /// </summary>
    public void PUBLIC_SetColor_FindObjectsByTag(string Tag)
    {
        foreach (GameObject gm in GameObject.FindGameObjectsWithTag(Tag))
        {
            if (gm.GetComponent<Renderer>())
            {
                if (VR) gm.GetComponent<Renderer>().material.color = cpVR.ColorPickerResult;
                else gm.GetComponent<Renderer>().material.color = cpPC.ColorPickerResult;
            }
        }
    }
    private string targetVar;
    /// <summary>
    /// MONOBEHAVIOUR CONNECTOR: Set Color in internal variable
    /// </summary>
    public void PUBLIC_SetColor_Mono(string VariableName)
    {
        targetVar = VariableName;
    }
    /// <summary>
    /// MONOBEHAVIOUR CONNECTOR: Set Color in internal variable
    /// </summary>
    public void PUBLIC_SetColor_Mono(MonoBehaviour @MonoBehaviour)
    {
        if (MonoBehaviour.GetType().GetField(targetVar) != null && MonoBehaviour.GetType().GetField(targetVar).GetValue(MonoBehaviour).GetType() == typeof(Color))
            MonoBehaviour.GetType().GetField(targetVar).SetValue(MonoBehaviour, ((VR) ? cpVR.ColorPickerResult : cpPC.ColorPickerResult));
    }
    /// <summary>
    /// Set Color in internal Monobehaviour variable
    /// </summary>
    public void PUBLIC_SetColor_Mono(MonoBehaviour @MonoBehaviour, string Variable)
    {
        if (MonoBehaviour.GetType().GetField(Variable) != null && MonoBehaviour.GetType().GetField(Variable).GetValue(MonoBehaviour).GetType() == typeof(Color))
            MonoBehaviour.GetType().GetField(Variable).SetValue(MonoBehaviour, ((VR) ? cpVR.ColorPickerResult : cpPC.ColorPickerResult));
    }
    #endregion
}
