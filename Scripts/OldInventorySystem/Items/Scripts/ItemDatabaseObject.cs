using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Database", menuName = "Inventory System/Item/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject_SO[] ItemObjects;

    //Use Double dictionary to store, quicker to retrieve but heavier to store
    public Dictionary<int, ItemObject_SO> GetItem = new Dictionary<int, ItemObject_SO>();
    //check if a key is in the dictionary


    public void OnAfterDeserialize(){



        for (int i = 0; i<ItemObjects.Length; i++){
            ItemObjects[i].data.Id = i;
            GetItem.Add(i, ItemObjects[i]);
        }
        
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject_SO>();
        
    }
}
