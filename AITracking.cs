using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITracking : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    private Transform killer;
    private NavMeshAgent agent;
    public Transform raycastOrigin;
    public float distance;
    private Animator animator;
    private Vector3 lastSeenPosition;
    private bool reachedRandom = true;
    private SyringeCheck syringeCheck;
    public bool taskDone;
    public bool caught;
    public AudioSource laugh;
    private bool played = false;
    public bool respawn;
    public Vector3 killerSpawn1;
    public Vector3 killerSpawn2;

    private float killTimer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("running", false);
        animator.SetBool("caught", false);
        syringeCheck = FindObjectOfType<SyringeCheck>();
        killer = GetComponent<Transform>();
        killerSpawn1 = new Vector3 (1.5f, 0, -48);
        killerSpawn2 = new Vector3(22.5f, 0, -48);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 direction = player.position - raycastOrigin.position;
        distance = direction.magnitude;
        bool sight = (Physics.Raycast(raycastOrigin.position, (direction).normalized, out hit));

        if (sight && hit.collider.CompareTag("Player"))
          {
                lastSeenPosition = player.position;

                if (distance < 4 && caught == false)
                {
                    caught = true;

                    if (caught)
                    {
                        killTimer = 0;
                        agent.SetDestination(transform.position);
                        Quaternion rotation = Quaternion.LookRotation(cam.transform.position);
                        rotation *= Quaternion.Euler(0, 45, 0);
                        transform.rotation = rotation;
                        animator.SetBool("running", false);
                        animator.SetBool("caught", true);
                        animator.SetBool("walking", false);
                        if (played == false)
                        {
                            laugh.Play();
                            played = true;
                        }
                    }
                }
            else if (distance > 10)
                {
                    caught = false;
                    agent.SetDestination(player.position);
                    animator.SetBool("running", true);
                    animator.SetBool("caught", false);
                    animator.SetBool("walking", false);
                }
           }
        else
            {
            if (lastSeenPosition != Vector3.zero && !taskDone)
            {

                animator.SetBool("running", true);
                animator.SetBool("caught", false);
                if ((transform.position-lastSeenPosition).magnitude < 1f)
                {
                    agent.SetDestination(lastSeenPosition);
                    taskDone = true;
                    reachedRandom = true;
                }
            }
            else if (reachedRandom && syringeCheck.syringeCount >= 1)
            {
                taskDone = true;
                Vector3 randomDirection = Random.insideUnitSphere;
                randomDirection.y = 0;
                Vector3 randomPosition = transform.position + randomDirection * Random.Range(20, 50);
                agent.SetDestination(randomPosition);
                animator.SetBool("walking", true);
                animator.SetBool("running", false);
                animator.SetBool("caught", false);
                reachedRandom = false;
            }
            if ((agent.remainingDistance < 0.1f))
                {
                 reachedRandom = true;
                }
            }

        if (caught)
        {
            killTimer += Time.deltaTime;

            if(killTimer >= 2.5f)
            {
                respawn = true;
                transform.position = killerSpawn1;
                if (distance < 10)
                {
                    transform.position = killerSpawn2;
                }
                caught = false;
                reachedRandom = true;
                played = false;
            }
        }
    }
}
