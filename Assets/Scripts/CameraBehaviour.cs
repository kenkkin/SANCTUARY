using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, 0);
    }
}
