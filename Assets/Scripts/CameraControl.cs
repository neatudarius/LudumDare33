using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    //private Vector3 positionAmplitude;
    private Vector3 positionPeriod;
    private float shakeDuration;
    private float shakeTimeLeft;

    private Vector3 basePosition;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    
    //aux var
    private Vector3 positionDamp;

	void Start () {
        basePosition =  targetPosition = initialPosition = transform.position;
        
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Z)) {
            Shake(new Vector3(0, 1f, 0), new Vector3(0, 0.05f, 0), 0.5f);
        }

        Vector3 currentPosition;
        currentPosition.x = Mathf.Lerp(initialPosition.x, targetPosition.x, positionDamp.x);
        currentPosition.y = Mathf.Lerp(initialPosition.y, targetPosition.y, positionDamp.y);
        currentPosition.z = Mathf.Lerp(initialPosition.z, targetPosition.z, positionDamp.z);

        transform.position = currentPosition;

        if (positionDamp.x < 1)
            positionDamp.x += Time.deltaTime / positionPeriod.x;
        if (positionDamp.y < 1)
            positionDamp.y += Time.deltaTime / positionPeriod.y;
        if (positionDamp.z < 1)
            positionDamp.z += Time.deltaTime / positionPeriod.z;

        if (shakeTimeLeft > 0.0f)
            shakeTimeLeft -= Time.deltaTime / shakeDuration;
        else
            targetPosition = basePosition;
	}

    void Shake(Vector3 amplitude, Vector3 inverseFrequency, float duration) {

        if (shakeTimeLeft > 0.1f)
            return;

        shakeTimeLeft = shakeDuration = duration;
        positionPeriod = inverseFrequency;

        StartCoroutine(ChangeTarget("x", initialPosition.x, amplitude.x, inverseFrequency.x));
        StartCoroutine(ChangeTarget("y", initialPosition.y, amplitude.y, inverseFrequency.y));
        StartCoroutine(ChangeTarget("z", initialPosition.z, amplitude.z, inverseFrequency.z));
    }

    IEnumerator ChangeTarget(string axis, float initial, float amplitude, float period) {
        float newValue = initial + Random.Range(-amplitude, +amplitude);

        if (axis == "x") {
            initialPosition.x = transform.position.x;
            targetPosition.x = newValue;
            positionDamp.x = 0;
        }
        else if (axis == "y") {
            initialPosition.y = transform.position.y;
            targetPosition.y = newValue;
            positionDamp.y = 0;
        }

        else if (axis == "z") {
            initialPosition.z = transform.position.z;
            targetPosition.z = newValue;
            positionDamp.z = 0;
        }
        
        yield return new WaitForSeconds(period);
        if (shakeTimeLeft > 0.0f)
            StartCoroutine(ChangeTarget(axis, initial, amplitude/2, period));
    }
}
