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

  [SerializeField] private bool usingMobile;
  [SerializeField] private Vector3 logRotationDifference;
  [SerializeField] private Transform leftPalm;
  [SerializeField] private Transform rightPalm;
  [SerializeField] private GameObject rotator;
  [SerializeField] private Quaternion qDiffLog;
  private Quaternion currentQ;
  private Quaternion lastQ;

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
    Vector3 eulerDiff = Vector3.zero;
    Quaternion qDiff = Quaternion.Euler(0,0,0); 
    if (appropriateGesture) {
      SetRotator();
      currentQ = rotator.transform.rotation;
      if (lastQ == Quaternion.Euler(0,0,0))
        lastQ = currentQ;
      
      qDiff = (currentQ * Quaternion.Inverse(lastQ));
      eulerDiff = CompensateOrientation(qDiff);
      
      lastQ = currentQ;
      qDiffLog = qDiff;
      return eulerDiff;
    } else {
      lastQ = Quaternion.Euler(0, 0, 0);
      qDiff = Quaternion.Euler(0, 0, 0);
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

  /// <summary>
  /// Compensates the orientation when using a smartphone since it is
  /// then held horizontally.
  /// TODO: Maybe: y -> x / x -> y, switched back
  /// </summary>
  /// <returns>The difference in orientation but compensated.</returns>
  /// <param name="rotationDifference">Difference in rotation.</param>
  private Vector3 CompensateOrientation(Quaternion q) {
    Vector3 euler;
    euler.x = q.eulerAngles.x;
    euler.y = -q.eulerAngles.y; // correct 
    euler.z = -q.eulerAngles.z;
    return euler;
  }





}
