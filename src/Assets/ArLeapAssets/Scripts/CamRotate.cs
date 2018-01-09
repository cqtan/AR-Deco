using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the main camera according to the rotation of the physical mobile
/// device itself.
/// </summary>
public class CamRotate : MonoBehaviour {
  private bool camAvailable;

  void Start() {
    WebCamDevice[] devices = WebCamTexture.devices;

    if (devices.Length == 0) {
      Debug.Log("no camera detected");
      camAvailable = false;
      return;
    }
    Input.gyro.enabled = true;
    camAvailable = true;

  }

  void Update() {
    if (!camAvailable) return;

    Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, 
                                               Input.gyro.attitude.y,
                                               -Input.gyro.attitude.z, 
                                               -Input.gyro.attitude.w);
    this.transform.rotation = cameraRotation;
  }
}
