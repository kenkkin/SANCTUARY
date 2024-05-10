using UnityEngine;
using UnityEngine.AI;

public class RayShooter : MonoBehaviour
{
    public GameObject spherePrefab;
    public float sphereLifetime = 3f;
    public Camera playerCamera; // reference to the camera

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // changed from 0 to 1 for right click
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
                {
                    Vector3 targetPosition = navHit.position;
                    FireSphere(targetPosition);
                }
            }
        }
    }

    private void FireSphere(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - playerCamera.transform.position).normalized; // Calculate direction from camera
        GameObject sphere = Instantiate(spherePrefab, playerCamera.transform.position, Quaternion.identity);
        Rigidbody sphereRigidbody = sphere.GetComponent<Rigidbody>();

        // set velocity to shoot the projectile in the calculated direction
        if (sphereRigidbody != null)
        {
            sphereRigidbody.velocity = direction * CalculateLaunchSpeed(targetPosition);
        }

        Destroy(sphere, sphereLifetime);
    }

    private float CalculateLaunchSpeed(Vector3 targetPosition)
    {
        // calculate the launch speed based on the distance and desired projectile flight time
        float distance = Vector3.Distance(playerCamera.transform.position, targetPosition);
        float flightTime = sphereLifetime;
        return distance / flightTime;
    }
}
