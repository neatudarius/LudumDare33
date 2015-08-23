using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RagePanelController : MonoBehaviour {
    public int value = 0;
    public int cost = 2;
    public Text textObj;
    public bool activated = false;

    void Start ( ) {
        value = 0;
        activated = false;
        textObj.text = value.ToString ( );
    }


    public void IncreaseRage ( ) {
        value++;
        textObj.text = value.ToString ( );
    }

    public void DecreaseRage ( ) {
        value -= cost;
        textObj.text = value.ToString ( );
        StartCoroutine ( Reactivate ( 5.0f ) );
    }

    IEnumerator Reactivate ( float time ) {
        yield return new WaitForSeconds ( time );
        activated = false;
    }
}
