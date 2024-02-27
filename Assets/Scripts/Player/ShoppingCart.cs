using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCart : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform hitCastTransform;
    [SerializeField] private Vector3 hitCastSize;
    private ShoppingCartMovement shoppingCartMovement;
    // to ignore this gameobject in castCheck
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask killableLayer;
    [Header("Settings")]
    [SerializeField] private float hitObstacleForce = 100f;
    [Space(10)]
    [Header("Dying Visual Objects")]
    [SerializeField] private GameObject shoppingCartHandleVisual;

    bool isDead = false;

    private void Start() {
        shoppingCartMovement = GetComponent<ShoppingCartMovement>();
    }

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
            DisableMovement();
            shoppingCartHandleVisual.GetComponent<Collider>().enabled = true;
            shoppingCartHandleVisual.AddComponent<Rigidbody>();
            SoundManager.StopSoundAudioSource(SoundManager.Sound.PlayerMove);
            PauseCanvas.Instance.Show();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Goal goal) && !goal.hasWon)
        {
            WonLevel(goal);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (((1 << other.gameObject.layer) & killableLayer) != 0)
        {
            Die();
        }
    }

    private void WonLevel(Goal goal)
    {
        goal.WonLevel();
        DisableMovement();
        SoundManager.StopSoundAudioSource(SoundManager.Sound.PlayerMove); 
    }

    private void DisableMovement()
    {
        shoppingCartMovement.enabled = false;
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
