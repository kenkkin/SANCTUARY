using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrolling")]
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float patrolSpeed;

    [Space]

    [Header("Chasing")]
    [SerializeField] float chaseSpeed;

    [Space]

    [Header("States")]
    [SerializeField] float sightRange;
    [SerializeField] float fieldOfViewAngle;
    [SerializeField] bool inSightRange;

    [Space]

    [Header("Enemy Movement")]
    [SerializeField] NavMeshAgent enemy;
    [SerializeField] Transform player;
    [SerializeField] LayerMask isPlayer;

    private int targetPoint;
    private bool patrolForward = true; // Flag to determine the direction of patrolling

    void Awake()
    {
        player = GameObject.Find("player").transform;
        enemy = GetComponent<NavMeshAgent>();
        targetPoint = 0;
    }

    void Update()
    {
        // inSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        inSightRange = InSightRange(player.position);

        if (!inSightRange)
        {
            Patrolling();
        }
        else
        {
            Chase();
        }
    }

    void Patrolling()
    {
        Debug.Log("Patrol");

        // Determine the direction of patrolling
        int direction = patrolForward ? 1 : -1;

        // Calculate the next target waypoint index
        int nextTargetIndex = targetPoint + direction;

        // Check if the enemy reaches the end of the waypoint array
        if (nextTargetIndex >= wayPoints.Length || nextTargetIndex < 0)
        {
            // Reverse the direction of patrolling
            patrolForward = !patrolForward;

            // Calculate the next target waypoint index
            nextTargetIndex = targetPoint + direction;

            // Clamp the next target index to stay within bounds
            nextTargetIndex = Mathf.Clamp(nextTargetIndex, 0, wayPoints.Length - 1);
        }

        // Move towards the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextTargetIndex].position, patrolSpeed * Time.deltaTime);

        // Rotate towards the target waypoint
        Vector3 directionToTarget = (wayPoints[nextTargetIndex].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // Check if the enemy has reached the target waypoint
        if (Vector3.Distance(transform.position, wayPoints[nextTargetIndex].position) < 0.1f)
        {
            // Update the target point index
            targetPoint = nextTargetIndex;
        }
    }

    public void Chase()
    {
        Debug.Log("Chase");
        enemy.SetDestination(player.position);
        transform.LookAt(player);
        enemy.speed = chaseSpeed;
    }

    bool InSightRange(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Vector3.Angle(direction, transform.forward); // calculate the angle

        // check if the target is within sight range and within the field of view angle
        if (direction.magnitude < sightRange && angle < fieldOfViewAngle * 0.5f)
        {
            // check if there are obstacles between the enemy and the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, sightRange, isPlayer))
            {
                // if the hit object is the player, return true
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
