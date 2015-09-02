using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CoinsController : MonoBehaviour {
    public GameObject CoinPrefab;
    public Transform coinsParent;
    public float delayTime = 0.5f;
    public float yMin = 1.5f, yMax = 5.5f;
    float yMiddle;
    public float
        deltaX = 1f,
        deltaY = 1f;


    List<GameObject> listOfBeans;
    int ID;
    bool avaible;
    Vector3 coinScale;
    float
        defaultX,
        minR,
        maxR,
        midR;


    // Use this for initialization
    void Start ( ) {
        listOfBeans = new List<GameObject> ( );
        avaible = true;
        ID = 0;
        coinScale = new Vector3 ( 0.4f, 0.4f, 0.4f );
        yMiddle = ( yMin + yMax ) / 2;
        defaultX = transform.position.x;
        maxR = ( yMax - yMin ) / 2;
        minR = maxR / 4;
        midR = ( minR + maxR ) / 2;
    }

    void Update ( ) {
        
        if ( avaible ) {
            avaible = false;

            int rand = GlobalManager.rand ( 1,9 );
            switch ( rand ) {
                case 1:
                    // Horizontal line
                    RandomLineRight ( );
                    break;
                case 2:
                    // Vertical line
                    RandomLineUp ( );
                    break;
                case 3:
                    // MultiLine
                    RandomMultiLine ( );
                    break;
                case 4:
                    // Rectangle
                    RandomRectangle ( );
                    break;
                case 5:
                    //Triangle
                    RandomTriangle ( );
                    break;
                case 6:
                    //Circle
                    RandomCircle ( );
                    break;
                case 7:
                    //MultiCircles
                    RandomMultiCircles ( );
                    break;
                case 8:
                    // Two Paralel Lines
                    RandomTwoParalelLines ( );
                    break;
                case 9:
                    // Mountain
                    RandomMountain ( );
                    break;
                case 10:
                    //
                    break;

                default:

                    break;
            }
            WaitUntilAvaible ( 5f );
        }
    }

    void RandomMountain ( ) {
        float x = defaultX,
            y = 0f;
        int portion = 0;
        
        // Sense
        bool ascending = GlobalManager.rand(0,1) == 0;
        if (ascending) {
            y = yMin+deltaY;
        } else {
            y = yMax;
        }


        int times = GlobalManager.rand ( 1, 4 )*2-1,
            cnt = GlobalManager.rand ( 2, 8 );
        if (ascending)
            for ( int i = 1; i <= times; i++ ) {
                if ( ascending ) {
                    for ( int j = 1; j <= cnt; j++ ) {
                         listOfBeans.Add ( GetCoin ( x, y ) );
                        y += deltaY;
                        x += deltaX;
                        if ( y > yMax ) {
                            if (i == 1)
                                cnt = j;
                            break;
                        }
                    }
                    y -= 2 * deltaY;
                } else {
                    for ( int j = 2; j < cnt; j++ ) {
                        listOfBeans.Add ( GetCoin ( x, y ) );
                        y -= deltaY;
                        x += deltaX;
                        if ( y < yMin ) {
                            break;
                        }
                    }
                }
                ascending = !ascending;
            }
        else
            for ( int i = 1; i <= times; i++ ) {
                if ( ascending ) {
                    for ( int j = 2; j < cnt; j++ ) {
                        listOfBeans.Add ( GetCoin ( x, y ) );
                        y += deltaY;
                        x += deltaX;
                        if ( y > yMax ) {
                            break;
                        }
                    }
                } else {
                    for ( int j = 1; j <= cnt; j++ ) {
                        listOfBeans.Add ( GetCoin ( x, y ) );
                        y -= deltaY;
                        x += deltaX;
                        if ( y < yMin ) {
                            if ( i == 1 )
                                cnt = j;
                            break;
                        }
                    }
                    y += 2 * deltaY;
                }
                ascending = !ascending;
            }

    }
    void RandomMultiLine ( ) {
        float lowestY = GlobalManager.rand ( yMin, yMax );
        int xCount = GlobalManager.rand ( 3, 8 );
        int yCount = GlobalManager.rand ( 2, 6 );
        DrawMultiline ( lowestY, xCount, yCount );
    }

    void DrawMultiline (float lowestY, int xCount, int yCount ) {
        float y = lowestY;
        for ( int i = 1; i <= yCount; i++ ) {
            DrawLineRight (y, xCount );
            y += deltaY;
            if ( y > yMax )
                break;
        }
    }

    void RandomTwoParalelLines ( ) {
        float y1 = ( GlobalManager.rand ( 1, 2 ) == 1 ? yMin : yMiddle );
        float y2 = yMax;
        int XCount = GlobalManager.rand ( 2, 8 );
        DrawLineRight ( y1, XCount );
        DrawLineRight ( y2, XCount );
    }
    void RandomLineRight ( ) {
        float y = GlobalManager.rand ( yMin, yMax );
        int XCount = GlobalManager.rand ( 3, 8 );
        DrawLineRight ( y, XCount );
    }
    void DrawLineRight (float y, float xCount ) {
        float  x = defaultX;
        for ( int i = 1; i <= xCount; i++ ) {
            listOfBeans.Add ( GetCoin (  x, y  ) );
            x += deltaX;
        }
    }

    void RandomLineUp ( ) {
        float lowestY = GlobalManager.rand ( yMin, yMiddle );
        int yCount = GlobalManager.rand ( 3, 8 );
        DrawLineUp ( lowestY, yCount );
    }
    void DrawLineUp ( float lowestY, float yCount ) {
        float x = defaultX, y = lowestY;
        for ( int i = 1; i <= yCount; i++ ) {
            listOfBeans.Add ( GetCoin (  x, y  ) );
            y += deltaY;
            if ( y > yMax )
                break;
        }
    }

    void RandomRectangle ( ) {
        int xCount = GlobalManager.rand ( 3, 8 ), yCount = GlobalManager.rand ( 3, 8 );
        float
            y1 = GlobalManager.rand ( 1.5f, 2.0f ),
            x1 = defaultX;
        DrawRectangle ( x1, y1, xCount, yCount );
    }
    void DrawRectangle (float x1, float y1, int xCount, int yCount ) {
        float x2 = x1 + deltaX * ( xCount - 1 ), y2 = y1;
        for ( int i = 1; i <= yCount; i++ ) {
            listOfBeans.Add ( GetCoin ( x1, y2 ) );
            listOfBeans.Add ( GetCoin ( x2, y2 ) );
            y2 += deltaY;
            if ( y2 > yMax ) {
                break;
            }
        }
        y2 -= deltaY;
        x1 += deltaX;
        for ( int i = 2; i < xCount; i++ ) {
            listOfBeans.Add ( GetCoin ( x1, y1 ) );
            listOfBeans.Add ( GetCoin ( x1, y2 ) );
            x1 += deltaX;
        }

    }

    void RandomTriangle ( ) {
        int yCount = GlobalManager.rand ( 3, 8 );
        float
            y = GlobalManager.rand ( yMiddle, yMax ),
            x = defaultX;
        DrawTriangle ( x, y,  yCount );
    }
    void DrawTriangle ( float x, float y, int yCount ) {
        for ( int i = 1; i <= yCount; i++ ) {
            listOfBeans.Add ( GetCoin ( x, y ) );
            for ( int j = 1; j < i; j++ ) {
                listOfBeans.Add ( GetCoin ( x + deltaX*j, y ) );
                listOfBeans.Add ( GetCoin ( x - deltaX * j, y ) );
            }
            y -= deltaY;
            if ( y < yMin ) {
                break;
            }
        }

    }

    void RandomCircle ( ) {
        int cnt = GlobalManager.rand ( 4, 8 );
        float y = GlobalManager.rand ( yMiddle, yMax );
        float x = defaultX;
        float R = GlobalManager.rand ( minR, maxR );
        if ( GlobalManager.rand ( 0, 1 ) == 0 )
            listOfBeans.Add ( GetCoin ( x, y ) );
        DrawCircle ( x, y, R, cnt );
    }
    void DrawCircle ( float x, float y, float R, int cnt ) {
        float angle = 0, phi = 2 * Mathf.PI / cnt;
        for ( int i = 1; i <= cnt; i++ ) {
            float c = ( float ) Mathf.Cos ( angle ), s = ( float ) Mathf.Sin ( angle );
            listOfBeans.Add ( GetCoin ( x + c * R, y + s * R ) );
            angle += phi;
        }
    }

    void RandomMultiCircles ( ) {
        int cnt = GlobalManager.rand ( 4, 8 );
        float y = GlobalManager.rand ( yMiddle, yMax );
        float x = defaultX;
        float R = GlobalManager.rand ( midR, maxR );
        int circles = GlobalManager.rand ( 2, 3 );

        // Put a coin in center
        if ( GlobalManager.rand ( 1, 100 ) % 2 == 0 )
            listOfBeans.Add ( GetCoin ( x, y ) );
        for ( int i = 1; i <= circles; i++ ) {
            DrawCircle ( x, y, R, cnt );
            R -= R / circles;
        }
    }

    void WaitUntilAvaible ( float time = 1f ) {
        StartCoroutine ( Release ( time ) );
    }
    IEnumerator Release ( float time  ) {
        yield return new WaitForSeconds ( time );
        avaible = true;
    }

    public void Remove ( string coinName ) {
        listOfBeans.RemoveAll ( item => item.name == coinName);
    }
    


    GameObject GetCoin (float x,float y ) {
        ID++;
        GameObject newCoin = ( GameObject ) Instantiate ( CoinPrefab, new Vector3 ( x, y, -1 ), Quaternion.identity );
        newCoin.transform.SetParent ( coinsParent );
        newCoin.transform.localScale = coinScale;
        newCoin.gameObject.name = ID.ToString ( );
        return newCoin;
    }

    /*
        GameObject GetCoin ( float y ) {
        ID++;
        GameObject newCoin = ( GameObject ) Instantiate ( CoinPrefab, new Vector3 ( defaultX, y, -1 ), Quaternion.identity );
        newCoin.transform.SetParent ( coinsParent );
        newCoin.transform.localScale = coinScale;
        newCoin.gameObject.name = ID.ToString ( );
        return newCoin;
    }
    */
}
