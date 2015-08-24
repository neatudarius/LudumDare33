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
        if ( rage == null && GameObject.Find ( "RagePanel" ) ) {
            rage = GameObject.Find ( "RagePanel" ).GetComponent<RagePanelController> ( );
            progressBar = GameObject.Find ( "ProgressBarLabelRight" ).GetComponent<ProgressBarBehaviour> ( );
            player = GameObject.Find ( "Player" ).GetComponent<PlayerControl> ( );
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
    public static float backgroundSpeed_Rage = 20.0f;
    public static float foregroundSpeed_Rage = 25.0f;


    // curent speed
    public static float foregroundSpeed = 2.0f;
    public static float backgroundSpeed = 1.0f;

	// Adjust difficulty using these!!
	public static float baseDifficulty = 1.5f;
    public static float rageDifficulty = 15.0f;
    public static float maxDifficulty = 12f;
    static float lastDifficulty = 0f;
    public static float speedIncrement = 0.1f;

	// Do not give this initial value
	public static float difficultyMultiplier;

    // rageMode
    public static RagePanelController rage;
    public static ProgressBarBehaviour progressBar;
    public static PlayerControl player;

    //Random numbers
    static System.Random random;

    void Start ( ) {
        rage = null;
        progressBar = null;
        random = new System.Random ( );
        ResetDifficulty ();
        if ( rage == null && GameObject.Find ( "RagePanel" ) ) {
            rage = GameObject.Find ( "RagePanel" ).GetComponent<RagePanelController> ( );
            progressBar = GameObject.Find ( "ProgressBarLabelRight" ).GetComponent<ProgressBarBehaviour> ( );
            player = GameObject.Find ( "Player" ).GetComponent<PlayerControl> ( );
        }


        //gamejolt API
        if (GameJolt.API.Manager.Instance.CurrentUser != null) {
            //user is logged
        }
        else {
            //user is not logged
        }
    }

    void Update ( ) {
        /*
        if ( Input.GetKeyUp ( KeyCode.P ) ) {
            printSreenCounter++;
            Application.CaptureScreenshot ( StringsDatabase.screenShotName + printSreenCounter.ToString ( ) + ".png" );
        }
        */
        if ( Input.GetKeyUp ( KeyCode.R ) && rage && !rage.activated && rage.Ready()) {
            rage.Activate ( );
        }

		if(difficultyMultiplier < maxDifficulty) difficultyMultiplier += Time.deltaTime * speedIncrement;
    }
	
    public static void LoadLevel ( string nextLevel ) {
        Application.LoadLevel ( nextLevel );
    }
	
	static public void ResetDifficulty() {
		difficultyMultiplier = baseDifficulty;
	}

    static public void RageON ( ) {
        lastDifficulty = difficultyMultiplier;
        difficultyMultiplier = rageDifficulty;

        player.anim.SetInteger ( "state", 1 );
    }

    static public void RageOFF ( ) {
        difficultyMultiplier = lastDifficulty;
    }

    static public void FreezeSpeed() {
		difficultyMultiplier = 0.0f;
	}

    public static float rand ( float a, float b ) {
        float t = ( float ) random.NextDouble ( );
        return ( 1 - t ) * a + t * b;
    }

    public static int rand ( int a, int b ) {
        return random.Next ( a, b + 1 );
    }

    public void ShowSignIn() {
        GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
            if (success) {
                //user logged in
            }
            else {
                //user failed to log in
            }
        });
    }

    public void ShowLeaderboards() {
        GameJolt.UI.Manager.Instance.ShowLeaderboards((bool success) => {
            //user closed the leaderboards

        });
    }

    public void SendScore() {
        int dist = (int)player.distance, 
            coins = (int)rage.GetTotal();

        //send coins
        int scoreValue = coins;
        string scoreText = coins.ToString() + " coins"; // A string representing the score to be shown on the website.
        int tableID = 0; // Main highscore table.
        string extraData = ""; // This will not be shown on the website. You can store any information.

        GameJolt.API.Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });

        //send distance
        scoreValue = dist;
        scoreText = dist.ToString() + " meters";
        tableID = 91835; // Distance highscore table.
        
        GameJolt.API.Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => {
            Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
        });
        
    }
}