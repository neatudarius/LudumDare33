using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using ProgressBar;
public class GlobalManager : MonoBehaviour {
    #region GlobalManager's Things - DON'T CHANGE!!!
    private static GlobalManager _instance;

    public static GlobalManager instance {
        get {
            if ( _instance == null ) {
                _instance = GameObject.FindObjectOfType<GlobalManager> ( );

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad ( _instance.gameObject );
            }

            return _instance;
        }
    }

    void Awake ( ) {
        if ( _instance == null ) {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad ( this );
        } else {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if ( this != _instance )
                Destroy ( this.gameObject );
        }
    }
    #endregion    
    //static int printSreenCounter = 0;

    //default speed values
    public static float backgroundSpeed_Normal = 1.0f;
    public static float foregroundSpeed_Normal = 2.0f;
    public static float backgroundSpeed_Accelerated = 2.0f;
    public static float foregroundSpeed_Accelerated = 3.0f;
    public static float backgroundSpeed_Jumping = 10.0f;
    public static float foregroundSpeed_Jumping = 12.0f;

    // curent speed
    public static float foregroundSpeed = 2.0f;
    public static float backgroundSpeed = 1.0f;

    // rageMode

    public static RagePanelController rage;
    public static ProgressBarBehaviour progressBar;

    void Start ( ) {
        rage = GameObject.Find ( "RagePanel" ).GetComponent<RagePanelController> ( );
        progressBar = GameObject.Find ( "ProgressBarLabelRight" ).GetComponent<ProgressBarBehaviour> ( );
    }


    void Update ( ) {
        /*
        if ( Input.GetKeyUp ( KeyCode.P ) ) {
            printSreenCounter++;
            Application.CaptureScreenshot ( StringsDatabase.screenShotName + printSreenCounter.ToString ( ) + ".png" );
        }
        */
        if ( Input.GetKeyUp ( KeyCode.R ) ) {
            if ( !rage.activated && rage.cost <= rage.value && progressBar.Value >= 100) {
                rage.activated = true;
                rage.DecreaseRage ( );
                rage.displayText.gameObject.SetActive ( true );
            }

        }
    }


    public void LoadLevel ( string nextLevel ) {
        Application.LoadLevel ( nextLevel );
    }



}