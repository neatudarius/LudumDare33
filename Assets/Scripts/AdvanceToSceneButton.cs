
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AdvanceToSceneButton : MonoBehaviour, IPointerUpHandler {
    public Text buttonName;


    
    #region IPointerUpHandler implementation
   
    public void OnPointerUp(PointerEventData eventData) {
        if ( buttonName.text == StringsDatabase._advanceToGameButton ) {
               GlobalManager.ActivateScene ( );
           }
    }

    #endregion

     
}