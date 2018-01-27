using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently testing: Rotation
public class TestScript : MonoBehaviour {
  [SerializeField] private GameObject target;
  [SerializeField] private GameObject lookAtThing;
  [SerializeField] private Vector3 logRotationDifference;
  [SerializeField] private Vector3 currentEulers;
  [SerializeField] private float eulerX;
  [SerializeField] private float eulerY;
  [SerializeField] private float eulerZ;
  [SerializeField] private Quaternion quaternion;
  [SerializeField] private Quaternion qDiffLog;

  private Vector3 currentRotation;
  private Vector3 lastRotation;
  float currentAngle = 0f;
  float lastAngle = 0f;

  private Quaternion currentQ;
  private Quaternion lastQ;      

  private float currentX;

	private float min = 1.0f;
	private float max = 2.0f;
  private float moveSpeed = 5f;

	// starting value for the Lerp
	static float t = 0.0f;

	void Start () {
		
	}
	
  // While 'P' is down, turn target in relation to this
	void Update () {
    currentEulers = this.transform.eulerAngles;
    eulerX = this.transform.rotation.eulerAngles.x;
    eulerY = this.transform.eulerAngles.y;
    eulerZ = this.transform.eulerAngles.z;
    quaternion = this.transform.rotation;
    Vector3 eulerDiff = Vector3.zero;
    Quaternion qDiff = Quaternion.Euler(0,0,0); 
    if (Input.GetKey(KeyCode.P)) {
      if (Input.GetKey(KeyCode.O)) {
        //this.transform.rotation = Quaternion.Euler(GetTargetRotation());
        this.transform.LookAt(lookAtThing.transform);
      }
      //currentRotation = this.transform.eulerAngles;
      currentQ = this.transform.rotation;
      if (lastQ == Quaternion.Euler(0,0,0))
        lastQ = currentQ;
      
      qDiff = (currentQ * Quaternion.Inverse(lastQ));
      eulerDiff = CompensateOrientation(qDiff);

      lastQ = currentQ;
      qDiffLog = qDiff;
    } else {
      lastQ = Quaternion.Euler(0, 0, 0);
      qDiff = Quaternion.Euler(0, 0, 0);
    }

    if (eulerDiff != Vector3.zero) {
      target.transform.Rotate(eulerDiff);
    }
	}

  private Vector3 GetTargetRotation() {
    return target.transform.eulerAngles;
  }

  private Vector3 CompensateOrientation(Quaternion q) {
    Vector3 euler;
    euler.x = -q.eulerAngles.z;
    euler.y = q.eulerAngles.y;
    euler.z = q.eulerAngles.x;
    return euler;
  }

  private float GetRotationX() {
    float rotationX;
    float angleX = Mathf.Atan2(this.transform.forward.z,
                              this.transform.forward.x);
    angleX *= Mathf.Rad2Deg;

    if (angleX > 180)
      angleX -= 360;

    currentAngle = angleX;

    if (lastAngle == 0f)
      lastAngle = currentAngle;

    rotationX = (currentAngle - lastAngle);
    lastAngle = currentAngle;
    currentX = rotationX;

    return rotationX;
  }

	private IEnumerator OutlineLerp(float min, float max, float time) {
		float elapsedTime = 0;

		while (elapsedTime < time) {
			this.GetComponent<MeshRenderer> ().material.SetFloat ("_OutlineWidth", Mathf.Lerp(min, max, (elapsedTime / time)));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

}
