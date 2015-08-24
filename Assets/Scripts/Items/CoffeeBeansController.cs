using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CoffeeBeansController : MonoBehaviour {
    public GameObject CoffeeBeanPrefab;
    public GameObject leftBound, rightBound;
    public Transform coffeeParent;
    public float delayTime = 0.1f;
    public float minY = 1.0f, maxY = 5.0f, dif = 3f;
    float Y_MAX = 8f;
    float Y_MIN = 5.5f;
    float defaultY = 1.0f;

    List<GameObject> listOfBeans;
    bool avaible;

    // Use this for initialization
    void Start ( ) {
        listOfBeans = new List<GameObject> ( );
        avaible = true;
        defaultY = minY;
    }

    void FixedUpdate ( ) {
        if ( avaible ) {
            avaible = false;
            int rand = GlobalManager.rand ( 1, 3 );
            switch ( rand ) {
                case 1:
                    //line 
                    defaultY = GlobalManager.rand ( 1.0f, 3.0f );
                    StartCoroutine ( TrowBeans_LINE ( GlobalManager.rand (3,5) ) );
                    break;
                case 2:
                    // Ascending
                    defaultY = GlobalManager.rand ( 1.0f, 3.0f );
                    StartCoroutine ( TrowBeans_Ascending ( GlobalManager.rand ( 3, 5 ) ) );
                    break;
                case 3:
                    //Descending
                    defaultY = 6.0f;
                    StartCoroutine ( TrowBeans_Descending ( GlobalManager.rand ( 3, 5 ) ) );
                    break;
            }
        }
    }

    IEnumerator TrowBeans_Descending ( int cnt ) {
        listOfBeans.Add ( GetBean ( defaultY ) );
        yield return new WaitForSeconds ( delayTime );
        defaultY -= dif;
        cnt--;
        if ( cnt > 0 && defaultY > minY )
            StartCoroutine ( TrowBeans_Descending ( cnt ) );
        else {
            int rand = GlobalManager.rand(1,2);
            if ( rand % 2 == 0 )
                StartCoroutine ( TrowBeans_Ascending ( GlobalManager.rand ( 1, 5 ) ) );
            else
                StartCoroutine ( Release ( 2.0f ) );
        }
    }

    IEnumerator TrowBeans_Ascending ( int cnt ) {
        listOfBeans.Add ( GetBean ( defaultY ) );
        yield return new WaitForSeconds ( delayTime );
        defaultY += dif;
        cnt--;
        if ( cnt > 0 && defaultY < Y_MAX )
            StartCoroutine ( TrowBeans_Ascending ( cnt ) );
        else {
            int rand = GlobalManager.rand(1,2);
            if ( rand % 2 == 0 )
                StartCoroutine ( TrowBeans_Descending ( GlobalManager.rand ( 1, 5 )  ) );
            else
                StartCoroutine ( Release ( 2.0f ) );
        }
    }

    IEnumerator TrowBeans_LINE ( int cnt ) {
        listOfBeans.Add ( GetBean ( defaultY ) );
        yield return new WaitForSeconds ( delayTime );
        cnt--;
        if ( cnt > 0 )
            StartCoroutine ( TrowBeans_LINE ( cnt ) );
        else
            StartCoroutine ( Release ( 2.50f ) );
    }


    IEnumerator Release ( float time ) {
        yield return new WaitForSeconds ( time );
        avaible = true;
    }

    private GameObject GetBean ( float _defaultY ) {
        GameObject newBean = ( GameObject ) Instantiate ( CoffeeBeanPrefab, transform.position, Quaternion.identity );
        newBean.transform.SetParent ( coffeeParent );
        newBean.transform.position = new Vector3 ( transform.position.x, _defaultY, -1 );
        //newBean.transform.localScale = new Vector3 ( 0.8f, 0.8f, 0.8f );
        newBean.transform.localScale = new Vector3 ( 0.4f, 0.4f, 0.4f );
        return newBean;
    }
}
