using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All features using gestures must implement a way to recognise the gestures
/// needed to trigger a specific functionality.
/// </summary>
public interface IGestureFeature {
  bool AppropriateGesture();
}
