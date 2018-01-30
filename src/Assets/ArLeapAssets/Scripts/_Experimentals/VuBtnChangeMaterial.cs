using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuBtnChangeMaterial : MonoBehaviour, IVirtualButtonEventHandler
{

  public VirtualButtonBehaviour VirtualButton;
  public GameObject Furniture;

  private MeshRenderer[] furnitureChildren;
  public Material matBlue;
  public Material matYellow;

  void Start()
  {
    VirtualButton.RegisterEventHandler(this);
    furnitureChildren = Furniture.GetComponentsInChildren<MeshRenderer>();

    //matBlue = Resources.Load("MyMats/cloth_1", typeof(Material)) as Material;
    //matYellow = Resources.Load("MyMats/cloth_2", typeof(Material)) as Material;

    if (matBlue != null && matYellow != null)
      Debug.Log("Mats loaded successfully!!!");
    else
      Debug.Log("UH OH!");
  }

  public void OnButtonPressed(VirtualButtonBehaviour vb)
  {
    Material newMat;

    if (furnitureChildren != null)
    {
      if (furnitureChildren[0].sharedMaterial == matBlue)
        newMat = matYellow;
      else
        newMat = matBlue;

      foreach (MeshRenderer child in furnitureChildren)
      {
        child.material = newMat;
      }
    }
  }

  public void OnButtonReleased(VirtualButtonBehaviour vb)
  {

  }

}
