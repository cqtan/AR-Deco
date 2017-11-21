using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRenderer : MonoBehaviour {
	private bool camAvailable;
	private WebCamTexture backCam;

	public GameObject camPlane;

	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0) {
			Debug.Log ("no camera detected");
			camAvailable = false;
			return;
		}

		for (int i = 0; i < devices.Length; i++) {
			if (!devices[i].isFrontFacing) {
				GameObject cameraParent = new GameObject ("camParent");
				cameraParent.transform.position = this.transform.position;
				this.transform.parent = cameraParent.transform;
				cameraParent.transform.Rotate (Vector3.right, 90);
				backCam = new WebCamTexture (devices[i].name, Screen.width, Screen.height);
			}
		}
		Input.gyro.enabled = true;

		if (backCam == null) {
			Debug.Log ("Unable to find back camera");
			return;
		}
			
		camPlane.GetComponent<MeshRenderer> ().material.mainTexture = backCam;
		backCam.Play ();

		camAvailable = true;
	}

	// Update is called once per frame
	void Update () {
		if (!camAvailable) {
			return;
		}
		Quaternion cameraRotation = new Quaternion (Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
		this.transform.localRotation = cameraRotation;
	}
}
