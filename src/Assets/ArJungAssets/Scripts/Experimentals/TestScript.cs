using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	private float min = 1.0f;
	private float max = 2.0f;

	// starting value for the Lerp
	static float t = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			StartCoroutine(OutlineLerp(min, max, 0.5f));
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			StartCoroutine(OutlineLerp(max, min, 0.5f));
		}
	}

	private IEnumerator OutlineLerp(float min, float max, float time)
	{
		float elapsedTime = 0;

		while (elapsedTime < time)
		{
			this.GetComponent<MeshRenderer> ().material.SetFloat ("_OutlineWidth", Mathf.Lerp(min, max, (elapsedTime / time)));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

}
