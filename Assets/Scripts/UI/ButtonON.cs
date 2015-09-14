using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonON : MonoBehaviour, IPointerDownHandler{
    public Text buttonName;
    public string command;

    #region IPointerDownHandler implementation

    public void OnPointerDown( PointerEventData eventData ) {
        //Debug.Log ( command + " ON " );
        if ( command == StringsDatabase._runButton ) {
            ControlsManager.isRunPressed = true;
            return;
        }

        if ( command == StringsDatabase._jumpButton ) {
            ControlsManager.isJumpPressed = true;
            return;
        }

        if ( command == StringsDatabase._rageButton ) {
            ControlsManager.isRagePressed = true;
            return;
        }

        if ( command == StringsDatabase._escButton ) {
            ControlsManager.isEscPressed = true;
            return;
        }
    }


    #endregion

    void LateUpdate ( ) {
        ControlsManager.ResetAll ( );
    }

}
