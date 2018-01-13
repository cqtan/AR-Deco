using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchRotation : MonoBehaviour, IGestureFeature {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private GameObject gestureTools;

  private OutlineWithRay outliner; 
  private HandRotation handRotation;
  private Vector3 rotation;
  private GameObject target;

  void Start () {
    outliner = gestureTools.GetComponent<OutlineWithRay>();
    handRotation = gestureTools.GetComponent<HandRotation>();
	}
	
	void Update () {
    ManageRotation();
	}



  public bool AppropriateGesture() {
    if (gesture.LeftIndexPinch == true && gesture.RightIndexPinch == true) {
      return false;
    } else {
      return false;
    }
  }

  // TODO: Check if it works!
  private void ManageRotation() {
    rotation = handRotation.CalculateRotation(AppropriateGesture());
    if (outliner.HasCollided) {
      target = outliner.GetRayHit().collider.gameObject;
      target.transform.Rotate(rotation);
    }
  }

}
