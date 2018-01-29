using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour {

  [SerializeField] private GameObject trashcan;
  [SerializeField] private GameObject gestureTools;
  [SerializeField] private float moveSpeed;
  [SerializeField] private float moveTime;
  [SerializeField] float logDistance;

  private OutlineWithRay outliner;
  private GameObject target;
  private Vector3 lastPosition;
  private Vector3 trashPos;
  private bool hasMoved;

	void Start () {
    outliner = gestureTools.GetComponent<OutlineWithRay>();
    trashPos = trashcan.transform.position;
    hasMoved = false;
	}
	
	void Update () {
    if (outliner.HasCollided && TrashIsVisible()) {
      target = GetTarget();
      if (hasMoved == false) {
        StartCoroutine(MoveTarget(target, trashPos, moveSpeed, moveTime));
      }
    } else if (hasMoved){
      StartCoroutine(MoveTarget(target, lastPosition, moveSpeed * 2f, moveTime));
      hasMoved = false;
    }
	}

  private bool TrashIsVisible() {
    if (trashcan != null) {
      return trashcan.GetComponent<MeshRenderer>().enabled;
    } else
      return false;
  }

  private GameObject GetTarget() {
    return outliner.GetRayHit().collider.gameObject;
  }

  private IEnumerator MoveTarget(GameObject t, Vector3 dest, float speed, float time) {
    if (hasMoved == false) {
      lastPosition = t.transform.position;
      hasMoved = true;
    }
    float distance = Vector3.Distance(trashcan.transform.position, 
                                      t.transform.position);
    float elapsedTime = 0;
    float step = (speed / (t.transform.position - dest).magnitude * Time.deltaTime);

    while (elapsedTime <= time) {
      elapsedTime += step;
      t.transform.position = Vector3.Lerp(t.transform.position, dest, elapsedTime);
      yield return new WaitForEndOfFrame();
    }
    //t.transform.position = dest;
    logDistance = distance;
  }
}
