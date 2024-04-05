using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requiredItem;

    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        //如果持有正确物品并且事件未触发过
        if (itemName == requiredItem && !isDone)
        {
            //事件触发标志变化
            isDone = true;
            //调用事件
            OnClickedAction();
            EventHandler.CallItemUsedEvent(itemName);

        }
    }


    //持有正确物品时点击触发事件
    protected virtual void OnClickedAction()
    {

    }

    public virtual void EmptyClicked()
    {
        Debug.Log("EmptyClicked");
    }
}
