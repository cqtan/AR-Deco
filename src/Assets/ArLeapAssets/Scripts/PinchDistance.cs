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
  [SerializeField] private float distanceModifier; // 400
  [SerializeField] private Transform lookAtTarget;
  [SerializeField] private float minDistance; // 1
  [SerializeField] private float maxDistance; // 12
  [SerializeField] private float logDistance;

  private HandDistance handDistance;
  private GameObject target;
  private float distanceDifference;

	void Start () {
    handDistance = gestureTools.GetComponent<HandDistance>();
  }
	
	void Update () {
    if (SearchGrabbedObject() && AppropriateGesture()) {
      ManageDistance(target);
      PlaceWithinConstraints(target);
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
      target = GameObject.Find("GrabPoint");
      return true;
    } else {
      return false;
    }
  }

  public bool AppropriateGesture() {
    if (((gesture.RightIndexPinch == true && gesture.LeftGrabStrength > 0.9f) ||
        (gesture.LeftIndexPinch == true && gesture.RightGrabStrength > 0.9f)) &&
        !(gesture.LeftIndexPinch == true && gesture.RightIndexPinch == true)) {
      return true;
    } else if (handDistance.CurrentGesture == Enums.Gestures.DistanceGesture) {
      handDistance.ResetLastDistance();
      return false;
    } else
      return false;
  }

  /// <summary>
  /// Manages the distance of the grabbed object
  /// </summary>
  private void ManageDistance(GameObject t) {
    distanceDifference = handDistance.CalculateHandDistance(Enums.Gestures.DistanceGesture, 
                                                            distanceModifier);
    t.transform.position += transform.forward * distanceDifference * Time.deltaTime;
  }

  private void PlaceWithinConstraints(GameObject t) {
    float distance = Vector3.Distance(lookAtTarget.transform.position,
                                      t.transform.position);
    logDistance = distance;
    if (distance <= minDistance) {
      t.transform.position += transform.forward * 1.1f * Time.deltaTime;
    } else if (distance >= maxDistance) {
      t.transform.position += transform.forward * -1.1f * Time.deltaTime;
    }
  }
}
