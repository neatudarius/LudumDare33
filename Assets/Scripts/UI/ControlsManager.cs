using UnityEngine;
using System.Collections;

public class ControlsManager : MonoBehaviour {
    public static bool isJumpPressed;
    public static bool isRunPressed;
    public static bool isRagePressed;
    public static bool isEscPressed;

    
    // Use this for initialization
    void Start () {
        isJumpPressed = false;
        isRunPressed = false;
        isRagePressed = false;
        isEscPressed = false;
    }

    void Update ( ) {
        //bug.Log ( "jump: " + isJumpPressed.ToString ( ) + " " + "rage: " + isRagePressed.ToString( ) );
    }
    public static void ResetAll ( ) {
        isRagePressed = false;
        isEscPressed = false;
    }
}
