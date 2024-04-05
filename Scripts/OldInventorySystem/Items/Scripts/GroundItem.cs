using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject_SO item;
    //atomatically grab the "uiDisplay" of the corresponding ItemObject and set it to the sprite child



    //Anything that is changed in the editor is a serialization change which is goning to fire the callback function below:
    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        //This lets unity knows that something changed on that object so you are able to save it
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }
    


}
