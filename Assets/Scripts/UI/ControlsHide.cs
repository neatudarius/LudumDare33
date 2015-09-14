using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsHide : MonoBehaviour {
    public Image [ ] backgrounds;
    public Text [ ] texts; 
	// Use this for initialization
	void Start () {
        StartCoroutine ( DeactivateBackgrouds ( 10f ) );
	}
	
	IEnumerator DeactivateBackgrouds ( float time )
    {
        yield return new WaitForSeconds ( time );
        for ( int i = 0; i < backgrounds.Length; i++ )
        {
            //Make background invisible
            Color c = backgrounds [ i ].color;
            c.a = 0f;
            backgrounds [ i ].color = c;
            //Disable text
            texts [ i ].transform.gameObject.SetActive ( false );
        }
    }
}
