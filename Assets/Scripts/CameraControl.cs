using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    //position shake
    private Vector3 positionPeriod;
    private float shakeDuration;
    private float shakeTimeLeft;

    private Vector3 basePosition;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    
    //aux var
    private Vector3 positionDamp;

    //tilt shake
    private float tiltAmplitude;
    
    private float baseTilt;
    private float initialTilt;
    private float targetTilt;

    private float tiltPeriod;
    private float tiltDuration;
    private float tiltTimeLeft;

    private float tiltDamp;

	void Start () {
        basePosition =  targetPosition = initialPosition = transform.position;
        baseTilt = targetTilt = initialTilt = transform.rotation.z;
	}
	
	void Update () {

        /*
        if (Input.GetKeyDown(KeyCode.Z)) {
            Shake(new Vector3(0, 1f, 0), new Vector3(0, 0.05f, 0), 0.5f);
        }
        */
        Vector3 currentPosition;
        float currentTilt;

        //position shake
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
            initialPosition = targetPosition = basePosition;
        
        //tilt shake 
        currentTilt = Mathf.LerpAngle(initialTilt, targetTilt, tiltDamp);

        transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                currentTilt
            );

        if (tiltDamp < 1)
            tiltDamp += Time.deltaTime / tiltPeriod;

        if (tiltTimeLeft > 0.0f)
            tiltTimeLeft -= Time.deltaTime / tiltDuration;
        else
            initialTilt = targetTilt = baseTilt;
        
	}

    public void PositionShake(Vector3 amplitude, Vector3 frecvency, float duration) {

        if (shakeTimeLeft > 0.1f)
            return;

        shakeTimeLeft = shakeDuration = duration;
        positionPeriod.x = 1.0f / frecvency.x;
        positionPeriod.y = 1.0f / frecvency.y;
        positionPeriod.z = 1.0f / frecvency.z;

        StartCoroutine(ChangeTarget("x", initialPosition.x, amplitude.x, positionPeriod.x));
        StartCoroutine(ChangeTarget("y", initialPosition.y, amplitude.y, positionPeriod.y));
        StartCoroutine(ChangeTarget("z", initialPosition.z, amplitude.z, positionPeriod.z));
    }

    public void TiltShake(float amplitude, float frecvency, float duration) {

        if (tiltTimeLeft > 0.1f)
            return;

        tiltTimeLeft = tiltDuration = duration;
        tiltPeriod = 1.0f / frecvency;

        StartCoroutine(ChangeTilt(initialTilt, amplitude, tiltPeriod));
    }

    IEnumerator ChangeTilt(float initial, float amplitude, float period) {
        float newValue;
        initialTilt = transform.localEulerAngles.z;

        do {
            newValue = initial + Random.Range(-amplitude, +amplitude);
        } while (Mathf.Abs(targetTilt - newValue) < amplitude / 5);

        targetTilt = newValue;
        tiltDamp = 0;

        yield return new WaitForSeconds(period);
        if (tiltTimeLeft > 0.1f)
            StartCoroutine(ChangeTilt(initial, amplitude, period));
    }

    IEnumerator ChangeTarget(string axis, float initial, float amplitude, float period) {
        float newValue;

        if (axis == "x") {
            initialPosition.x = transform.position.x;
            do{
                newValue = initial + Random.Range(-amplitude, +amplitude);
            } while ( Mathf.Abs(targetPosition.x - newValue) < amplitude/5 );

            targetPosition.x = newValue;
            positionDamp.x = 0;
        }
        else if (axis == "y") {
            initialPosition.y = transform.position.y;
            do {
                newValue = initial + Random.Range(-amplitude, +amplitude);
            } while (Mathf.Abs(targetPosition.y - newValue) < amplitude / 5);

            targetPosition.y = newValue;
            positionDamp.y = 0;
        }

        else if (axis == "z") {
            initialPosition.z = transform.position.z;
            do {
                newValue = initial + Random.Range(-amplitude, +amplitude);
            } while (Mathf.Abs(targetPosition.z - newValue) < amplitude / 5);

            targetPosition.z = newValue;
            positionDamp.z = 0;
        }
        
        yield return new WaitForSeconds(period);
        if (shakeTimeLeft > 0.0f)
            StartCoroutine(ChangeTarget(axis, initial, amplitude/2, period));
    }
}
