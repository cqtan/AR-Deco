using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
  [SerializeField] private ArGestureManager gesture;
  [SerializeField] private GameObject parentObj;
 // [SerializeField] private GameObject[] furnitureMarkers;
  [SerializeField] private GameObject[] furnitures;

  private bool isTriggered;
  private GameObject go;

  void Start()
  {

  }

  void Update() { }

  // TODO: Clean mess...
  public void OnTriggerStay(Collider other)
  {
    if (gesture.LeftGrabStrength == 1 || gesture.RightGrabStrength == 1)
    {
      if (go == null)
      {
        for (int i = 0; i < furnitures.Length; i++)
        {
          if (IsVuMarker(other) == true)
          {
            if (other.gameObject.name.Substring(2) == furnitures[i].name)
            {
              go = Instantiate(furnitures[i], parentObj.transform);
              Debug.Log("Furniture CREATED!");
            }
          } 
          else if (other.gameObject.name.Substring(0, other.gameObject.name.Length - 7) == furnitures[i].name)
          {
            // For grabbing objects that are already placed in the scene, e.g. "(clone)sofa".            
            go = other.gameObject;
            Debug.Log("Furniture found!");
          }
        }
      }
      if (go != null)
      {
        go.transform.position = this.transform.position;
      }
    }
  }

  public bool IsVuMarker(Collider col) 
  {
    string name = col.name;
    if (name.IndexOf("Vu") > -1) return true;
    else return false;
  }


  // Instantiate only with the VuMarker, instantiated objects should not be 'duplicateable'. 
  public void OnTriggerEnter(Collider other)
  {
    Debug.Log("ENTERED!");
    //if (gesture.leftgrabstrength == 1 || gesture.rightgrabstrength == 1)
    //{
    //  debug.log("enter: grab detected");

    //  gameobject go = null;
    //  for (int i = 0; i < furnitures.length; i++)
    //  {
    //    // get collider name and take remove suffix/postfix
    //    if (other.gameobject.name == furnitures[i].name)
    //    {
    //      go = instantiate(furnitures[i], parentobj.transform);
    //      debug.log("furniture created!");
    //    }
    //  }
    //  while (go != null)
    //  {
    //    if (gesture.leftgrabstrength == 0 || gesture.rightgrabstrength == 0)
    //    {
    //      go.transform.parent = parentobj.transform;
    //    }
    //  }

    //}
  }

  public void OnTriggerExit(Collider other)
  {
    if (go != null) go = null;
    Debug.Log("EXIT = Collider name: " + other.gameObject.name);
  }
}
