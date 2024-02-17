using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCamera : MonoBehaviour
{
    
    CinemachineVirtualCamera virtualCamera;
    
    private void Start() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera.Follow == null || virtualCamera.LookAt == null)
        {
            virtualCamera.Follow = ShoppingCart.Instance.transform;
            virtualCamera.LookAt = ShoppingCart.Instance.transform;
        }
    }
    
}
