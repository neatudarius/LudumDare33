using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RangePanelController : MonoBehaviour {
    public int range;
    public Text rangeText;

	
	void Start () {
        range = 0;
        rangeText.text = range.ToString ( );
    }


    public void IncreaseRange ( ) {
        ++range;
        rangeText.text = range.ToString ( );
    }
}
