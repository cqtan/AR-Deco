using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objects that were targeted by the user with the 'OutlineWithRay' class can 
/// be repositioned around the user with a single handed grab gesture. If the
/// target comes from a VuMarker, then a prefab of that object is instanciated 
/// and grabbed. If it was already instanciated, then that object is grabbed.
/// </summary>
public class GrabOutlined : MonoBehaviour, IGestureFeature {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private OutlineWithRay outliner;
  [SerializeField] private GameObject parentObject;
  [SerializeField] private Transform lookAtTarget;

  private GameObject grabbedObject;
  private Collider collisionObject;
  private GameObject grabPoint;
  private GameObject[] targetables;

	void Start () {
    if (outliner != null)
      targetables = outliner.GetTargetables();
	}
	
	void Update () {
    if (outliner.HasCollided) {
      collisionObject = outliner.GetRayHit().collider;
      GrabObject();
    } else if (grabbedObject != null) {
      grabbedObject = null;
    }
	}

  private void GrabObject() {
    if (AppropriateGesture()) {
      if (grabbedObject == null) {
        GetGrabbedObject();
        SetGrabPoint();
      } else {
        UpdateObjectPosition(grabbedObject);
      }
    }
  }

  public bool AppropriateGesture() {
    if (gesture.RightGrabStrength > 0.9f && !(gesture.LeftGrabStrength > 0.9f) ||
        gesture.LeftGrabStrength > 0.9f && !(gesture.RightGrabStrength > 0.9f)) {
      return true;
    } else
      return false;
  }

  /// <summary>
  /// Checks whether the target is needs to be instanciated (VuMarker) or 
  /// not (instanciated object).
  /// </summary>
  private void GetGrabbedObject() {
    for (int i = 0; i < targetables.Length; i++) {
      if (IsVuMarker(collisionObject) == true) {
        if (CheckFurnitureExists(collisionObject, targetables[i].name)) {
          grabbedObject = Instantiate(targetables[i], parentObject.transform);
          Debug.Log("Furniture CREATED!");
        }
      } else if (CheckCloneExists(collisionObject, targetables[i].name)) {
        grabbedObject = collisionObject.gameObject;
        Debug.Log("Furniture found!");
      }
    }
  }

  /// <summary>
  /// To help in positioning the object, a 'GrabPoint' is instanciated to anchor
  /// the grabbed targets position to it. GrabPoint's parent on the other hand is
  /// then anchored to the camera itself, meaning that the grabbed object moves
  /// in relation to the user's head motion while performing the gesture.
  /// </summary>
  private void SetGrabPoint() {
    if (grabPoint == null)
      grabPoint = new GameObject("GrabPoint");
    grabPoint.transform.parent = this.transform.parent;
    grabPoint.transform.position = outliner.GetRayHit().collider.transform.position;
  }

  private void UpdateObjectPosition(GameObject go) {
    grabPoint.transform.LookAt(lookAtTarget);
    go.transform.position = grabPoint.transform.position;
  }

  /// <summary>
  /// Checks if the VuMarker Furniture is in the list of predefined furnitures.
  /// </summary>
  private bool CheckFurnitureExists(Collider other, string furnitureName) {
    return (other.gameObject.name.Substring(2) == furnitureName) ? true : false;
  }

  /// <summary>
  /// Checks if the cloned furniture is in the list of predefined furnitures.
  /// For grabbing objects that are already placed in the scene, e.g. "(clone)sofa".
  /// </summary>
  private bool CheckCloneExists(Collider other, string furnitureName) {
    string clone = other.gameObject.name.Substring(0, other.gameObject.name.Length - 7);
    return (clone == furnitureName) ? true : false;
  }

  /// <summary>
  /// Returns true if the collided object <paramref name="col"/> comes from the VuMarker.
  /// </summary>
  /// <returns><c>true</c>, if vu marker was used, <c>false</c> otherwise.</returns>
  /// <param name="col">The collided object</param>
  private bool IsVuMarker(Collider col) {
    string name = col.name;
    if (name.IndexOf("Vu") > -1) return true;
    else return false;
  }
}
