using UnityEngine;
using System.Collections;

public class CoinScroller : MonoBehaviour {
    void Update ( ) {
        transform.position = transform.position + GlobalManager.difficultyMultiplier * GlobalManager.foregroundSpeed * Vector3.left * Time.deltaTime;
    }
}
