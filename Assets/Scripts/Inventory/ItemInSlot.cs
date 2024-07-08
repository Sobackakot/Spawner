
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class ItemInSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Item currentItem;

    private Image _imageItem;
    private TextMeshProUGUI _nameItem;

    private Transform originTransform;
    private RectTransform rectTransform;
    private Canvas cancas;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        originTransform = GetComponent<Transform>();
        rectTransform = GetComponent<RectTransform>();

        cancas = rectTransform.GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        _imageItem =GetComponent<Image>();
        _nameItem = rectTransform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;  
        originTransform = transform.parent; 
        rectTransform.SetParent(cancas.transform); 
        rectTransform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / cancas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; 
        rectTransform.SetParent(originTransform);
    }
     
    public void SetDataItem(Item newItem) 
    { 
        currentItem = newItem;
        _imageItem.sprite = currentItem.imageItem;
        _nameItem.text = currentItem.nameItem;
        _imageItem.enabled = true;  
    }
    public void ResetDataItem()
    { 
        currentItem = null;
        _imageItem.sprite = null;
        _nameItem.text = " ";
        _imageItem.enabled = false;
    }
     
}
