using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabOutlined : MonoBehaviour {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private OutlineWithRay outliner;
  [SerializeField] private GameObject parentObject;

  private GameObject grabbedObject;
  private Collider collisionObject;
  private GameObject grabPoint;
  private GameObject[] targetables;

	void Start () {
    if (outliner != null)
      targetables = outliner.GetTargetables();
	}
	
	void Update () {
    MoveArrow();

    if (outliner.HasCollided) {
      collisionObject = outliner.GetRayHit().collider;
      GrabObject();
    } else if (grabbedObject != null) {
      grabbedObject = null;
    }
	}

  private void GrabObject() {
    if (gesture.RightGrabStrength > 0.9f || gesture.LeftGrabStrength > 0.9f) {
    //if (Input.GetKey(KeyCode.G)) {
      if (grabbedObject == null) {
        GetGrabbedObject();
        SetGrabPoint();
      } else {
        UpdateObjectPosition(grabbedObject);
      }
    }
  }

  private void MoveArrow() {
    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
      Vector3 position = this.transform.position;
      position.x--;
      this.transform.position = position;
    }

    if (Input.GetKeyDown(KeyCode.RightArrow)) {
      Vector3 position = this.transform.position;
      position.x++;
      this.transform.position = position;
    }
  }

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

  private void SetGrabPoint() {
    if (grabPoint == null)
      grabPoint = new GameObject("GrabPoint");
    grabPoint.transform.parent = this.transform.parent;
    grabPoint.transform.position = outliner.GetRayHit().collider.transform.position;
  }

  private void UpdateObjectPosition(GameObject go) {
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
