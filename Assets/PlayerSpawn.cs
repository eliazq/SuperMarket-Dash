using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Start() {
        ShoppingCart.Instance.transform.position = transform.position;
        ShoppingCart.Instance.transform.rotation = transform.rotation;
    }
}
