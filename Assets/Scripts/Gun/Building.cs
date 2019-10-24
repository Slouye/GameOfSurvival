using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    private void OnEnable()
    {
        InputManager.Instance.IsBuild = true;
    }

    private void OnDisable()
    {
        InputManager.Instance.IsBuild = false;
        BuildPanelController.Instance.SetLeftKeyNull();
        BuildPanelController.Instance.ResetUI();
    }
}
