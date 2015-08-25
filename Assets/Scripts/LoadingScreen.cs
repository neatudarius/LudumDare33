using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
    public Text displayText;
    public Text buttonName;
    
    void Start ( ) {
        buttonName.text = StringsDatabase._pleaseWaitButton;
        displayText.text = StringsDatabase.loadingMessage;
    }

    void Update ( ) {
        if ( GlobalManager.PercentLoaded ( ) >= 0.9f ) {
            displayText.text = StringsDatabase.loadedGameMessage;
            buttonName.text = StringsDatabase._advanceToGameButton;
        }
    
    }

}