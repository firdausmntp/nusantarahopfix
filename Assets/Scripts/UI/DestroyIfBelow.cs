using UnityEngine;

public class DestroyIfBelow : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (transform.position.y < player.position.y - 10f)
        {
            Destroy(gameObject);
        }
    }
}
