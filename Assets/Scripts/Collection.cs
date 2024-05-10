using UnityEngine;

public class Collection : MonoBehaviour
{
    public GameHandler GH;
    public AudioClip collectibleSound;
    void Start()
    {
        GH = GameObject.Find("Canvas").GetComponent<GameHandler>();
    }

private void OnTriggerEnter(Collider other)
    {
        GH.collectibles++;
        AudioSource.PlayClipAtPoint(collectibleSound, transform.position);
        Destroy(gameObject); 
    }
}
