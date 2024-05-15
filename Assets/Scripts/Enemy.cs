using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 min, max;
    [SerializeField] private Vector3 destination;
    [SerializeField] private bool playerDetected = false;
    [SerializeField] private bool playerAttack = false;

    [SerializeField] private GameObject player;
    [SerializeField] private float playerDistanceDetection = 50;
    [SerializeField] private float playerAttackDistance = 30;
    [SerializeField] private float visionAngle = 45;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        RandomDestination();

        StartCoroutine(Patroll());
        StartCoroutine(Alert());
    }

    private void RandomDestination()
    {
        destination = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(max.z, min.z));
        navMeshAgent.SetDestination(destination);
    }

    IEnumerator Patroll()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, destination) < 1.5f)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 3f));
                RandomDestination();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Alert()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < playerDistanceDetection)
            {
                Vector3 vectorPlayer = player.transform.position - transform.position;
                if (Vector3.Angle(vectorPlayer.normalized, transform.forward) < visionAngle)
                {
                    Debug.Log("Player detected");
                    playerDetected = true;
                    navMeshAgent.SetDestination(player.transform.position);
                    StopCoroutine(Patroll());
                    break;
                }
                else
                {
                    playerDetected = false;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        StopCoroutine("Alert");
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < playerAttackDistance)
            {
                Debug.Log("Player attack");
                navMeshAgent.SetDestination(transform.position);
                navMeshAgent.velocity = Vector3.zero;

                playerAttack = true;
                player.GetComponent<Player>().OnEnemyAttack();
            }
            else
            {
                destination = player.transform.position;
                navMeshAgent.SetDestination(destination);
                playerAttack = false;
                if (Vector3.Distance(transform.position, player.transform.position) >= playerDistanceDetection)
                {
                    playerDetected = false;
                    StartCoroutine(Patroll());
                    StartCoroutine(Alert());
                    StopCoroutine(Attack());
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}