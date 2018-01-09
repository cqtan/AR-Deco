using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchDistance : MonoBehaviour {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private OutlineWithRay outliner;

  private GameObject grabbedObject;

	void Start () {
		
  }
	
	void Update () {
    if (SearchGrabbedObject()) {
      if (AppropriateGestures()) {
        ManageDistance();
      }
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
    
  }

}
