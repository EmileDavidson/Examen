using System;
using System.Collections;
using System.Collections.Generic;
using Toolbox.Attributes;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{
    public Camera camera;
    
    public bool lookX;
    public bool lookY;
    public bool lookZ;

    private void Awake()
    {
        camera ??= Camera.main;

        if (camera == null)
        {
            Debug.LogError("Camera not found");
            Destroy(this);
        }
    }

    void Update()
    {
        LookAtCamera();
    }

    [Button]
    public void LookAtCamera()
    {
        //rotate to look at camera x,y,z if bool is true
        transform.rotation = Quaternion.Euler(
            lookX ? camera.transform.rotation.eulerAngles.x : transform.rotation.eulerAngles.x,
            lookY ? camera.transform.rotation.eulerAngles.y : transform.rotation.eulerAngles.y,
            lookZ ? camera.transform.rotation.eulerAngles.z : transform.rotation.eulerAngles.z
        );
    }
}