using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastOutliner : MonoBehaviour {
	[SerializeField] private Transform rayOrigin;
    [SerializeField] string collidingTagName;
	[SerializeField] private float min = 1.0f;
	[SerializeField] private float max = 1.05f;

	private GameObject lastGo;

	void Start () {}
	
	void Update () {
		RaycastHit hit;
        Vector3 forward = rayOrigin.TransformDirection(Vector3.forward);
		Ray ray = new Ray(rayOrigin.position, forward);

		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider.gameObject.tag == collidingTagName) {
				GameObject go = hit.collider.gameObject;
				StartCoroutine (OutlineLerp (go, max, min, 0.5f));
				lastGo = go;
			} else {
				if (lastGo.GetComponent<MeshRenderer> ().material.GetFloat("_OutlineWidth") == max) {
					StartCoroutine (OutlineLerp (lastGo, max, min, 0.5f));
					lastGo = null;
				}
			}
		}
	}

	private IEnumerator OutlineLerp(GameObject go, float min, float max, float time)
	{
		float elapsedTime = 0;

		while (elapsedTime < time)
		{
			go.GetComponent<MeshRenderer> ().material.SetFloat ("_OutlineWidth", Mathf.Lerp(min, max, (elapsedTime / time)));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
