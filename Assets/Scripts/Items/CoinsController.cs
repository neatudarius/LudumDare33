using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CoinsController : MonoBehaviour
{
    public GameObject CoinPrefab, ComboDetectorPrefab;
    public Transform coinsParent;
    public float delayTime = 0.5f;
    public float yMin = 1.5f, yMax = 5.5f;
    float yMiddle;
    public float
        deltaX = 1f,
        deltaY = 1f;
    List<GameObject> listOfCoins, combos;
    int ID_COINS, ID_COMBOS;
    bool avaible;
    Vector3 coinScale;
    float
        defaultX,
        minR,
        maxR,
        midR;


    // Use this for initialization
    void Start ( )
    {
        combos = new List<GameObject> ( );
        listOfCoins = new List<GameObject> ( );
        avaible = true;
        ID_COINS = ID_COMBOS = 0;
        coinScale = new Vector3 ( 0.4f, 0.4f, 0.4f );
        yMiddle = ( yMin + yMax ) / 2;
        defaultX = transform.position.x;
        maxR = ( yMax - yMin ) / 2;
        minR = maxR / 4;
        midR = ( minR + maxR ) / 2;
    }

    void Update ( )
    {
        if ( avaible )
        {
            avaible = false;
            //combos.Add ( GetCombo ( ) );
            GenerateCoins ( );
            //combos [ combos.Count-1 ].GetComponent<Combo> ( ).coins = listOfCoins;
            //
        }
    }

    void GenerateCoins ( )
    {
        int rand = GlobalManager.rand ( 1, 16 );
        switch ( rand )
        {
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
                RandomMountain ();
                break;
            case 10:
                // Horizontal line
                ComboLineRight ( );
                break;
            case 11:
                // Vertical line
                ComboLineUp ( );
                break;
            case 12:
                // Rectangle
                ComboRectangle ( );
                break;
            case 13:
                //Triangle
                ComboTriangle ( );
                break;
            case 14:
                //Circle
                ComboCircle ( );
                break;
            case 15:
                //MultiCircles
                ComboMultiCircles ( );
                break;
                break;
            case 16:
                // Mountain
                ComboMountain ( );
                break;

            default:

                break;
        }
        
    }


    void ComboMountain ( )
    {
        int times = GlobalManager.rand ( 1, 2 );
        int cnt = GlobalManager.rand ( 2, 3 );
        DrawMountain ( times, cnt );
    }
    void RandomMountain()
    {
        int times = GlobalManager.rand ( 1, 4 ) * 2 - 1,
            cnt = GlobalManager.rand ( 2, 8 );
        DrawMountain ( times, cnt );
    }
    void DrawMountain (int times, int cnt )
    {
        combos.Add ( GetCombo ( ) );
        float x = defaultX,
            y = 0f;
        int portion = 0;

        // Sense
        bool ascending = GlobalManager.rand ( 0, 1 ) == 0;
        if ( ascending )
        {
            y = yMin + deltaY;
        }
        else
        {
            y = yMax;
        }



        if ( ascending )
            for ( int i = 1; i <= times; i++ )
            {
                if ( ascending )
                {
                    for ( int j = 1; j <= cnt; j++ )
                    {
                        listOfCoins.Add ( GetCoin ( x, y ) );
                        y += deltaY;
                        x += deltaX;
                        if ( y > yMax )
                        {
                            if ( i == 1 )
                                cnt = j;
                            break;
                        }
                    }
                    y -= 2 * deltaY;
                }
                else
                {
                    for ( int j = 2; j < cnt; j++ )
                    {
                        listOfCoins.Add ( GetCoin ( x, y ) );
                        y -= deltaY;
                        x += deltaX;
                        if ( y < yMin )
                        {
                            break;
                        }
                    }
                }
                ascending = !ascending;
            }
        else
            for ( int i = 1; i <= times; i++ )
            {
                if ( ascending )
                {
                    for ( int j = 2; j < cnt; j++ )
                    {
                        listOfCoins.Add ( GetCoin ( x, y ) );
                        y += deltaY;
                        x += deltaX;
                        if ( y > yMax )
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for ( int j = 1; j <= cnt; j++ )
                    {
                        listOfCoins.Add ( GetCoin ( x, y ) );
                        y -= deltaY;
                        x += deltaX;
                        if ( y < yMin )
                        {
                            if ( i == 1 )
                                cnt = j;
                            break;
                        }
                    }
                    y += 2 * deltaY;
                }
                ascending = !ascending;
            }
        SaveCombo ( );
    }

    void RandomMultiLine ( )
    {
        float lowestY = GlobalManager.rand ( yMin, yMax );
        int xCount = GlobalManager.rand ( 3, 8 );
        int yCount = GlobalManager.rand ( 2, 6 );
        DrawMultiline ( lowestY, xCount, yCount );
    }

    void DrawMultiline ( float lowestY, int xCount, int yCount )
    {
        float y = lowestY;
        for ( int i = 1; i <= yCount; i++ )
        {
            DrawLineRight ( y, xCount );
            y += deltaY;
            if ( y > yMax )
                break;
        }
    }

    void RandomTwoParalelLines ( )
    {
        float y1 = ( GlobalManager.rand ( 1, 2 ) == 1 ? yMin : yMiddle );
        float y2 = yMax;
        int XCount = GlobalManager.rand ( 2, 8 );
        DrawLineRight ( y1, XCount );
        DrawLineRight ( y2, XCount );
    }

    void ComboLineRight ( )
    {
        int XCount = GlobalManager.rand ( 2, 4 );
        float y = 0f;
        if ( XCount > 2 )
            y = GlobalManager.rand ( yMin, yMin + (yMiddle- yMin ) / 2 );
        else
            y = GlobalManager.rand ( yMin + ( yMiddle - yMin ) / 2, yMax );
        DrawLineRight ( y, XCount );
    }
    void RandomLineRight ( )
    {
        float y = GlobalManager.rand ( yMin, yMax );
        int XCount = GlobalManager.rand ( 3, 8 );
        DrawLineRight ( y, XCount );
    }

    void DrawLineRight ( float y, float xCount )
    {
        combos.Add ( GetCombo ( ) );
        float x = defaultX;
        for ( int i = 1; i <= xCount; i++ )
        {
            listOfCoins.Add ( GetCoin ( x, y ) );
            x += deltaX;
        }

        
        SaveCombo();
        
    }


    void ComboLineUp ( )
    {
        float lowestY = GlobalManager.rand ( yMin, yMiddle );
        int yCount = GlobalManager.rand ( 2, 6 );
        DrawLineUp ( lowestY, yCount );
    }
    void RandomLineUp ( )
    {
        float lowestY = GlobalManager.rand ( yMin, yMiddle );
        int yCount = GlobalManager.rand ( 3, 8 );
        DrawLineUp ( lowestY, yCount );
    }

    void DrawLineUp ( float lowestY, float yCount )
    {
        combos.Add ( GetCombo ( ) );
        float x = defaultX, y = lowestY;
        for ( int i = 1; i <= yCount; i++ )
        {
            listOfCoins.Add ( GetCoin ( x, y ) );
            y += deltaY;
            if ( y > yMax )
                break;
        }
        
        SaveCombo();
        
    }


    void ComboRectangle ( )
    {
        int xCount = GlobalManager.rand ( 2, 3 ), yCount = 2;
        float
            y1 = GlobalManager.rand ( 1.5f, 2.0f ),
            x1 = defaultX;
        if ( xCount == 2 )
            y1 = GlobalManager.rand ( 1.5f, 4.0f );
        DrawRectangle ( x1, y1, xCount, yCount );
    }
    void RandomRectangle ( )
    {
        int xCount = GlobalManager.rand ( 3, 8 ), yCount = GlobalManager.rand ( 3, 8 );
        float
            y1 = GlobalManager.rand ( 1.5f, 2.0f ),
            x1 = defaultX;
        DrawRectangle ( x1, y1, xCount, yCount );
    }
    void DrawRectangle ( float x1, float y1, int xCount, int yCount )
    {
        combos.Add ( GetCombo ( ) );

        float x2 = x1 + deltaX * ( xCount - 1 ), y2 = y1;
        for ( int i = 1; i <= yCount; i++ )
        {
            listOfCoins.Add ( GetCoin ( x1, y2 ) );
            listOfCoins.Add ( GetCoin ( x2, y2 ) );
            y2 += deltaY;
            if ( y2 > yMax )
            {
                break;
            }
        }
        y2 -= deltaY;
        x1 += deltaX;
        for ( int i = 2; i < xCount; i++ )
        {
            listOfCoins.Add ( GetCoin ( x1, y1 ) );
            listOfCoins.Add ( GetCoin ( x1, y2 ) );
            x1 += deltaX;
        }
        
        SaveCombo();
        
    }


    void ComboTriangle ( )
    {
        int yCount = 2;
        float
            y = GlobalManager.rand ( yMiddle, (yMax+ yMiddle)/2 ),
            x = defaultX;
        DrawTriangle ( x, y, yCount );
    }

    void RandomTriangle ( )
    {
        int yCount = GlobalManager.rand ( 3, 8 );
        float
            y = GlobalManager.rand ( yMiddle, yMax ),
            x = defaultX;
        DrawTriangle ( x, y, yCount );
    }
    void DrawTriangle ( float x, float y, int yCount )
    {
        combos.Add ( GetCombo ( ) );

        for ( int i = 1; i <= yCount; i++ )
        {
            listOfCoins.Add ( GetCoin ( x, y ) );
            for ( int j = 1; j < i; j++ )
            {
                listOfCoins.Add ( GetCoin ( x + deltaX * j, y ) );
                listOfCoins.Add ( GetCoin ( x - deltaX * j, y ) );
            }
            y -= deltaY;
            if ( y < yMin )
            {
                break;
            }
        }
        
        SaveCombo();
        
    }


    void ComboCircle ( )
    {
        int cnt = GlobalManager.rand ( 4, 8 );
        float y = GlobalManager.rand ( yMiddle, yMax );
        float x = defaultX;
        float R = minR;
        combos.Add ( GetCombo ( ) );
        if ( GlobalManager.rand ( 0, 1 ) == 0 )
            listOfCoins.Add ( GetCoin ( x, y ) );
        DrawCircle ( x, y, R, cnt );
        SaveCombo ( );
    }

    void RandomCircle ( )
    {
        int cnt = GlobalManager.rand ( 4, 8 );
        float y = GlobalManager.rand ( yMiddle, yMax );
        float x = defaultX;
        float R = GlobalManager.rand ( minR, maxR );
        combos.Add ( GetCombo ( ) );
        if ( GlobalManager.rand ( 0, 1 ) == 0 )
            listOfCoins.Add ( GetCoin ( x, y ) );
        DrawCircle ( x, y, R, cnt );
        SaveCombo ( );
    }
    void DrawCircle ( float x, float y, float R, int cnt )
    {

        float angle = 0, phi = 2 * Mathf.PI / cnt;
        for ( int i = 1; i <= cnt; i++ )
        {
            float c = ( float ) Mathf.Cos ( angle ), s = ( float ) Mathf.Sin ( angle );
            listOfCoins.Add ( GetCoin ( x + c * R, y + s * R ) );
            angle += phi;
        }

    }


    void ComboMultiCircles ( )
    {
        int cnt = GlobalManager.rand ( 4, 5);
        float y = GlobalManager.rand ( yMiddle, yMax );
        float x = defaultX;
        float R = GlobalManager.rand ( midR/2, midR/2 );
        int circles = 2;

        // Put a coin in center
        combos.Add ( GetCombo ( ) );
        if ( GlobalManager.rand ( 0, 1 ) == 0 )
            listOfCoins.Add ( GetCoin ( x, y ) );
        for ( int i = 1; i <= circles; i++ )
        {
            DrawCircle ( x, y, R, cnt );
            R -= R / circles;
        }
        SaveCombo ( );
    }

    void RandomMultiCircles ( )
    {
        int cnt = GlobalManager.rand ( 4, 8 );
        float y = GlobalManager.rand ( yMiddle, yMax );
        float x = defaultX;
        float R = GlobalManager.rand ( midR, maxR );
        int circles = GlobalManager.rand ( 2, 3 );

        // Put a coin in center
        combos.Add ( GetCombo ( ) );
        if ( GlobalManager.rand ( 0, 1 ) == 0 )
            listOfCoins.Add ( GetCoin ( x, y ) );
        for ( int i = 1; i <= circles; i++ )
        {
            DrawCircle ( x, y, R, cnt );
            R -= R / circles;
        }
        SaveCombo ( );
    }

    public void MakeAvaible ( )
    {
        avaible = true;
    }
    IEnumerator Release ( float time )
    {
        yield return new WaitForSeconds ( time );
        avaible = true;
    }

    public void Remove ( string comboName )
    {
        //Debug.Log ( "fu " + comboName + " " + combos.Count );
        combos.RemoveAll ( item => item.name == comboName );
        //Debug.Log ( "este " + combos.Count );
    }



    GameObject GetCoin ( float x, float y )
    {
        ID_COINS++;
        GameObject newCoin = ( GameObject ) Instantiate ( CoinPrefab, new Vector3 ( x, y, -1 ), Quaternion.identity );
        newCoin.transform.SetParent ( combos [ combos.Count - 1 ].transform );
        newCoin.transform.localScale = coinScale;
        newCoin.gameObject.name = ID_COINS.ToString ( );
        return newCoin;
    }

    GameObject GetCombo ( )
    {
        ID_COMBOS++;
        GameObject newCombo = ( GameObject ) Instantiate ( ComboDetectorPrefab );
        newCombo.gameObject.name = ID_COMBOS.ToString ( );
        newCombo.transform.SetParent ( coinsParent );
        return newCombo;
    }

    void SaveCombo()
    {

        listOfCoins [ listOfCoins.Count - 1 ].transform.GetChild ( 0 ).gameObject.GetComponent<Coin> ( ).last = true;
        combos [ combos.Count - 1 ].GetComponent<Combo> ( ).coins = listOfCoins;
        listOfCoins = new List<GameObject> ( );

    }
}
