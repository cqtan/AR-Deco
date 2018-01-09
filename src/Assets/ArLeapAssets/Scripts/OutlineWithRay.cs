using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates and outline around an Object that the raycast collides with.
/// </summary>
public class OutlineWithRay : MonoBehaviour {
  public bool HasCollided { get; private set; }
  public LineRenderer Line { get; set; }

  [SerializeField] private bool lineIsVisible;
  [SerializeField] private bool rayIsBackwards;
  [SerializeField] private Material MaterialOnHit;
  [SerializeField] private Material MaterialOnNoHit;
  [SerializeField] private Transform rayOrigin;
  [SerializeField] string targetableTagName;
  [SerializeField] private GameObject[] targetables;
  [SerializeField] private float min = 1.0f;
  [SerializeField] private float max = 1.05f;

  private GameObject previousGo;
  private RaycastHit hit;

  void Start () {
    HasCollided = false;
    createLine();
  }
	
	void Update () {
    UpdateLine();

    if (RayIsColliding()) {
      HasCollided = true;
      ManageOutline();
    } else {
      HasCollided = false;
    }
	}

  /// <summary>
  /// Creates a ray from 'rayOrigin' and shoots out in forward direction.
  /// <returns>
  /// True, if ray collides with a collidable object and assigns
  /// collision data to the 'hit' variable.
  /// </returns>
  /// </summary>
  private bool RayIsColliding() {
    Vector3 forward = rayOrigin.TransformDirection((Vector3.forward));
    if (rayIsBackwards)
      forward *= -1;
    Ray ray = new Ray(rayOrigin.position, forward);

    return Physics.Raycast(ray, out hit);
  }

  /// <summary>
  /// Increases and decreases outline of collision object with the ray. 
  /// </summary>
  private void ManageOutline() {
    if (hit.collider.gameObject.tag == targetableTagName) {
      GameObject go = hit.collider.gameObject;
      StartCoroutine(OutlineLerp(go, max, min, 0.5f));
      previousGo = go;
    } else {
      if (previousGo.GetComponent<MeshRenderer>().material.GetFloat("_OutlineWidth") == max) {
        StartCoroutine(OutlineLerp(previousGo, max, min, 0.5f));
        previousGo = null;
      }
    }
  }

  private IEnumerator OutlineLerp(GameObject go, float min, float max, float time) {
    float elapsedTime = 0;
    
    while (elapsedTime < time) {
      go.GetComponent<MeshRenderer>().material.SetFloat("_OutlineWidth", Mathf.Lerp(min, max, (elapsedTime / time)));
      elapsedTime += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
  }

  private void createLine() {
    if (lineIsVisible) {
      Line = rayOrigin.gameObject.GetComponent<LineRenderer>() == null ? 
             rayOrigin.gameObject.AddComponent<LineRenderer>() : 
             rayOrigin.gameObject.GetComponent<LineRenderer>();
      Line.material = MaterialOnNoHit;
      Line.sortingLayerName = "OnTop";
      Line.sortingOrder = 5;
      Line.SetVertexCount(2);
      Line.SetPosition(0, rayOrigin.transform.position);
      Line.SetPosition(1, rayOrigin.transform.forward * 10);
      Line.startWidth = 0.05f;
      Line.endWidth = 0.05f;
      Line.useWorldSpace = true;
    }
  }

  private void UpdateLine() {
    if (lineIsVisible) {
      Line.SetPosition(0, rayOrigin.transform.position);

      if (HasCollided) {
        Line.SetPosition(1, hit.point);
        Line.material = MaterialOnHit;
      } else {
        Line.SetPosition(1, rayOrigin.transform.forward * 10 + transform.position);
        Line.material = MaterialOnNoHit;
      }
    }
  }

  public RaycastHit GetRayHit() { return hit; }

  public GameObject[] GetTargetables() { return targetables; }

}
