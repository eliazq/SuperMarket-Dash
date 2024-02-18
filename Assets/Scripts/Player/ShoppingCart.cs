using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCart : MonoBehaviour
{
    public static ShoppingCart Instance;
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
    private ShoppingCartMovement cartMovement;

    bool isDead = false;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        cartMovement = GetComponent<ShoppingCartMovement>();
        SceneManager.sceneLoaded += SceneChanged_Action;
    }

    private void SceneChanged_Action(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (!cartMovement.enabled)
        {
            cartMovement.enabled = true;
        }
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
    
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Goal goal))
        {
            goal.WonLevel();
            Debug.Log("Won The Level From Player");
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
