using UnityEngine;
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

    // Maintenance
    private bool isGameOver = false;
    private bool isInMainMenu; // check if current scene is StringsDatabase.menuSceneName
    private bool isShowingMenu; // check if should show MenuControlPanel
    private bool isShowingCreditsPanel; // check if should show CreditsControlPanel


    // Main Menu/ Pause Menu
    private int ButtonsCount;
    private GameObject[ ] buttons;// as object
    public Text title;
    public GameObject signInPart;
    private string[ ] buttonsNames; // what you see


    // Settings menu
    private GameObject backToMenuButton;

    void Start ( ) {
        globalManager = GameObject.FindObjectOfType<GlobalManager> ( );

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

        BuildMenuButtons ( );
        BuildCreditsPanel ( );


        isShowingCreditsPanel = false;
        CreditsControlPanel.SetActive ( false );

        if (GameJolt.API.Manager.Instance.CurrentUser == null) {
            signInPart.SetActive(true);
        }
        else {
            signInPart.SetActive(false);
        }
    }

    void Update ( ) {
        if ( Input.GetKeyDown ( KeyCode.Escape ) && !isGameOver ) {
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
        ButtonsCount = 3;

        buttons = new GameObject[ ButtonsCount ];
        buttonsNames = new string[ ButtonsCount ];

        buttonsNames[ 0 ] = StringsDatabase._playGameButton;
        buttonsNames[ 1 ] = StringsDatabase._showScoresButton;
        buttonsNames[ 2 ] = StringsDatabase._creditsButton;
    }

    // Assign Pause Menu Buttons' names
    void PauseMenu ( ) {
        ButtonsCount = 2;

        buttons = new GameObject[ ButtonsCount ];
        buttonsNames = new string[ ButtonsCount ];

        buttonsNames[ 0 ] = StringsDatabase._resumeGameButton;
        buttonsNames[ 1 ] = StringsDatabase._advanceToMenuScene;
    }

    // Assign Game Over Menu Buttons' names
    void GameOverMenu ( ) {
        if ( isShowingMenu ) {
            return;
        }

        buttons[ 0 ].GetComponent<ButtonAction> ( ).buttonName.text = StringsDatabase._replayGameButton;
        buttons[ 0 ].GetComponent<ButtonAction> ( ).command = StringsDatabase._replayGameButton;


        isShowingMenu = true;

        string dist = ((int)GlobalManager.player.distance).ToString() + " meters ";
        string beans = GlobalManager.rage.GetTotal ( ).ToString() + " coins";
        title.text = dist + "\n" + beans;
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
        Vector3 currentPosition = new Vector3 ( -15, +100, 0 );
        Vector3 offset = new Vector3 ( 0, -50, 0 );
        for ( int i = 0; i < ButtonsCount; i++ ) {
            currentPosition += offset;
            buttons[ i ] = GetButton ( SF_ButtonPrefab, MenuControlPanel, buttonsNames[ i ], currentPosition );
        }
    }


    // For a preset set of value build a settings menu
    void BuildCreditsPanel ( ) {
        float W = Screen.width, H = Screen.height;
        Vector3 position = new Vector3 ( -W / 3.3f, +H / 3.0f, 0f );
        Vector3 translation = new Vector3 ( 0, -50, 0 );

        backToMenuButton = GetButton ( SF_ButtonPrefab, CreditsControlPanel, StringsDatabase._backToMenuButton, position + 8 * translation );


    }

    // Create a button, set parent, position, scale (1,1,1)
    private GameObject GetButton ( GameObject ButtonPrefab, GameObject ButtonParent, string ButtonName, Vector3 localPosition ) {
        GameObject newButton = ( GameObject ) Instantiate ( ButtonPrefab );
        newButton.transform.SetParent ( ButtonParent.transform );
        newButton.GetComponent<ButtonAction> ( ).buttonName.text = ButtonName;
        newButton.GetComponent<ButtonAction> ( ).command = ButtonName;
        newButton.transform.localPosition = localPosition;
        newButton.transform.localScale = new Vector3 ( 1, 1, 1 );
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