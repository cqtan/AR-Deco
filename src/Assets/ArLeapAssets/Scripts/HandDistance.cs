using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDistance : MonoBehaviour{
  
  public Enums.Gestures CurrentGesture { get; private set; }

  [SerializeField] private float LogHandDistance;
  [SerializeField] private Transform leftPalm;
  [SerializeField] private Transform rightPalm;
  private float currentDistance;
  private float lastDistance;

  void Start() {
    CurrentGesture = Enums.Gestures.None;
  }

  void Update() {}

  /// <summary>
  /// Calculates the hand distance only when the appropriate hand gestures were
  /// recognised. A modifier value is added to cope with scaling difference.
  /// </summary>
  /// <returns>The hand distance.</returns>
  public float CalculateHandDistance(Enums.Gestures gesture,
                                     float distanceModifier = 1f) {
    GestureHasChanged(gesture);

    float distanceDifference = 0f;
    float distance, distanceScaled;

    distance = Vector3.Distance(leftPalm.position, rightPalm.position);
    distanceScaled = (distance * distanceModifier);
    currentDistance = distanceScaled;

    // prevent value jumping
    if (lastDistance == 0f) {
      lastDistance = currentDistance;
      Debug.Log("HERE!");
    }

    distanceDifference = (currentDistance - lastDistance);
    lastDistance = currentDistance;

    LogHandDistance = distanceDifference;
    return distanceDifference;
  }

  private void GestureHasChanged(Enums.Gestures g) {
    if (CurrentGesture != g) {
      CurrentGesture = g;
      lastDistance = 0f;
      Debug.Log("Current Gesture changed!");
    }
  }

  public void ResetLastDistance() {
    lastDistance = 0f;
  }

}
