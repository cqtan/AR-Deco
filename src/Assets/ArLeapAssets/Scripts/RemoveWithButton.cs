using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// Remove objects from the scene by making the VuMarker for the trashcan 
/// visible and pressing (or covering) circular the VuButtonbelow tehe VuMarker.
/// The object that was targeted will then be deleted.
/// </summary>
public class RemoveWithButton : MonoBehaviour, IVirtualButtonEventHandler {

  [SerializeField] private GameObject trashcan;
  [SerializeField] private GameObject gestureTools;
  [SerializeField] private VirtualButtonBehaviour virtualButton;
  private OutlineWithRay outliner;

	void Start () {
    outliner = gestureTools.GetComponent<OutlineWithRay>();
    virtualButton.RegisterEventHandler(this);
	}
	
	void Update () {}

  private bool TrashIsVisible() {
    if (trashcan != null) {
      return trashcan.GetComponent<MeshRenderer>().enabled;
    } else
      return false;
  }

  public void OnButtonPressed(VirtualButtonBehaviour vb) {
    if (outliner.HasCollided && TrashIsVisible()) {
      Destroy(outliner.GetRayHit().collider.gameObject);
      Debug.Log("Destroyed!");
    }
  }

  public void OnButtonReleased(VirtualButtonBehaviour vb) {}
}
