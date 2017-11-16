/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ToggleTargetRender : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES

    [SerializeField]
    MeshRenderer[] renderers;
    bool ready = true;
    bool renderVirtualTargets = true;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
#if UNITY_EDITOR
        foreach (MeshRenderer meshRenderer in renderers)
        {
            Destroy(meshRenderer);
        }
#endif
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && ready)
        {
            ready = false;

            renderVirtualTargets = !renderVirtualTargets;

            if (renderVirtualTargets)
            {
                IEnumerable<TrackableBehaviour> behaviours =
                TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();

                foreach (TrackableBehaviour behaviour in behaviours)
                {
                    MeshRenderer meshRenderer = behaviour.gameObject.GetComponent<MeshRenderer>();
                    meshRenderer.enabled = !meshRenderer.enabled;
                }
            }

            ready = true;
        }
#endif
    }

    void LateUpdate()
    {
#if UNITY_ANDROID
        if (!renderVirtualTargets)
        {
            foreach (MeshRenderer meshRenderer in renderers)
            {
                meshRenderer.enabled = false;
            }
        }
#endif
    }

    #endregion // MONOBEHAVIOUR_METHODS
}
