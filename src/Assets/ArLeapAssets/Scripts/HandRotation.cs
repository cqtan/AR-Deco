using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the appropriate gesture is identified, it determines the amount of 
/// rotation needed for the grabbed object to rotate. By using a simple object
/// as reference, i.e. the 'rotator' object, the grabbed object then rotates in
/// relation to this rotator object. The rotator object is always set between both
/// leap motion palms and its forward direction always points towards the right
/// palm, giving the user a more intuitive approach.
/// </summary>
public class HandRotation : MonoBehaviour {

  [SerializeField] private Vector3 logRotationDifference;
  [SerializeField] private Transform leftPalm;
  [SerializeField] private Transform rightPalm;
  [SerializeField] private GameObject rotator;
  private Vector3 currentRotation;
  private Vector3 lastRotation;

	void Start () {}
	
	void Update () {}

  /// <summary>
  /// Calculates the amount of rotation the rotator object has made in order for
  /// the grabbed object to rotate in the same amount.
  /// </summary>
  /// <returns>The rotation difference.</returns>
  /// <param name="appropriateGesture">If appropriate gesture identified, 
  /// then <c>true</c></param>
  public Vector3 CalculateRotation(bool appropriateGesture) {
    Vector3 rotationDifference = Vector3.zero;
    if (appropriateGesture) {
      SetRotator();
      currentRotation = rotator.transform.eulerAngles;
      if (lastRotation == Vector3.zero)
        lastRotation = currentRotation;
      rotationDifference = (currentRotation - lastRotation);
      lastRotation = currentRotation;
      logRotationDifference = rotationDifference;
      return rotationDifference;
    } else {
      lastRotation = Vector3.zero;
      return Vector3.zero;
    }
  }

  /// <summary>
  /// Sets the rotator in between both palms and makes its forward direction point 
  /// to the right palm. 
  /// </summary>
  private void SetRotator() {
    if (rotator == null) {
      rotator = new GameObject("Rotator");
      rotator.transform.parent = this.transform.parent;
    }
    Vector3 middlePoint = (leftPalm.position - rightPalm.position) * 0.5f + rightPalm.position;
    rotator.transform.position = middlePoint;
    rotator.transform.LookAt(rightPalm);
  }
}
