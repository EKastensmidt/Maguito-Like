using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private float shakeTime, shakePower, shakeFadeTime, shakeRotation;
    [SerializeField] private float rotationMultiplier = 5f;
    public static ScreenShake instance;

    public float RotationMultiplier { get => rotationMultiplier; set => rotationMultiplier = value; }

    void Start()
    {
        instance = this;
    }

    void LateUpdate()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;

            float x = Random.Range(-1f, 1f) * shakePower;
            float y = Random.Range(-1f, 1f) * shakePower;
            transform.position += new Vector3(x, y, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1, 1));
    }

    public void StartShake(float length, float power)
    {
        shakeTime = length;
        shakePower = power;
        shakeFadeTime = power / length;
        shakeRotation = power * rotationMultiplier;
    }
}
