using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class RagePanelController : MonoBehaviour {
    public int value = 0;
    public int cost = 2;
    public Text counter, displayText;
    public bool activated = false;
    public float waitBeforeRefill = 5.0f;
    public float refillTime = 5.0f;
    public float beanRage = 10.0f;
    public float delay = 0.1f;
    public float rageDuration = 10.0f;

    void Start ( ) {
        value = 0;
        activated = false;
        counter.text = value.ToString ( );
        displayText.text = "Rage Mode";
        displayText.gameObject.SetActive ( false );
        StartCoroutine ( Reactivate ( waitBeforeRefill ) );
    }

    
    public void IncreaseRage ( ) {
        value++;
        counter.text = value.ToString ( );
        GlobalManager.progressBar.IncrementValue (beanRage);
        
    }

    public void DecreaseRage ( ) {
        value -= cost;
        counter.text = value.ToString ( );
        GlobalManager.progressBar.DecrementValue ( GlobalManager.progressBar.Value );
        StartCoroutine ( Reactivate ( waitBeforeRefill ) );
    }

    IEnumerator Reactivate ( float time ) {
        yield return new WaitForSeconds ( time );
        StartCoroutine ( DeactivateRage (  ) );

    }

    IEnumerator DeactivateRage ( ) {
        yield return new WaitForSeconds ( rageDuration );
        activated = false;
        displayText.gameObject.SetActive ( false );
        StartCoroutine ( Refill ( refillTime ) );
    }

    IEnumerator Refill ( float time ) {
        GlobalManager.progressBar.IncrementValue (beanRage);
        yield return new WaitForSeconds ( delay);
        if (GlobalManager.progressBar.Value < 100) {
            StartCoroutine ( Refill ( time - delay ) );
        }
    }


}
