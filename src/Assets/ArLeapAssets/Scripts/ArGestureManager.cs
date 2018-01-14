using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coloreality;
using Coloreality.LeapWrapper;

/// <summary>
/// Manages all leap motion values needed for the implemented gestures. Acts
/// very much like an Observable in the sence that if provides all Observers
/// data about leap motion.
/// </summary>
public class ArGestureManager : MonoBehaviour {
  
  ColorealityManager cManager;
  public float LeftGrabStrength { get; private set; }
  public float RightGrabStrength { get; private set; }
  public float LeftGrabAngle { get; private set; }
  public float RightGrabAngle { get; private set; }
  public bool LeftIndexPinch { get; private set; }
  public bool RightIndexPinch { get; private set; }
  public int HandCount { get; private set; }

  [SerializeField] private GameObject leftThumb;
  [SerializeField] private GameObject leftIndex;
  [SerializeField] private GameObject rightThumb;
  [SerializeField] private GameObject rightIndex;

  private List<LeapHand> hands;

  void Start() {
    cManager = ColorealityManager.Instance;
    if (cManager == null) {
      Debug.LogError("Cannot find ColorealityManager Instance.");
      enabled = false; // disables component
    }
  }

  void Update() {
    if (cManager.Leap.Data != null) {
      hands = cManager.Leap.Data.frame.Hands;
      HandCount = hands.Count;
      if (HandCount > 0) {
        GetGrabValues(hands);
        CheckPinching(hands);
      } else {
        ResetAllValues();
      }
    }
  }

  private void ResetAllValues() {
    LeftGrabAngle = 0.0f;
    LeftGrabStrength = 0.0f;
    RightGrabAngle = 0.0f;
    RightGrabStrength = 0.0f;
    LeftIndexPinch = false;
    RightIndexPinch = false;
  }

  /// <summary>
  /// Reads the grab gesture values from the Leap and assigns them to member 
  /// variables depending if a left or right hands is visible.Also sets values 
  /// to 0 if the hand is not visible.
  /// </summary>
  /// <param name="hands">The hand object created by Leap</param>
  private void GetGrabValues(List<LeapHand> hands) {
    foreach (LeapHand hand in hands) {
      if (hand.IsLeft) {
        LeftGrabStrength = hand.GrabStrength;
        LeftGrabAngle = hand.GrabAngle;

        if (HandCount == 1) {
          RightGrabAngle = 0.0f;
          RightGrabStrength = 0.0f;
        }
      }

      if (hand.IsRight) {
        RightGrabStrength = hand.GrabStrength;
        RightGrabAngle = hand.GrabAngle;

        if (HandCount == 1) {
          LeftGrabAngle = 0.0f;
          LeftGrabStrength = 0.0f;
        }
      }
    }
  }

  /// <summary>
  /// Checks if either hand is performing a pinch with the index finger.
  /// </summary>
  /// <param name="hands">Array of hands detected by the Leap.</param>
  private void CheckPinching(List<LeapHand> hands) {
    foreach (LeapHand hand in hands) {
      if (hand.IsLeft && LeftGrabAngle < 1.8f) {
        Vector3 thumbPos = leftThumb.transform.position;
        Vector3 indexPos = leftIndex.transform.position;
        float distance = Vector3.Distance(thumbPos, indexPos);
        LeftIndexPinch = distance < 0.03f ? true : false;
      } 

      if (hand.IsRight && RightGrabAngle < 1.8f) {
        Vector3 thumbPos = rightThumb.transform.position;
        Vector3 indexPos = rightIndex.transform.position;
        float distance = Vector3.Distance(thumbPos, indexPos);
        RightIndexPinch = distance < 0.03f ? true : false;
      }
    }
  }



}
