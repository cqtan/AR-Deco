using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
	private bool camAvailable;

	// Use this for initialization
	void Start ()
	{
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0) {
			Debug.Log ("no camera detected");
			camAvailable = false;
			return;
		}
		Input.gyro.enabled = true;
		camAvailable = true;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!camAvailable) {
			return;
		}
		Quaternion cameraRotation = new Quaternion (Input.gyro.attitude.x, Input.gyro.attitude.y, 
			                            -Input.gyro.attitude.z, -Input.gyro.attitude.w);
		this.transform.localRotation = cameraRotation;
	}
}
