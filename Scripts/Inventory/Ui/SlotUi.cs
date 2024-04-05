using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUi : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public ItemToolTip toolTip;
    private ItemDetails currentItem;
    private bool isSelected;

    public void SetItem (ItemDetails _itemDetails)
    {
        currentItem = _itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = _itemDetails.itemSprite;
        // itemImage.SetNativeSize();

    }

    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //点击选择物品，再次点击取消选择
        isSelected = ! isSelected;

        //触发可能的事件
        EventHandler.CallItemSelectedEvent( currentItem, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入时显示ToolTip
        if(this.gameObject.activeInHierarchy)        
        {
            toolTip.gameObject.SetActive(true);
            toolTip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标离开时不显示ToolTip
        toolTip.gameObject.SetActive(false);
        
    }
}
