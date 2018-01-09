using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the instatiated furniture objects in the correct orientation when
/// using a mobile device.
/// </summary>
public class CorrectFurnitureRotation : MonoBehaviour {
  void Start() {
    if (Application.platform == RuntimePlatform.WindowsEditor ||
      Application.platform == RuntimePlatform.OSXEditor)
      Debug.Log("Running on WINDOWS or OSX");
    else
      this.transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
  }
}
