using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// For debugging without Leap Motion
/// </summary>
public class Emulator : MonoBehaviour {

  [SerializeField] private float moveSpeed;
  [SerializeField] private float rotationSpeed;

	void Start () {}
	
	void Update () {
    MoveMe();
    RotateMe();
	}

  private void MoveMe() {
    Vector3 position = this.transform.position;

    if (Input.GetKey(KeyCode.W)) {
      position.y += moveSpeed * Time.deltaTime;
      this.transform.position = position;
    }

    if (Input.GetKey(KeyCode.A)) {
      position.x -= moveSpeed * Time.deltaTime;
      this.transform.position = position;
    }

    if (Input.GetKey(KeyCode.S)) {
      position.y -= moveSpeed * Time.deltaTime;
      this.transform.position = position;
    }

    if (Input.GetKey(KeyCode.D)) {
      position.x += moveSpeed * Time.deltaTime;
      this.transform.position = position;
    }

    if (Input.GetKey(KeyCode.Q)) {
      position.z += moveSpeed * Time.deltaTime;
      this.transform.position = position;
    }

    if (Input.GetKey(KeyCode.E)) {
      position.z -= moveSpeed * Time.deltaTime;
      this.transform.position = position;
    }
  }

  private void RotateMe() {
    if (Input.GetKey(KeyCode.Z))
      this.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

    if (Input.GetKey(KeyCode.U))
      this.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);

    if (Input.GetKey(KeyCode.H))
      this.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

    if (Input.GetKey(KeyCode.J))
      this.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);

    if (Input.GetKey(KeyCode.N))
      this.transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);

    if (Input.GetKey(KeyCode.M))
      this.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
  }
}
