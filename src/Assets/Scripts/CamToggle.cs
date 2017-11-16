using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToggle : MonoBehaviour {

	public GameObject VuCamera;
	public GameObject MainCamera;

	void Start () {
		//VuCamera.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {
			
			Touch myTouch = Input.GetTouch (0);

			Touch[] myTouches = Input.touches;
			for (int i = 0; i < Input.touchCount; i++) {
				if (myTouches [i].phase == TouchPhase.Began) {
					if (MainCamera.activeSelf == false) {
						VuCamera.SetActive (false);
						MainCamera.SetActive (true);
					} else {
						MainCamera.SetActive (false);
						VuCamera.SetActive (true);
					}
				}
			}
		}

	}
}
