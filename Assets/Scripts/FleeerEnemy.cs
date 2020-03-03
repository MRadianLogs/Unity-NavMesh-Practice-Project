using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeerEnemy : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private NavMeshAgent enemyAgent = null;

    [SerializeField]
    private bool isPointsPatrolType = true; //If true, the enemy patrols using 4 points in the map. Else, will use a  patroll circle radius.
    [SerializeField]
    private int patrolCircleRadius = 5; //Used if isPointsPatrolType is false.
    //[SerializeField]
    private Transform[] patrolList = null;
    [SerializeField]
    private Transform patrolPoint1 = null; //Also represents patrol circle origin.
    [SerializeField]
    private Transform patrolPoint2 = null;
    [SerializeField]
    private Transform patrolPoint3 = null;
    [SerializeField]
    private Transform patrolPoint4 = null;
    private int currentPatrolPointIndex = 0;

    private bool detectedPlayer = false;
    [SerializeField]
    private int detectionRadius = 20;
    [SerializeField]
    private int fleeOffset = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (isPointsPatrolType)
        {
            patrolList = new Transform[4];
            patrolList[0] = patrolPoint1;
            patrolList[1] = patrolPoint2;
            patrolList[2] = patrolPoint3;
            patrolList[3] = patrolPoint4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the player has not been detected.
        if (!detectedPlayer)
        {
            //Patroll.
            Patroll();
        }
        else
        {
            //Go after them and attack!
            ReactToPlayer();
        }
        //Check for player.
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < detectionRadius)
        {
            detectedPlayer = true;
        }
        else
        {
            detectedPlayer = false;
        }
    }

    private void Patroll()
    {
        if (isPointsPatrolType)
        {
            //Navigate between the four points.
            if (enemyAgent.remainingDistance < 0.5f)
            {
                enemyAgent.SetDestination(patrolList[currentPatrolPointIndex].position);
                if (currentPatrolPointIndex < (patrolList.Length - 1))
                {
                    currentPatrolPointIndex++;
                }
                else
                {
                    currentPatrolPointIndex = 0;
                }
            }

        }
        else
        {
            if (enemyAgent.remainingDistance < 0.5f)
            {
                Vector2 nextPatrolPoint = Random.insideUnitCircle.normalized;
                //Navigate to the next patrol point!                               //TODO: Check vector math!
                enemyAgent.SetDestination(new Vector3(patrolPoint1.position.x + (patrolCircleRadius * nextPatrolPoint.x), patrolPoint1.position.y, patrolPoint1.position.z + (patrolCircleRadius * nextPatrolPoint.y)));
            }
        }
    }

    private void ReactToPlayer()
    {
        //Flee from the player! Run in the opposite direction!
        Vector3 distanceVector = transform.position - player.transform.position;
        Vector3 distanceVectorNormalized = distanceVector.normalized;
        Vector3 targetDistance = (distanceVectorNormalized * fleeOffset);
        enemyAgent.SetDestination(transform.position + targetDistance);

    }
}
