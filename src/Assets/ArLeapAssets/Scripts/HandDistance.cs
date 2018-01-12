using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDistance : MonoBehaviour{
  
  [SerializeField] private float LogHandDistance;
  [SerializeField] private Transform leftPalm;
  [SerializeField] private Transform rightPalm;
  private float currentDistance;
  private float lastDistance;

  void Start() {}

  void Update() {}

  /// <summary>
  /// Calculates the hand distance only when the appropriate hand gestures were
  /// recognised. A modifier value is added to cope with scaling difference.
  /// </summary>
  /// <returns>The hand distance.</returns>
  public float CalculateHandDistance(bool appropriateGesture, float distanceModifier = 1f) {
    float distanceDifference = 0f;
    if (appropriateGesture) {
      float distance, distanceScaled;

      distance = Vector3.Distance(leftPalm.position, rightPalm.position);
      distanceScaled = (distance * distanceModifier);
      currentDistance = distanceScaled;

      // prevent value jumping
      if (lastDistance == 0.0f)
        lastDistance = currentDistance;

      distanceDifference = (currentDistance - lastDistance);
      lastDistance = currentDistance;

      LogHandDistance = distanceDifference;
      return distanceDifference;
    } else {
      lastDistance = 0.0f;
      return 0f;
    }
  }
}
