using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchDistance : MonoBehaviour {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private OutlineWithRay outliner;
  [SerializeField] private float distanceModifier;

  private GameObject grabbedObject;
  private float distanceDifference;

	void Start () {}
	
	void Update () {
    if (SearchGrabbedObject()) {
      ManageDistance();
    }
	}

  private bool SearchGrabbedObject() {
    if (GameObject.Find("GrabPoint") != null) {
      grabbedObject = GameObject.Find("GrabPoint");
      return true;
    } else {
      return false;
    }
  }

  private bool AppropriateGestures() {
    if ((gesture.RightIndexPinch == true && gesture.LeftGrabStrength > 0.9f) || 
        (gesture.LeftIndexPinch == true && gesture.RightGrabStrength > 0.9f)) {
      return true;
    } else
      return false;
  }

  private void ManageDistance() {
    distanceDifference = gesture.CalculateHandDistance(AppropriateGestures(), distanceModifier);
    grabbedObject.transform.position = new Vector3(0f, distanceDifference, 0f);
  }

}
