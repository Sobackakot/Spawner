
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class ItemInSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public int index { get; set; }
    public Item itemData { get; private set; }

    private Image _imageItem;
    private TextMeshProUGUI _nameItem;

    private Transform originTransform;
    private RectTransform pickItemTransform;
    private Canvas cancas;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        originTransform = GetComponent<Transform>();
        pickItemTransform = GetComponent<RectTransform>();

        cancas = pickItemTransform.GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        _imageItem =GetComponent<Image>();
        _nameItem = pickItemTransform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
        originTransform = transform.parent;
        pickItemTransform.SetParent(cancas.transform);
        pickItemTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        pickItemTransform.anchoredPosition += eventData.delta / cancas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        pickItemTransform.SetParent(originTransform);
    }
    public void SetDataItem(Item item) 
    { 
        itemData = item;
        _imageItem.sprite = itemData.imageItem;
        _nameItem.text = itemData.nameItem;
        _imageItem.enabled = true;
    }
    public void ResetDataItem()
    { 
        itemData = null;
        _imageItem.sprite = null;
        _nameItem.text = "";
        _imageItem.enabled = false;
    } 
}
