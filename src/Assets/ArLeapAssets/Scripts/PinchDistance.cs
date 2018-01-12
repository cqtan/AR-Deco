using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the distance of a grabbed object by means of the pinch gesture.
/// One hand performs the grab gesture implemented in the GrabOutlined class
/// and the other performs a pinch gesture. While holding these 2 gestures, the
/// distance of the grabbed object changes in relation to the distance of both
/// hands. In this case, the closer the hands get, the further away the object 
/// becomes and vice-versa.
/// </summary>
public class PinchDistance : MonoBehaviour, IGestureFeature {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private GameObject gestureTools;
  [SerializeField] private float distanceModifier;

  private OutlineWithRay outliner;
  private HandDistance handDistance;
  private GameObject grabbedObject;
  private float distanceDifference;

	void Start () {
    outliner = gestureTools.GetComponent<OutlineWithRay>();
    handDistance = gestureTools.GetComponent<HandDistance>();
  }
	
	void Update () {
    if (SearchGrabbedObject()) {
      ManageDistance();
    }
	}

  /// <summary>
  /// Gets a reference of the 'GrabPoint' object to which the grabbed object's
  /// position is anchored to so that the distance may be changed relative to the
  /// user. 
  /// </summary>
  /// <returns><c>true</c>, if grabbed object was found, <c>false</c> otherwise.</returns>
  private bool SearchGrabbedObject() {
    if (GameObject.Find("GrabPoint") != null) {
      grabbedObject = GameObject.Find("GrabPoint");
      return true;
    } else {
      return false;
    }
  }

  public bool AppropriateGesture() {
    if ((gesture.RightIndexPinch == true && gesture.LeftGrabStrength > 0.9f) || 
        (gesture.LeftIndexPinch == true && gesture.RightGrabStrength > 0.9f)) {
      return true;
    } else
      return false;
  }

  /// <summary>
  /// Manages the distance of the grabbed object
  /// </summary>
  private void ManageDistance() {
    distanceDifference = handDistance.CalculateHandDistance(AppropriateGesture(), distanceModifier);
    grabbedObject.transform.position += new Vector3(0f, distanceDifference, 0f);
  }

}
