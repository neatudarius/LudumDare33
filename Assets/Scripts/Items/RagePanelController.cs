using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

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
    public Text counter;
    public GameObject displayText;
    public bool activated = false;
    public float duration = 10f;

    public VignetteAndChromaticAberration effect;


    float t = 0f;
    void Start ( ) {
        value = 0;
        total = 0;
        activated = false;
        current = 0;

        counter.text = total.ToString ( );
        displayText.gameObject.SetActive ( false );
        
        
    }

    
    public void IncreaseRage ( ) {
        total++;
        current++;
        counter.text = total.ToString ( )+ " coins";
        if ( !activated )
            value = (float)current/needed*100f;
                   
    }

    public void Activate ( ) {
        activated = true;
        t = 0f;
        //displayText.gameObject.SetActive ( false );
        effect.enabled = true;
        GlobalManager.RageON ( );

    }

    void Update ( ) {
        if ( Ready ( ) ) {
            displayText.gameObject.SetActive (true );
        } else {
            displayText.gameObject.SetActive ( false );
        }
        if ( activated ) {
            value = Mathf.Lerp ( 100f, 0f, t += Time.deltaTime / duration );
        }
    }
    void FixedUpdate ( ) {
            if ( activated && GlobalManager.progressBar.current < 0.1f ) {
            activated = false;
            value = 0f;
            current = 0;
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
  }
