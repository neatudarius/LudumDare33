using UnityEngine;
using System.Collections.Generic;

public class Combo : MonoBehaviour {
    public List<GameObject> coins = new List<GameObject> ();
    bool canMakeCombo = true;
    public float timeUntilDestroy = 0.7f;
    CoinsController coinsController;
    int value = 0;
    void Start()
    {
        canMakeCombo = true;
        coinsController = transform.parent.parent.gameObject.GetComponent<CoinsController> ( );
        value = coins.Count;
    }
    // Use this for initialization
    public void Remove ( string coinName, string flag )
    {
        coins.RemoveAll ( item => item.name == coinName );
        if ( flag != "collect" )
            canMakeCombo = false;
        if ( coins.Count == 0 )
        {
            if ( canMakeCombo && value > 1)
            {
                GlobalManager.rage.Combo ( value );
            }
            coinsController.Remove ( gameObject.name );
            Destroy ( gameObject, timeUntilDestroy );
        }
    }

    public void MakeAvaible()
    {
        coinsController.MakeAvaible ( );
    }
}
