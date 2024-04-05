using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x,  Input.mousePosition.y, 0));
    private ItemName currentItem;
    private bool canClick;
    private bool holdItem;

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;

    }

    

    private void Update()
    {
        canClick = ObjectAtMousePosition();


        //如果手物体激活则跟随鼠标位置
        if(hand.gameObject.activeInHierarchy)
        {
            hand.position =  Input.mousePosition;

        }
        //检测与鼠标互动的物体
        if(canClick && Input.GetMouseButtonDown(0))
        {
            //检测鼠标互动情况
            //Debug.Log("ClickAction");
            ClickAction(ObjectAtMousePosition().gameObject);
        }

    }

    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);

    }

    private void OnItemSelectedEvent( ItemDetails _itemDetails, bool _isSelected)
    {
        
        holdItem = _isSelected;
        if (_isSelected)
        {
            currentItem = _itemDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);

    }

    //根据互动物体的tag调用不同的行为
    private void ClickAction(GameObject clickObject)
    {
        
        switch (clickObject.tag)
        {
            case "Teleport":
                
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item_Remake>();
                item?.ItemClicked();
                break;

            case "Interactive":
               
                var interactive = clickObject.GetComponent<Interactive>();
                //如有持有物品，会判断是否是触发事件的物品
               
                if (holdItem)
                {
                    interactive?.CheckItem(currentItem);
                }
                else
                {
                    interactive?.EmptyClicked();
                }
                break;
            case "Player":
                var unitUIControl = clickObject.GetComponent<IUnitUIControl>();
                unitUIControl?.UnitClicked();
                break;
            case "UIButton":
                var IUIButton = clickObject.GetComponent<IUIButton>();
                IUIButton?.ButtonClicked();
                break;


        }
    }
    




    private Collider2D ObjectAtMousePosition()
    {
        //Return gameobject the overlap with the mouse position
        return Physics2D.OverlapPoint(mouseWorldPos);
    }

}
