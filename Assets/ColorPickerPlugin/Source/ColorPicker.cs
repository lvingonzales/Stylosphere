using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Color picker for PC & Mobile.
/// Written by Matej Vanco in 2019, Updated in 2022
/// </summary>
[AddComponentMenu("Matej Vanco/Color Picker/Color Picker")]
[RequireComponent(typeof(Image))]
public class ColorPicker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    [Header("* Texture Source")]
    public Image targetPaletteImage; // Texture/palette source (color palette)
    [Header("* Image Pointer")]
    public Image pickingPointer; // Color picking pointer
    [Space]
    [Tooltip("Include alpha channel of the picked color?")]
    public bool includeAlphaChannel = true;
    [Tooltip("Hide the picking pointer graphics on drop? Also show pointer on drag?")]
    public bool hidePointerOnDrop = true;

    [Header("Picking Color Event")]
    public UnityEvent colorPickerPublicEvent; // Event if user picks a color

    /// <summary>
    /// Main picking result - get this variable for final picking result (readonly)
    /// </summary>
    public Color ColorPickerResult { private set; get; }

    // Privates

    private Texture2D paletteImgSrc; // Current palette image
    private bool cursorEnter = false; // Overlaping image with cursor?

    #region Pointer Events

    public void OnPointerEnter(PointerEventData e)
    {
        cursorEnter = true;
    }

    public void OnPointerExit(PointerEventData e)
    {
        cursorEnter = false;
    }

    public void OnPointerClick(PointerEventData e)
    {
        OnPickingColor();
    }

    public void OnDrag(PointerEventData e)
    {
        OnPickingColor();
        if (pickingPointer && hidePointerOnDrop)
            pickingPointer.enabled = true;
    }

    public void OnEndDrag(PointerEventData e)
    {
        if (pickingPointer && hidePointerOnDrop)
            pickingPointer.enabled = false;
    }

    private void OnPickingColor()
    {
        if (!cursorEnter)
            return;

        Vector2 mPos = Input.mousePosition;
        pickingPointer.transform.position = mPos;

        mPos = targetPaletteImage.rectTransform.InverseTransformPoint(mPos);

        mPos.x += targetPaletteImage.rectTransform.rect.width * targetPaletteImage.rectTransform.pivot.x;
        mPos.y += targetPaletteImage.rectTransform.rect.height * targetPaletteImage.rectTransform.pivot.y;

        mPos *= new Vector2(paletteImgSrc.width / targetPaletteImage.rectTransform.rect.width, paletteImgSrc.height / targetPaletteImage.rectTransform.rect.height);

        if ((mPos.x <= paletteImgSrc.width && mPos.x >= 0) && (mPos.y <= paletteImgSrc.height && mPos.y >= 0))
            ColorPickerResult = paletteImgSrc.GetPixel((int)mPos.x, (int)mPos.y);

        if (!includeAlphaChannel)
        {
            Color c = ColorPickerResult;
            c.a = 1.0f;
            ColorPickerResult = c;
        }

        colorPickerPublicEvent?.Invoke();
    }

    #endregion

    private void Awake()
    {
        if(targetPaletteImage == null)
        {
            Debug.LogError("Color Picker: Target Palette Image is missing!");
            return;
        }
        if(targetPaletteImage.sprite == null)
        {
            Debug.LogError("Color Picker: Target Palette Image does not contain any sprite!");
            return;
        }

        paletteImgSrc = targetPaletteImage.sprite.texture;
        if(paletteImgSrc.isReadable == false)
        {
            Debug.LogError("Color Picker: Target Palette Image sprite is not set to 'Readable'! Select the sprite source texture and enable the 'Read/Write Enabled' property.");
            return;
        }
    }

    /// <summary>
    /// Show/hide color picker panel by its activation
    /// </summary>
    public void PUBLIC_ShowHide()
    {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    /// <summary>
    /// Show/ hide color picker panel by boolean
    /// </summary>
    public void PUBLIC_ShowHide(bool Active)
    {
        gameObject.SetActive(Active);
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
