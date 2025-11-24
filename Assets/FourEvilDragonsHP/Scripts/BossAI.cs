using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Transform player;

    public float detectionRadius = 20f;
    public float attackRadius = 3f;
    public float roamRadius = 5f;
    public float roamDelay = 4f;

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float roamTimer;

    private enum BossState { Idle, Roam, Chase, Attack }
    private BossState state = BossState.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        roamTimer = roamDelay;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case BossState.Idle:
                HandleIdle(distance);
                break;

            case BossState.Roam:
                HandleRoam(distance);
                break;

            case BossState.Chase:
                HandleChase(distance);
                break;

            case BossState.Attack:
                HandleAttack(distance);
                break;
        }
    }

    private void HandleIdle(float distance)
    {
        if (distance <= detectionRadius)
        {
            state = BossState.Chase;
            return;
        }

        roamTimer -= Time.deltaTime;
        if (roamTimer <= 0)
        {
            state = BossState.Roam;
            roamTimer = roamDelay;
        }
    }

    private void HandleRoam(float distance)
    {
        if (distance <= detectionRadius)
        {
            state = BossState.Chase;
            return;
        }

        if (!agent.hasPath)
        {
            Vector3 newPos = startPosition + Random.insideUnitSphere * roamRadius;
            agent.SetDestination(new Vector3(newPos.x, transform.position.y, newPos.z));
        }

        if (agent.remainingDistance <= 1f)
        {
            state = BossState.Idle;
        }
    }

    private void HandleChase(float distance)
    {
        if (distance > detectionRadius * 1.5f)
        {
            state = BossState.Idle;
            return;
        }

        agent.SetDestination(player.position);

        if (distance <= attackRadius)
        {
            state = BossState.Attack;
        }
    }

    private void HandleAttack(float distance)
    {
        agent.SetDestination(transform.position); // stop moving

        transform.LookAt(player);

        // TODO: Trigger an animation event here
        Debug.Log("Boss is attacking!");

        if (distance > attackRadius + 2f)
        {
            state = BossState.Chase;
        }
    }
}
