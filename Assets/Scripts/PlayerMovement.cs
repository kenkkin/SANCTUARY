using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] GameObject deathMenu;
    private bool uiInteraction = false;

    [Space]

    [Header("Rotation")]
    [SerializeField] Transform weapon;
    [SerializeField] float offset;

    private float originalSpeed;

    void Start()
    {
        deathMenu.SetActive(false);
        
        originalSpeed = agent.speed;
    }

    void Update()
    {
        // movement
        Move();

        // rotation
        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0.0f, 0.0f, angle + offset);

        // UI
        // Check if UI interaction is occurring
        if (EventSystem.current.IsPointerOverGameObject())
        {
            uiInteraction = true;
        }
        else
        {
            uiInteraction = false;
        }

        // Process NavMesh interaction only if there is no UI interaction
        if (!uiInteraction && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked point is on the NavMesh
                if (hit.collider.CompareTag("NavMesh"))
                {
                    // Move the player to the clicked point
                    GetComponent<NavMeshAgent>().destination = hit.point;
                }
            }
        }
    }

    public void Move()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0.0f;

            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                playerMovement.enabled = false;
            }
        }
    }

    public void IncreaseSpeed(float amount)
    {
        agent.speed += amount;
    }

    // Method to reset player speed back to normal
    public void ResetSpeed()
    {
        agent.speed = originalSpeed;
    }
}
