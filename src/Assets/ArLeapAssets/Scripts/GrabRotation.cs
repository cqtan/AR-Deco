using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the appropriate gesture is identified (2 grab gestures), then
/// check if the user is targeting an object. If so, then rotate that
/// object in relation to the position of both hands. Much like rotating
/// an object between both hands.
/// </summary>
public class GrabRotation : MonoBehaviour, IGestureFeature {
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
