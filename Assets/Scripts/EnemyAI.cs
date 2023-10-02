using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;          // Reference to the player's transform
    public float followRange = 10f;   // The range within which the enemy will follow the player
    public float normalSpeed = 3f;    // Normal movement speed
    public float chaseSpeed = 6f;     // Speed when chasing the player
    public float forwardSpeed = 5f;   // Speed when moving forward without chasing
    public float forwardDuration = 2f; // Duration for moving forward before checking player position

    private NavMeshAgent agent;
    private bool isChasing = false;
    private float forwardTimer = 0f;
    private Vector3 playerChasePosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= followRange)
        {
            if(isChasing)
            {
                Debug.Log(agent.pathPending + " - " + agent.remainingDistance);
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    //Debug.Log("waiting");
                    forwardTimer += Time.deltaTime;
                    if (forwardTimer >= forwardDuration)
                    {
                        //Debug.Log("new chasing");
                        forwardTimer = 0f;
                        isChasing = false;
                    }
                }
                else
                {
                    Debug.Log("chere");
                    agent.SetDestination(player.position);
                }
                    //Vector3 forwardVelocity = agent.velocity.normalized * forwardSpeed;
                    //agent.velocity = new Vector3(forwardVelocity.x, agent.velocity.y, forwardVelocity.z);

            }
            else
            {
                //Debug.Log("beggan chasing");
                isChasing = true;
                agent.SetDestination(player.position);
            }

            agent.speed = chaseSpeed;
            
        }
        else
        {
            if (isChasing)
            {
                //Debug.Log("stop chasing");
                forwardTimer += Time.deltaTime;
                if (forwardTimer >= forwardDuration)
                {
                    forwardTimer = 0f;
                    isChasing = false;
                }

                Vector3 forwardVelocity = agent.velocity.normalized * forwardSpeed;
                agent.velocity = new Vector3(forwardVelocity.x, agent.velocity.y, forwardVelocity.z);
            }
            else
            {
                //Debug.Log("normal chasing");
                agent.SetDestination(player.position);
            }
            agent.speed = normalSpeed;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide check");
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("collide");
            agent.SetDestination(transform.position);
            agent.speed = normalSpeed;
            isChasing = false;
        }
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        // Check if the player is within the follow range
        if (distanceToPlayer <= followRange)
        {
            agent.speed = chaseSpeed;
            //agent.SetDestination(playerChasePosition);
            if (isChasing)
            {

                //forwardTimer += Time.deltaTime;
                //if (forwardTimer >= forwardDuration)
                //{
                //    forwardTimer = 0f;
                //    isChasing = false;
                //    agent.speed = normalSpeed;
                //    agent.SetDestination(transform.localPosition);
                //}
            }
            else
            {
                playerChasePosition = player.position;
                agent.SetDestination(playerChasePosition);
                isChasing = true;
            }
        }
        else
        {
            if (isChasing)
            {
                forwardTimer += Time.deltaTime;
                if (forwardTimer >= forwardDuration)
                {
                    forwardTimer = 0f;
                    isChasing = false;
                    agent.speed = normalSpeed;
                    agent.SetDestination(transform.position);
                }
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }
    }
}
