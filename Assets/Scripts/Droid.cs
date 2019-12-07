using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Droid : MonoBehaviour
{
    public enum State { Patrol, Pursuit };

    public State            state = State.Patrol;
    public Transform[]      waypoints;
    public LayerMask        groundMask;
    public float            maxSightDistance = 10;
    public float            coneOfSight = 45;
    public ParticleSystem   chargePS;
    public ParticleSystem   burstPS;
    public GameObject       laserPrefab;
    public Transform        shootPoint;

    int                 waypointIndex = 0;
    NavMeshAgent        agent;
    PlayerController    targetPlayer;
    Vector3             lastSeen;
    Coroutine           shootCR;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        if (state == State.Patrol)
        {
            if (agent.remainingDistance < 0.1f)
            {
                waypointIndex = (waypointIndex + 1) % waypoints.Length;

                Vector3 destPoint = waypoints[waypointIndex].position;

                agent.SetDestination(destPoint);
                agent.isStopped = false;
            }

            PlayerController[] players = GameObject.FindObjectsOfType<PlayerController>();
            foreach (PlayerController player in players)
            {
                if (HasLOS(player, true, true))
                {
                    agent.isStopped = true;
                    state = State.Pursuit;
                    targetPlayer = player;
                    break;
                }
            }
        }
        else if (state == State.Pursuit)
        {
            Vector3 toPlayer = targetPlayer.transform.position - transform.position;
            toPlayer.y = 0.0f; toPlayer.Normalize();

            if (!HasLOS(targetPlayer, true, false))
            {
                if (Vector3.Distance(lastSeen, transform.position) > 2.0f)
                {
                    agent.isStopped = false;
                    agent.SetDestination(lastSeen);
                }
                else
                {
                    agent.isStopped = false;
                    state = State.Patrol;
                    targetPlayer = null;
                }
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(toPlayer, Vector3.up);
                lastSeen = targetPlayer.transform.position;

                if (shootCR == null)
                {
                    shootCR = StartCoroutine(ShootCR());
                }
            }
        }
    }

    IEnumerator ShootCR()
    {
        chargePS.Play();
        
        float scale = 1.0f;
        while (scale > 0.3f)
        {
            scale = scale - Time.deltaTime * 0.5f;
            chargePS.transform.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }

        chargePS.Stop();
        burstPS.Play();

        Vector3 toPlayer = (targetPlayer.transform.position + Vector3.up * 0.9f) - shootPoint.transform.position;
        Quaternion pointDir = Quaternion.LookRotation(toPlayer, Vector3.up);

        Instantiate(laserPrefab, shootPoint.transform.position, pointDir);

        yield return new WaitForSeconds(0.25f);

        burstPS.Stop();

        yield return new WaitForSeconds(4.0f);

        shootCR = null;
    }

    bool HasLOS(PlayerController player, bool respectDistance, bool respectCone)
    {
        Vector3 toPlayer = (player.transform.position + Vector3.up * 0.9f) - transform.position;
        Ray   ray = new Ray(transform.position, toPlayer.normalized);
        bool hit = Physics.SphereCast(ray, 0.25f, float.MaxValue, groundMask);

        if (hit)
        {
            return false;
        }

        if (respectDistance)
        {
            if (toPlayer.magnitude > maxSightDistance)
            {
                return false;
            }
        }

        if (respectCone)
        {
            toPlayer.y = 0.0f; toPlayer.Normalize();
            float dp = Vector3.Dot(toPlayer, transform.forward);
            if (dp < Mathf.Cos(coneOfSight * Mathf.Deg2Rad))
            {
                return false;
            }
        }

        return true;
    }
}
