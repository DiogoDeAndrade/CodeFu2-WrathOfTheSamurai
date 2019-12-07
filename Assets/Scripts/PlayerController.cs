using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    [Range(0.0f, 1.0f)]
    public float drag = 0.1f;

    Camera      gameCamera;
    Rigidbody   rigidBody;
    Animator    animator;
    bool        dead = false;

    void Start()
    {
        gameCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!dead)
        {
            Vector3 cameraX = gameCamera.transform.right; cameraX.y = 0.0f; cameraX.Normalize();
            Vector3 cameraZ = gameCamera.transform.forward; cameraZ.y = 0.0f; cameraZ.Normalize();

            Vector3 movementDir = cameraX * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime +
                                  cameraZ * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            Vector3 velocity = rigidBody.velocity;

            velocity.x = velocity.x * (1.0f - drag);
            velocity.z = velocity.z * (1.0f - drag);

            velocity = velocity + movementDir;

            rigidBody.velocity = velocity;

            Quaternion desiredRotation = Quaternion.Euler(0.0f, gameCamera.transform.rotation.eulerAngles.y, 0.0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, 180.0f * Time.deltaTime);

            animator.SetFloat("SpeedX", Vector3.Dot(velocity, cameraX));
            animator.SetFloat("SpeedZ", Vector3.Dot(velocity, cameraZ));
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        dead = true;
        GameObject.FindObjectOfType<GameOver>().ActivateGameOver();
    }
}
