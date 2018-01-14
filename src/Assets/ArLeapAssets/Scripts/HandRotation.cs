using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation : MonoBehaviour {

  [SerializeField] private Vector3 logRotationDifference;
  [SerializeField] private Transform leftPalm;
  private Vector3 currentRotation;
  private Vector3 lastRotation;

	void Start () {}
	
	void Update () {
    //Debug.Log("Euler: " + leftPalm.rotation.eulerAngles);
  }

  public Vector3 CalculateRotation(bool appropriateGesture) {
    Vector3 rotationDifference = Vector3.zero;
    //if (appropriateGesture) {
    if (Input.GetKey(KeyCode.Y)) {
      currentRotation = leftPalm.eulerAngles;
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
}
