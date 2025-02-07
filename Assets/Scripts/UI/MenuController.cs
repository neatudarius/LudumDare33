﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuController : MonoBehaviour {
    //Global Manager
    GlobalManager globalManager;

    // Button prefabs
    public GameObject SF_ButtonPrefab;

    // Panels
    public GameObject MenuControlPanel;
    public GameObject CreditsControlPanel;
    public GameObject RagePanel;
    public GameObject resumeButton;

    // Maintenance
    private bool isGameOver = false;
    private bool isInMainMenu; // check if current scene is StringsDatabase.menuSceneName
    private bool isShowingMenu; // check if should show MenuControlPanel
    private bool isShowingCreditsPanel; // check if should show CreditsControlPanel


    // Main Menu/ Pause Menu
    private int ButtonsCount;
    private GameObject[ ] buttons;// as object
    public Text title, scoreDisplay;
    public GameObject titleObj;
    public GameObject signInPart;
    private string[ ] buttonsNames; // what you see
    public Sprite muteSprite, soundSprite;

    // Settings menu
    private GameObject backToMenuButton;

    int H, W,h,w,offset, MAX_CNT = 4 ;

    void Awake ( ) {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.SetResolution ( Screen.currentResolution.width, Screen.currentResolution.height, true );
        Screen.fullScreen = true;
        
    }
    void Start ( ) {
        globalManager = GameObject.FindObjectOfType<GlobalManager> ( );

       
        if ( AudioListener.volume > 0f )
        {
            GameObject soundButton = GameObject.Find ( "Sound" );
            if (soundButton != null)
                soundButton.GetComponent<Image> ( ).sprite = soundSprite;
        }
        else
        {
            GameObject soundButton = GameObject.Find ( "Sound" );
            if ( soundButton != null )
                soundButton.GetComponent<Image> ( ).sprite = muteSprite;
        }
         
        isInMainMenu = ( Application.loadedLevelName == StringsDatabase.menuSceneName ? true : false );
        isGameOver = false;

        if ( RagePanel != null ) {
            RagePanel.SetActive ( true );
        }


        if ( isInMainMenu ) {
            MainMenu ( );
            isShowingMenu = true;
        } else {
            PauseMenu ( );
            isShowingMenu = false;
        }

        //BuildMenuButtons ( );
        //BuildCreditsPanel ( );


        isShowingCreditsPanel = false;
        CreditsControlPanel.SetActive ( false );

        //signInPart.transform.SetParent ( MenuControlPanel.transform);
        //signInPart.transform.localScale = new Vector3 ( 1, 1, 1 );
        //signInPart.GetComponent<RectTransform> ( ).sizeDelta = new Vector2 ( w/1.5f, h*5 );
        if (GameJolt.API.Manager.Instance.CurrentUser == null) {
            signInPart.SetActive(true);
        }
        else {
            signInPart.SetActive(false);
        }
    }

    void Update ( ) {
        if ( ControlsManager.isEscPressed && !isGameOver ) {
            if ( isShowingMenu ) {
                if ( !isInMainMenu ) {
                    isShowingMenu = false;
                }
            } else {
                if ( isShowingCreditsPanel ) {
                    isShowingCreditsPanel = false;
                }
                isShowingMenu = true;
            }
            ControlsManager.isEscPressed = false;
        }

        if ( isGameOver ) {
            GameOverMenu ( );
        }

        // Show/hide mouse cursor
        Cursor.visible = ( isShowingMenu || isShowingCreditsPanel ? true : false );

        if ( isShowingMenu ) {
            ShowMenu ( );
        } else {
            HideMenu ( );
        }

        if ( isShowingCreditsPanel ) {
            ShowCreditsPanel ( );
        } else {
            HideCreditsPanel ( );
        }

    }

    // This functions is called by buttons. Execute commands
    public void Command ( string command ) {
        // Play Button - Main Menu => Play first level
        if ( command == StringsDatabase._playGameButton || command == StringsDatabase._replayGameButton ) {
            Time.timeScale = 1.0f;
            GlobalManager.LoadLevel ( StringsDatabase.gameSceneName );
            return;
        }

        // Return to game
        if ( command == StringsDatabase._resumeGameButton ) {
            HideMenu ( );
            return;
        }


        // Show Settings panel, you must be in the menu
        if ( command == StringsDatabase._creditsButton ) {
            HideMenu ( );
            ShowCreditsPanel ( );
            return;
        }



        // Hide Settings panel and return to menu
        if ( command == StringsDatabase._backToMenuButton ) {
            if ( isShowingCreditsPanel )
                HideCreditsPanel ( );
            ShowMenu ( );
            return;
        }

        if ( command == StringsDatabase._advanceToMenuScene ) {
            GlobalManager.LoadLevel ( StringsDatabase.menuSceneName );
            return;
        }

        if ( command == StringsDatabase._loginButton ) {
            ShowSignIn ( );
            return;
        }


        if ( command == StringsDatabase._showScoresButton) {
            GlobalManager.ShowLeaderBoards ( );
            return;
        }

        if ( command == StringsDatabase._muteButton ) {
            if ( AudioListener.volume > 0f ) {
                AudioListener.volume = 0f;
                GameObject.Find ( "Sound" ).GetComponent<Image> ( ).sprite = muteSprite;
            } else{
                AudioListener.volume = 1f;
                GameObject.Find ( "Sound" ).GetComponent<Image> ( ).sprite = soundSprite;
            }
            
            return;
        }

        if ( command == StringsDatabase._quitButton ) {
            Application.Quit ( );
            return;
        }

        if ( command == StringsDatabase._signUpButton )
        {
            Application.OpenURL ( "https://gamejolt.com/join" );
            return;
        }

    }

    void ShowMenu ( ) {
        Time.timeScale = 1.0f;

        isShowingMenu = true;
        MenuControlPanel.SetActive ( true );

        if ( RagePanel != null ) {
            RagePanel.SetActive ( false );
        }

        if ( !isInMainMenu ) {
            Time.timeScale = 0.0f;
            string dist = ( ( int ) GlobalManager.player.distance ).ToString ( ) + " meters ";
            string beans = GlobalManager.rage.GetTotal ( ).ToString ( ) + " coins";
            scoreDisplay.text = dist + "\n" + beans;
        }
    }

    void HideMenu ( ) {
        Time.timeScale = 1.0f;

        isShowingMenu = false;
        MenuControlPanel.SetActive ( false );

        if ( RagePanel != null ) {
            RagePanel.SetActive ( true );
        }
    }

    void ShowCreditsPanel ( ) {
        Time.timeScale = 1.0f;

        isShowingCreditsPanel = true;
        CreditsControlPanel.SetActive ( true );

        if ( RagePanel != null ) {
            RagePanel.SetActive ( false );
        }

        if ( !isInMainMenu ) {
            Time.timeScale = 0.0f;
        }
    }



    void HideCreditsPanel ( ) {
        Time.timeScale = 1.0f;

        isShowingCreditsPanel = false;
        CreditsControlPanel.SetActive ( false );

        if ( isShowingMenu ) {
            Time.timeScale = 0.0f;
        }
    }

    // Assign Main Menu Buttons' names
    void MainMenu ( ) {
        ButtonsCount = 4;

        buttons = new GameObject[ ButtonsCount ];
        buttonsNames = new string[ ButtonsCount ];

        buttonsNames[ 0 ] = StringsDatabase._playGameButton;
        buttonsNames[ 1 ] = StringsDatabase._showScoresButton;
        buttonsNames[ 2 ] = StringsDatabase._creditsButton;
        buttonsNames[ 3 ] = StringsDatabase._quitButton;
    }

    // Assign Pause Menu Buttons' names
    void PauseMenu ( ) {
        ButtonsCount = 3;

        buttons = new GameObject[ ButtonsCount ];
        buttonsNames = new string[ ButtonsCount ];

        buttonsNames[ 0 ] = StringsDatabase._resumeGameButton;
        buttonsNames[ 1 ] = StringsDatabase._advanceToMenuScene;
        buttonsNames[ 2 ] = StringsDatabase._quitButton;
    }

    // Assign Game Over Menu Buttons' names
    void GameOverMenu ( ) {
        if ( isShowingMenu ) {
            return;
        }

        resumeButton.GetComponent<ButtonAction> ( ).buttonName.text = StringsDatabase._replayGameButton;
        resumeButton.GetComponent<ButtonAction> ( ).command = StringsDatabase._replayGameButton;


        isShowingMenu = true;


        if ( GameJolt.API.Manager.Instance.CurrentUser == null ) {
            signInPart.SetActive ( true );
        }
        else {
            signInPart.SetActive(false);
            GlobalManager.SendScore ( );
        }

    }

    // For a preset set of value build a menu
    void BuildMenuButtons ( ) {
        title.text = StringsDatabase.gameName;
        Vector3 currentPosition = new Vector3 ( -offset, titleObj.transform.localPosition.y - 5*offset, 0 );
        Vector3 translation = new Vector3 ( 0, -( h + offset ), 0 );
        for ( int i = 0; i < ButtonsCount; i++ ) {
            currentPosition += translation;
            buttons[ i ] = GetButton ( SF_ButtonPrefab, MenuControlPanel, buttonsNames[ i ], currentPosition );
        }
    }


    // For a preset set of value build a settings menu
    void BuildCreditsPanel ( ) {
        float W = Screen.width, H = Screen.height;
        Vector3 position = new Vector3 ( -W / 3.3f, -H / 2.0f + 3*offset, 0f );
        backToMenuButton = GetButton ( SF_ButtonPrefab, CreditsControlPanel, StringsDatabase._backToMenuButton, position );


    }

    // Create a button, set parent, position, scale (1,1,1)
    private GameObject GetButton ( GameObject ButtonPrefab, GameObject ButtonParent, string ButtonName, Vector3 localPosition ) {
        GameObject newButton = ( GameObject ) Instantiate ( ButtonPrefab );
        newButton.transform.SetParent ( ButtonParent.transform );
        newButton.GetComponent<ButtonAction> ( ).buttonName.text = ButtonName;
        newButton.GetComponent<ButtonAction> ( ).command = ButtonName;
        newButton.transform.localPosition = localPosition;
        newButton.transform.localScale = new Vector3 ( 1, 1, 1 );
        newButton.GetComponent<RectTransform> ( ).sizeDelta = new Vector2(w,h);
        return newButton;
    }

    public void GameIsOver ( float delay = 1.0f ) {
        GlobalManager.backgroundSpeed = 0f;
        GlobalManager.foregroundSpeed = 0f;
        StartCoroutine ( MakeGameOver ( delay ) );
    }

    IEnumerator MakeGameOver( float time = 1.0f ) {
        yield return new WaitForSeconds ( time );
        isGameOver = true;
    }

    public void ShowSignIn ( ) {
        GameJolt.UI.Manager.Instance.ShowSignIn ( ( bool success ) => {
            if ( success ) {
                if (Application.loadedLevelName== StringsDatabase.gameSceneName) GlobalManager.SendScore ( );
                signInPart.SetActive ( false );
            }
        } );
    }
}