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
            int rand = 5;//
            GlobalManager.rand ( 1, 5 );
            switch ( rand ) {
                case 1:
                    //line 
                    defaultY = GlobalManager.rand ( 1.0f, 3.0f );
                    StartCoroutine ( TrowBeans_LINE ( GlobalManager.rand ( 3, 5 ) ) );
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
                case 4:
                    // Circle
                    StartCoroutine ( Circle4 ( GlobalManager.rand ( 1.0f, 2.5f ) ) );
                    break;
                case 5:
                    // Circle
                    StartCoroutine ( Circle ( GlobalManager.rand ( 1.0f, 2.5f ) ) );
                    break;
                default:
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
            int rand = GlobalManager.rand ( 1, 2 );
            if ( rand % 2 == 0 )
                StartCoroutine ( TrowBeans_Ascending ( GlobalManager.rand ( 1, 5 ) ) );
            else
                StartCoroutine ( Release ( ) );
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
            int rand = GlobalManager.rand ( 1, 2 );
            if ( rand % 2 == 0 )
                StartCoroutine ( TrowBeans_Descending ( GlobalManager.rand ( 1, 5 ) ) );
            else
                StartCoroutine ( Release ( ) );
        }
    }

    IEnumerator TrowBeans_LINE ( int cnt ) {
        listOfBeans.Add ( GetBean ( defaultY ) );
        yield return new WaitForSeconds ( delayTime );
        cnt--;
        if ( cnt > 0 )
            StartCoroutine ( TrowBeans_LINE ( cnt ) );
        else
            StartCoroutine ( Release ( ) );
    }

    IEnumerator Circle ( float R ) {
        int cnt = 4;
        float cos = ( float ) Math.Sqrt ( 2f ) / 2f;
        Vector2 center = new Vector2 ( 18f, GlobalManager.rand ( 1f, 3f ) );
        Vector2[ ] pos = new Vector2[ cnt ];
        float angle = 0, phi = 360 / cnt;
        for ( int i = 0; i < cnt; i++ ) {
            Debug.Log ( angle );
            float c = ( float ) Math.Cos ( angle ), s = ( float ) Math.Sin ( angle );
            listOfBeans.Add ( GetBean ( center + new Vector2 ( c * R, s * R ) ) );
            angle += phi;
        }
        yield return new WaitForSeconds ( 0.01f );
        StartCoroutine ( Release ( 2.0f ) );
    }

    IEnumerator Circle4 ( float R ) {
        int cnt = 4;
        Vector2 center = new Vector2 ( 18f, GlobalManager.rand ( 1f, 3f ) );
        Vector2[ ] pos = new Vector2[ cnt ];
        pos[ 0 ] = new Vector2 ( 0, R );
        pos[ 1 ] = new Vector2 ( R, 0 );
        pos[ 2 ] = new Vector2 ( 0, -R );
        pos[ 3 ] = new Vector2 ( -R, 0 );
        for ( int i = 0; i < cnt; i++ ) {
            yield return new WaitForSeconds ( 0.01f );
            listOfBeans.Add ( GetBean ( center + pos[ i ] ) );
        }
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
