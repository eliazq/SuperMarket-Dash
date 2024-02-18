using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    private void Start() {
        SetPlayerPos();
    }
    
    private void SetPlayerPos()
    {
        ShoppingCart.Instance.transform.position = transform.position;
        ShoppingCart.Instance.transform.rotation = transform.rotation;
    }
}
