using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows an outline of the object that the camera is currently looking at. When
/// facing away, the outline disappears again. The reason for this is to make
/// targeting an object visibly clear for the user so that gestures may be
/// performed on it.
/// 
/// <remarks>
/// The colliding tag name must match the object's tag name and must
/// have the 'OutlineMat' material assigned to its Renderer component. 
/// </remarks>
/// </summary>
public class RaycastOutliner : MonoBehaviour {
  [SerializeField] private Transform rayOrigin;
  [SerializeField] string collidingTagName;
  [SerializeField] private float min = 1.0f;
  [SerializeField] private float max = 1.05f;

  private GameObject lastGo;

  void Start() { }

  void Update() {
    RaycastHit hit;
    Vector3 forward = rayOrigin.TransformDirection(Vector3.forward);
    Ray ray = new Ray(rayOrigin.position, forward);

    if (Physics.Raycast(ray, out hit)) {
      if (hit.collider.gameObject.tag == collidingTagName) {
        GameObject go = hit.collider.gameObject;
        StartCoroutine(OutlineLerp(go, max, min, 0.5f));
        lastGo = go;
      } else {
        if (lastGo.GetComponent<MeshRenderer>().material.GetFloat("_OutlineWidth") == max) {
          StartCoroutine(OutlineLerp(lastGo, max, min, 0.5f));
          lastGo = null;
        }
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
}
