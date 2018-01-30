using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scales the targeted object when performing the pinch gesture with
/// both hands. The larger the distance of both hands are while 
/// performing this gesture, the larger the object becomes and 
/// vice-versa.
/// </summary>
public class PinchScale : MonoBehaviour, IGestureFeature {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private GameObject gestureTools;
  [SerializeField] private float scaleModifier; // 5f
  [SerializeField] private float minScale; // 0.5f
  [SerializeField] private float maxScale; // 3f
  [SerializeField] private float logScale;

  [SerializeField] private GameObject testTarget;
  public bool usingTestTarget;
  private GameObject target;
  private OutlineWithRay outliner;
  private HandDistance handDistance;
  private float distanceDifference;

  void Start () {
    outliner = gestureTools.GetComponent<OutlineWithRay>();
    handDistance = gestureTools.GetComponent<HandDistance>();
	}
	
	void Update () {
    target = GetTarget();
    if (target != null && AppropriateGesture()) {
      ManageScale(target);
      PlaceWithinConstraints(target);
    }
	}

  private GameObject GetTarget() {
    if (usingTestTarget)
      return testTarget;
    else if (outliner.HasCollided)
      return outliner.GetRayHit().collider.gameObject;
    else
      return null;
  }

  public bool AppropriateGesture() {
    if (gesture.LeftIndexPinch == true && gesture.RightIndexPinch == true &&
        !(gesture.LeftGrabStrength > 0.9f && gesture.RightGrabStrength > 0.9f)) {
      return true;
    } else if (handDistance.CurrentGesture == Enums.Gestures.ScaleGesture) {
      handDistance.ResetLastDistance();
      return false;
    } else
      return false;
  }

  private void ManageScale(GameObject t) {
    distanceDifference = handDistance.CalculateHandDistance(Enums.Gestures.ScaleGesture,
                                                            scaleModifier);
    if (distanceDifference > -1f && distanceDifference < 1f) {
      logScale = distanceDifference;
      t.transform.localScale += new Vector3(distanceDifference,
                                            distanceDifference,
                                            distanceDifference);
    }
  }

  private void PlaceWithinConstraints(GameObject t) {
    Vector3 targetScale = t.transform.localScale;
    if (targetScale.x < minScale)
      targetScale = new Vector3(minScale, minScale, minScale);
    if (targetScale.x > maxScale)
      targetScale = new Vector3(maxScale, maxScale, maxScale);
  }
  
}
