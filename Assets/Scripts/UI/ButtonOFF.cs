using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonOFF : MonoBehaviour, IPointerUpHandler {
    public Text buttonName;
    public string command;

    #region IPointerUpHandler implementation

    public void OnPointerUp ( PointerEventData eventData ) {
        //Debug.Log ( command + " OFF " );
        if ( command == StringsDatabase._runButton ) {
            ControlsManager.isRunPressed = false;
            return;
        }

        if ( command == StringsDatabase._jumpButton ) {
            ControlsManager.isJumpPressed = false;
            return;
        }
        if ( command == StringsDatabase._rageButton ) {
            ControlsManager.isRagePressed = false;
            return;
        }

        if ( command == StringsDatabase._escButton ) {
            ControlsManager.isEscPressed = false;
            return;
        }
    }
    #endregion

}
