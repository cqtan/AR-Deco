using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coloreality;
using Coloreality.LeapWrapper;


public class ArGestureManager : MonoBehaviour
{
  ColorealityManager cManager;
  [SerializeField] private float leftGrabStrength;
  [SerializeField] private float rightGrabStrength;
  [SerializeField] private float leftGrabAngle;
  [SerializeField] private float rightGrabAngle;
  [SerializeField] private int handCount;

  private List<LeapHand> hands;

  void Start()
  {
    cManager = ColorealityManager.Instance;
    if (cManager == null)
    {
      Debug.LogError("Cannot find ColorealityManager Instance.");
      enabled = false; // disables component
    }
  }

  void Update()
  {
    if (cManager.Leap.Data != null)
    {
      List<LeapHand> hands = cManager.Leap.Data.frame.Hands;
      handCount = hands.Count;
      GetGrabValues(hands);
    }
  }

  private void ResetAllValues()
  {
    leftGrabAngle = 0.0f;
    leftGrabStrength = 0.0f;
    rightGrabAngle = 0.0f;
    rightGrabStrength = 0.0f;
    handCount = 0;
  }

  private void GetHands()
  {

  }

  /// <summary>
  /// Reads the grab gesture values from the Leap and assigns them to member variables depending 
  /// if a left or right hands is visible.Also sets values to 0 if the hand is not visible.
  /// </summary>
  /// <param name="hands">The hand object created by Leap</param>
  private void GetGrabValues(List<LeapHand> hands)
  {
    if (hands.Count == 0)
    {
      leftGrabAngle = 0.0f;
      leftGrabStrength = 0.0f;
      rightGrabAngle = 0.0f;
      rightGrabStrength = 0.0f;
    }

    foreach (LeapHand hand in hands)
    {
      if (hand.IsLeft)
      {
        leftGrabStrength = hand.GrabStrength;
        leftGrabAngle = hand.GrabAngle;
        
        if (hands.Count == 1)
        {
          rightGrabAngle = 0.0f;
          rightGrabStrength = 0.0f;
        }
      }

      if (hand.IsRight)
      {
        rightGrabStrength = hand.GrabStrength;
        rightGrabAngle = hand.GrabAngle;

        if (hands.Count == 1)
        {
          leftGrabAngle = 0.0f;
          leftGrabStrength = 0.0f;
        }
      }
    }
  }
}
