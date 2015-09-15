using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

enum RageState
{
    Charging = 0,
    Discharging = 1,
    Full = 2
}

public class RagePanelController : MonoBehaviour {
    public float value {
        get {
            if ( GlobalManager.progressBar == null )
                return 0;
            return GlobalManager.progressBar.Value;
        }
        set {
            if ( GlobalManager.progressBar == null )
                return;
            GlobalManager.progressBar.Value = value;
        }
    }
    public int needed = 100;
    int total = 0, current = 0 ;
    public Text counter, comboDisplay;
    public GameObject displayText;
    public bool activated = false;
    private RageState state;
    public float duration = 10f;
    public int incCoinsPeRage = 10;

    public VignetteAndChromaticAberration effect;

    private int _comboMultiply = 1;
    private int comboMultiply
    {
        get { return _comboMultiply; }
        set{
            _comboMultiply = value;
            comboDisplay.text = "Combo x" + _comboMultiply.ToString ( );
        }
    }
    public float comboDuration = 5f;
    private float resetComboTime = 0f;

    float t = 0f;

    private AudioSource audioSource;
    public AudioClip rageSound, comboSound;

    void Start ( ) {
        value = 0;
        total = 0;
        activated = false;
        state = RageState.Charging;
        current = 0;
        comboMultiply = 1;
        resetComboTime = 0f;

        counter.text = total.ToString ( ) + " coins";
        comboDisplay.text = "Combo x" + comboMultiply.ToString ( );
        displayText.gameObject.SetActive ( false );

        audioSource = GetComponent<AudioSource> ( );
    }

    
    public void IncreaseRage ( ) {
        total += comboMultiply;
        counter.text = total.ToString ( ) + " coins";
       
        

        if ( state == RageState.Charging )
        {
            current += comboMultiply;
            if ( current > needed )
                current = needed;
            value = ( float ) current / needed * 100f;
        }

    }

    public void Activate ( ) {
        activated = true;
        state = RageState.Discharging;
        current = 0;
        needed += incCoinsPeRage;
        comboMultiply = 1;
        t = 0f;
        effect.enabled = true;
        GlobalManager.RageON ( );
        audioSource.PlayOneShot ( rageSound );
    }

    void Update ( ) {
        //Debug.Log ( current + "/"+ value + "/"+ needed + " remain: " +  (resetComboTime - Time.time) +"s");
        if (Time.time > resetComboTime && comboMultiply > 1)
            comboMultiply = 1;
        if ( Ready ( ) ) {
            displayText.gameObject.SetActive (true );
        } else {
            displayText.gameObject.SetActive ( false );
        }
        if (state == RageState.Discharging ) {
            value = Mathf.Lerp ( 100f, 0f, t += Time.deltaTime / duration );
        }
    }
    void FixedUpdate ( ) {
        if ( activated && GlobalManager.progressBar.current < 0.1f ) {
            activated = false;
            state = RageState.Charging;
            value = 0f;
            //displayText.gameObject.SetActive ( false );
            effect.enabled = false;
            GlobalManager.RageOFF ( );
        }
    }

    public bool Ready ( ) {
        return GlobalManager.progressBar.isDone;       
    }

    public int GetTotal ( ) {
        return total;
    }

    public void Combo(int value)
    {
        comboMultiply++;
        resetComboTime = Time.time + comboDuration;
        audioSource.PlayOneShot ( comboSound );
    }

  }
