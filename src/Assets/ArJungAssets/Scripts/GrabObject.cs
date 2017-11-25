using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
  [SerializeField] private ArGestureManager gesture;

  void Start()
  {

  }

  void Update()
  {


  }

  public void OnTriggerEnter(Collider other)
  {
    Debug.Log("ENTER = Collider name: " + other.gameObject.name);
    if (other.gameObject.name == "Sofa")
    {
      Debug.Log("Sofa Enter found!");
    }

  }

  public void OnTriggerExit(Collider other)
  {
    Debug.Log("EXIT = Collider name: " + other.gameObject.name);
    if (other.gameObject.name == "Sofa")
    {
      Debug.Log("Sofa Exit found");
    }
  }
}
