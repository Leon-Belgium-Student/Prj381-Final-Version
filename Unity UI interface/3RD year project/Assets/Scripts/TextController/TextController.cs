using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public List<GameObject> TextObjects;
    // Start is called before the first frame update
    void Start()
    {
        if (TextObjects.Count > 0)
        {
            ActivateObject(TextObjects[0]);
        }
    }

   public void ActivateObject(GameObject ObjToActivate)
   {
        foreach (var Text in TextObjects)
        {
            Text.SetActive(false);
        }
        ObjToActivate.SetActive(true);
   }
    
}
