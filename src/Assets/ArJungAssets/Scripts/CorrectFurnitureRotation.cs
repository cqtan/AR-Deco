using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CorrectFurnitureRotation : MonoBehaviour
{
	void Start ()
	{
		if (Application.platform == RuntimePlatform.WindowsEditor ||
			Application.platform == RuntimePlatform.OSXEditor)
			Debug.Log("Running on WINDOWS or OSX");
		else 
			this.transform.rotation = Quaternion.Euler (-90f, 90f, 0f);
	}
}
