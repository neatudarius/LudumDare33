using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonAction : MonoBehaviour, IPointerUpHandler {
    public Text buttonName;
    public string command; 

    #region IPointerUpHandler implementation
   
    public void OnPointerUp(PointerEventData eventData) {
        if ( command != "" ) {
            FindObjectOfType<MenuController> ( ).Command ( command );
        }
    }

    #endregion

     
}
