using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform hitCastTransform;
    [SerializeField] private Vector3 hitCastSize;
    // to ignore this gameobject in castCheck
    [SerializeField] private LayerMask obstacleLayer;
    [Header("Settings")]
    [SerializeField] private float hitObstacleForce = 100f;
    [Space(10)]
    [Header("Dying Visual Objects")]
    [SerializeField] private GameObject visualObject;

    bool isDead = false;
    
    private void Update() {

        if (HitObstacle() && !isDead)
        {
            Die();
            Vector3 hitDir = new Vector3(Random.Range(-hitObstacleForce, hitObstacleForce), 0, -hitObstacleForce);
            GetComponent<Rigidbody>().AddForce(hitDir, ForceMode.Impulse);
        }

    }

    private bool HitObstacle()
    {
        return Physics.CheckBox(hitCastTransform.position, hitCastSize, Quaternion.identity, obstacleLayer);
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            GetComponent<ShoppingCartMovement>().enabled = false;

            visualObject.GetComponent<Collider>().enabled = true;
            visualObject.AddComponent<Rigidbody>();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.TryGetComponent(out Obstacle obstacle))
        {
            Die();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 boxCenter = hitCastTransform.position;
        Quaternion boxRotation = Quaternion.identity;
        Vector3 boxSize = hitCastSize * 2; // Convert half-extents to full extents
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, boxRotation, Vector3.one);
        Gizmos.DrawCube(Vector3.zero, boxSize);
    }
}
