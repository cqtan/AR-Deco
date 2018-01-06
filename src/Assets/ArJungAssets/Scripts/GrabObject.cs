using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Emulates the action of physically grabbing a furniture and changing its position.
/// 
/// Makes an object the source for grabbing other objects. Attach this to an object 
/// that has a collider and set as 'is Trigger'. The other objects should also have
/// a collider that is not a trigger. When a grab gesture is performed and the
/// two objects collide the furniture object of a VuMarker is either cloned and 
/// attached to the source or an already duplicated object is attached to the source.
/// </summary>
public class GrabObject : MonoBehaviour {
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private GameObject parentObj;
  [SerializeField] private GameObject[] furnitures;

  private GameObject grabbedObject;

  void Update() {}

  /// <summary>
  /// As long as the source and furniture objects collide check if hand is performing
  /// a grab gesture. If so, then attach the furniture to the source. Create a clone
  /// if the furniture comes from a VuMarker before attaching. 
  /// </summary>
  /// <param name="other">The furniture object that the source collides with.</param>
  public void OnTriggerStay(Collider other) {
    if (gesture.LeftGrabStrength == 1 || gesture.RightGrabStrength == 1) {
      if (grabbedObject == null) {
        for (int i = 0; i < furnitures.Length; i++) {
          if (IsVuMarker(other) == true) {
            if (CheckFurnitureExists(other, furnitures[i].name)) {
              grabbedObject = Instantiate(furnitures[i], parentObj.transform);
              //Debug.Log("Furniture CREATED!");
            }
          } else if (CheckCloneExists(other, furnitures[i].name)) {            
            grabbedObject = other.gameObject;
            //Debug.Log("Furniture found!");
          }
        }
      }
      if (grabbedObject != null) {
        grabbedObject.transform.position = this.transform.position;
      }
    }
  }

  /// <summary>
  /// Checks if the VuMarker Furniture is in the list of predefined furnitures.
  /// </summary>
  private bool CheckFurnitureExists (Collider other, string furnitureName) {
    return (other.gameObject.name.Substring(2) == furnitureName) ? true : false;
  }

  /// <summary>
  /// Checks if the cloned furniture is in the list of predefined furnitures.
  /// For grabbing objects that are already placed in the scene, e.g. "(clone)sofa".
  /// </summary>
  private bool CheckCloneExists (Collider other, string furnitureName) {
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

  public void OnTriggerEnter(Collider other) {
    Debug.Log("ENTERED!");
  }

  public void OnTriggerExit(Collider other) {
    if (grabbedObject != null) grabbedObject = null;
    Debug.Log("EXIT = Collider name: " + other.gameObject.name);
  }
}
