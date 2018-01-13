using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation : MonoBehaviour {

  [SerializeField] private Vector3 logRotationDifference;
  [SerializeField] private Transform leftPalm;
  private Vector3 currentRotation;
  private Vector3 lastRotation;

	void Start () {}
	
	void Update () {}

  public Vector3 CalculateRotation(bool appropriateGesture) {
    Vector3 rotationDfference = new Vector3(0f, 0f, 0f);
    if (appropriateGesture) {

      currentRotation = leftPalm.transform.eulerAngles;

      if (lastRotation == new Vector3(0, 0, 0))
        lastRotation = currentRotation;

      rotationDfference = (currentRotation - lastRotation);
      lastRotation = currentRotation;
      logRotationDifference = rotationDfference;
      return rotationDfference;
    } else {
      lastRotation = new Vector3(0, 0, 0);
      return new Vector3(0, 0, 0);
    }
  }
}
