using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CoffeeBeansController : MonoBehaviour {
    public GameObject CoffeeBeanPrefab;
    public GameObject leftBound, rightBound;
    public Transform coffeeParent;
    public float delayTime = 0.5f;
    public float minY = 1.0f, maxY = 5.0f, xdif = 0.001f, ydif = 0.001f;
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

            int rand = GlobalManager.rand ( 1, 9 );

            switch ( rand ) {
                case 1:
                    //line 
                    y = GlobalManager.rand ( 1.5f, 5.0f );
                    cnt = GlobalManager.rand ( 3, 8 );
                    StartCoroutine ( TrowBeans_Horizontal ( y, cnt ) );
                    break;
                case 2:
                    // double line
                    y = GlobalManager.rand ( 1.5f, 2.0f );
                    cnt = GlobalManager.rand ( 3, 8 );
                    StartCoroutine ( TrowBeans_Horizontal ( y, cnt ) );
                    StartCoroutine ( TrowBeans_Horizontal ( y + GlobalManager.rand ( 3.0f, 4.5f ), cnt ) );
                    break;
                case 3:
                    // vertical line
                    y = GlobalManager.rand ( 1.0f, 3.0f );
                    cnt = GlobalManager.rand ( 3, 5 );
                    for ( int i = 0; i < cnt; i++ ) {
                        StartCoroutine ( TrowBeans_Vertical ( y, 1 ) );
                        y += ydif;
                    }
                    break;
                case 4:
                    // vertical double line
                    avaible = true;
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
                    StartCoroutine ( TrowBeans_Descending ( 6.0f, cnt ) );
                    break;
                case 7:
                    // Circle
                    cnt = GlobalManager.rand ( 4, 8 );
                    R = GlobalManager.rand ( 1.0f, 2.5f );
                    StartCoroutine ( Circle ( cnt, R ) );
                    break;
                case 8:
                    // Shit
                    y = GlobalManager.rand ( 3.0f, 6.0f );
                    cnt = 2 * GlobalManager.rand ( 2, 6 );
                    StartCoroutine ( Shit ( y, cnt ) );
                    break;
                case 9:
                    // ZigZag
                    y = GlobalManager.rand ( 3.0f, 4.0f );
                    cnt = 5 * GlobalManager.rand ( 2, 6 );
                    StartCoroutine ( ZigZag ( y, cnt ) );
                    break;
                default:
                    cnt = GlobalManager.rand ( 4, 8 );
                    StartCoroutine ( Shit ( 3.0f, 2 * cnt ) );
                    break;
            }
        }
    }

    IEnumerator TrowBeans_Descending ( float y, int cnt ) {
        listOfBeans.Add ( GetBean ( y ) );
        yield return new WaitForSeconds ( 0.15f );
        y -= ydif;
        cnt--;
        if ( cnt > 0 && y > minY )
            StartCoroutine ( TrowBeans_Descending ( y, cnt ) );
        else {
            StartCoroutine ( Release ( ) );
        }
    }

    IEnumerator TrowBeans_Ascending ( float y, int cnt ) {
        listOfBeans.Add ( GetBean ( y ) );
        yield return new WaitForSeconds ( 0.15f );
        y += ydif;
        cnt--;
        if ( cnt > 0 && y < Y_MAX )
            StartCoroutine ( TrowBeans_Ascending ( y, cnt ) );
        else {
            StartCoroutine ( Release ( ) );
        }
    }

    IEnumerator TrowBeans_Horizontal ( float y, int cnt ) {
        listOfBeans.Add ( GetBean ( y ) );
        yield return new WaitForSeconds ( xdif );
        cnt--;
        if ( cnt > 0 )
            StartCoroutine ( TrowBeans_Horizontal ( y, cnt ) );
        else
            StartCoroutine ( Release ( 1.0f ) );
        yield return new WaitForSeconds ( 0 );
    }

    IEnumerator TrowBeans_Vertical ( float y, int cnt ) {
        listOfBeans.Add ( GetBean ( y ) );
        //yield return new WaitForSeconds ( ydif );
        cnt--;
        y += ydif;
        if ( cnt > 0 )
            StartCoroutine ( TrowBeans_Vertical ( y, cnt ) );
        else
            StartCoroutine ( Release ( 0.5f ) );
        yield return new WaitForSeconds ( 0f );
    }

    IEnumerator Circle ( int cnt, float R ) {
        Vector2 center = new Vector2 ( 18f, GlobalManager.rand ( 3.5f, 5f ) );
        Vector2[ ] pos = new Vector2[ cnt ];
        float angle = 0, phi = 2 * Mathf.PI / cnt;
        for ( int i = 0; i < cnt; i++ ) {
            float c = ( float ) Mathf.Cos ( angle ), s = ( float ) Mathf.Sin ( angle );
            listOfBeans.Add ( GetBean ( center + new Vector2 ( c * R, s * R ) ) );
            angle += phi;
        }
        yield return new WaitForSeconds ( 0f );
        StartCoroutine ( Release (2f ) );
    }
    IEnumerator ZigZag ( float y, int cnt ) {
        bool up = true;
        for ( int i = 1; i <= cnt; i++ ) {
            if ( up ) {
                while ( i % 5 != 0 ) {
                    listOfBeans.Add ( GetBean ( y ) );
                    y += ydif;
                    yield return new WaitForSeconds ( 0.1f );
                    i++;
                }
                listOfBeans.Add ( GetBean ( y ) );
                yield return new WaitForSeconds ( 0.1f );
            } else {
                while ( i % 5 != 0 ) {
                    y -= ydif;
                    listOfBeans.Add ( GetBean ( y ) );
                    yield return new WaitForSeconds ( 0.1f );
                    i++;
                }

                yield return new WaitForSeconds ( 0.1f );
            }

            up = !up;
        }
        StartCoroutine ( Release ( ) );
    }
    IEnumerator Shit ( float y, int cnt ) {
        bool up = true;
        for ( int i = 0; i < cnt; i++ ) {
            listOfBeans.Add ( GetBean ( y ) );
            if ( up )
                y += ydif;
            else
                y -= ydif;
            yield return new WaitForSeconds ( 0.1f );
            up = !up;
        }
        StartCoroutine ( Release ( ) );
    }
    IEnumerator Release ( float time = 1f ) {
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
