using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CoffeeBeansController : MonoBehaviour {
    public GameObject CoffeeBeanPrefab;
    public GameObject leftBound, rightBound;
    public Transform coffeeParent;
    public float delayTime = 0.001f;
    public float minY = 1.0f, maxY = 5.0f, xdif = 0.001f,ydif=0.001f;
    float Y_MAX = 8f;
    float Y_MIN = 5.5f;

    List<GameObject> listOfBeans;
    bool avaible;

    // Use this for initialization
    void Start ( ) {
        listOfBeans = new List<GameObject> ( );
        avaible = true;
    }

    void Update ( ) {
        if ( avaible ) {
            avaible = false;
            float y, R;
            int cnt;

            int rand = GlobalManager.rand ( 1, 7 );

            switch ( rand ) {
                case 1:
                    //line 
                    y = GlobalManager.rand ( 1.0f, 3.0f );
                    cnt = GlobalManager.rand ( 3, 5 );
                    StartCoroutine ( TrowBeans_LINE (y ,  cnt) );
                    break;
                case 2:
                    // double line
                    y = GlobalManager.rand ( 1.0f, 3.0f );
                    cnt = GlobalManager.rand ( 3, 5 );
                    StartCoroutine ( TrowBeans_LINE ( y, cnt ) );
                    StartCoroutine ( TrowBeans_LINE ( y+1.0f, cnt ) );
                    break;
                case 3:
                    // vertical line
                    y = GlobalManager.rand ( 1.0f, 3.0f );
                    cnt = GlobalManager.rand ( 3, 5 );
                    for ( int i = 0; i < cnt; i++ ) {
                        StartCoroutine ( TrowBeans_LINE ( y, 1 ) );
                        y += 1.0f;
                    }
                    break;
                case 4:
                    // vertical double line
                    y = GlobalManager.rand ( 1.0f, 3.0f );
                    cnt = GlobalManager.rand ( 3, 5 );
                    for ( int i = 0; i < cnt; i++ ) {
                        StartCoroutine ( TrowBeans_LINE ( y, 1 ) );
                        y += 1.0f;
                    }
                    break;
                case 5:
                    // Ascending
                    y = GlobalManager.rand ( 1.0f, 3.0f );
                    cnt = GlobalManager.rand ( 3, 5 );
                    StartCoroutine ( TrowBeans_Ascending ( y, cnt ) );
                    break;
                case 6:
                    //Descending
                    cnt = GlobalManager.rand ( 3, 5 );
                    StartCoroutine ( TrowBeans_Descending (6.0f, cnt ) );
                    break;
                case 7:
                    // Circle
                    cnt = GlobalManager.rand ( 4, 8 );
                    R = GlobalManager.rand ( 1.0f, 2.5f );
                    StartCoroutine ( Circle (cnt, R ) );
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator TrowBeans_Descending ( float y, int cnt ) {
        listOfBeans.Add ( GetBean ( y ) );
        yield return new WaitForSeconds ( delayTime );
        y -= ydif;
        cnt--;
        if ( cnt > 0 && y > minY )
            StartCoroutine ( TrowBeans_Descending ( y, cnt ) );
        else {
            int rand = GlobalManager.rand ( 1, 2 );
            if ( rand % 2 == 0 )
                StartCoroutine ( TrowBeans_Ascending (y, GlobalManager.rand ( 1, 5 ) ) );
            else
                StartCoroutine ( Release ( ) );
        }
    }

    IEnumerator TrowBeans_Ascending ( float y, int cnt ) {
        listOfBeans.Add ( GetBean ( y) );
        yield return new WaitForSeconds ( delayTime );
        y += ydif;
        cnt--;
        if ( cnt > 0 && y < Y_MAX )
            StartCoroutine ( TrowBeans_Ascending ( y, cnt ) );
        else {
            StartCoroutine ( Release ( ) );
        }
    }

    IEnumerator TrowBeans_LINE (float y, int cnt ) {
        listOfBeans.Add ( GetBean ( y ) );
        yield return new WaitForSeconds ( delayTime );
        cnt--;
        if ( cnt > 0 )
            StartCoroutine ( TrowBeans_LINE (y, cnt ) );
        else
            StartCoroutine ( Release ( ) );
    }

    IEnumerator Circle ( int cnt, float R ) {
        Vector2 center = new Vector2 ( 18f, GlobalManager.rand ( 3.5f, 5f ) );
        Vector2[ ] pos = new Vector2[ cnt ];
        float angle = 0, phi = 2 * Mathf.PI / cnt;
        for ( int i = 0; i < cnt; i++ ) {
            float c = ( float ) Mathf.Cos ( angle ), s = ( float ) Mathf.Sin ( angle );
            listOfBeans.Add ( GetBean ( center + new Vector2 ( c * R, s * R ) ) );
            
            Debug.Log ( angle + " " + new Vector2 ( c * R, s * R ) );

            angle += phi;
        }
        yield return new WaitForSeconds ( 0.01f );
        StartCoroutine ( Release ( 2.0f ) );
    }

    
    IEnumerator Release ( float time = 1.0f ) {
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


    private GameObject GetBean ( Vector2 pos ) {
        GameObject newBean = ( GameObject ) Instantiate ( CoffeeBeanPrefab, transform.position, Quaternion.identity );
        newBean.transform.SetParent ( coffeeParent );
        newBean.transform.position = new Vector3 ( pos.x, pos.y, -1 );
        //newBean.transform.localScale = new Vector3 ( 0.8f, 0.8f, 0.8f );
        newBean.transform.localScale = new Vector3 ( 0.4f, 0.4f, 0.4f );
        return newBean;
    }
}
