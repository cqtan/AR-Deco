using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchRotation : MonoBehaviour, IGestureFeature {
  [SerializeField] private Vector3 logRotation;
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
    if (gesture.LeftGrabStrength > 0.9f && gesture.RightGrabStrength > 0.9f) {
      return true;
    } else {
      return false;
    }
  }

  // TODO: Check if it works!
  private void ManageRotation() {
    rotation = handRotation.CalculateRotation(AppropriateGesture());
    logRotation = rotation;
    if (outliner.HasCollided) {
      if (target == null)
        target = outliner.GetRayHit().collider.gameObject;
      target.transform.Rotate(rotation);
    } else {
      target = null;
    }
  }

}
