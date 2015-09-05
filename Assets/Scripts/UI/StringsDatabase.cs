using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class StringsDatabase : MonoBehaviour
{
    #region StringsDatabase's Things - DON'T CHANGE!!!
	private static StringsDatabase _instance;

	public static StringsDatabase instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<StringsDatabase> ();

				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad (_instance.gameObject);
			}

			return _instance;
		}
	}

	void Awake ()
	{
		if (_instance == null) {
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad (this);
		} else {
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if (this != _instance)
				Destroy (this.gameObject);
		}
	}
    #endregion

    #region Titles, Names
    public static string
        gameName = "\nTREX Gone MAD",
        menuSceneName = "Menu",
        gameSceneName = "Game",
        loadingSceneName = "Loading";

    #endregion

    #region Tags
	public static string
		__player = "Player";
    #endregion

    #region Buttons
    public static string
        _playGameButton = "Play",
        _replayGameButton = "Play again :p",
        _resumeGameButton = "Resume",
        _backToMenuButton = "Back",
        _creditsButton = "Credits",
        _advanceToMenuScene = "Main Menu",
        _loginButton = "Log in",
        _showScoresButton = "Leaderboard",
        _muteButton = "Mute",
        _quitButton = "Quit",
        _rageButton = "Press to charge",
        _runButton = "Run",
        _jumpButton = "Jump",
        _advanceToGameButton = "Advance",
        _pleaseWaitButton = "Please wait",
        _escButton = "Esc",
        _signUpButton = "Sign up";

    public static string
        loadingMessage = "Loading",
        loadedGameMessage = "Loading";
        
    #endregion



    public static string
        screenShotName = "Screenshot";

    

}