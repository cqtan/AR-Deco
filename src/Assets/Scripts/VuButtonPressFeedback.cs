using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuButtonPressFeedback : MonoBehaviour, IVirtualButtonEventHandler {

	public VirtualButtonBehaviour VirtualButton;

	void Start () {
		VirtualButton.RegisterEventHandler (this);
	}

	public void OnButtonPressed	(VirtualButtonBehaviour vb)	 {
		Debug.Log ("Button Pressed");
	}

	public void OnButtonReleased (VirtualButtonBehaviour vb) {
		Debug.Log ("Button Released");
	}

}
