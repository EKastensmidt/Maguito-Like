using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    Vector3 cameraPos;
    void Start()
    {
        cameraPos = transform.position;
    }

    void Update()
    {
        transform.position = cameraPos;
    }
}
