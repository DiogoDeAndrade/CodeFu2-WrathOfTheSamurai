using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float        timeToLive = 5.0f;
    public float        moveSpeed = 40.0f;
    public GameObject   sparksPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0.0f)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = other.GetComponent<PlayerController>();
            if (player)
            {
                Instantiate(sparksPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
                player.Die();
            }
        }
    }
}
