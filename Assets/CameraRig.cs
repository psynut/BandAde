using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    private Camera[] cameras;

    private void Awake() {
        cameras = GetComponentsInChildren<Camera>();
        ActivateCamera(0);
    }

    public void ActivateCamera(int cameraNumber) {
        foreach(Camera m_camera in cameras) {
            m_camera.enabled = false;
        }
        cameras[cameraNumber].enabled = true;
    }
}
