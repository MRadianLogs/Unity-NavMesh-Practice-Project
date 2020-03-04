using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] string opponentTag = "Player";
    [SerializeField] int minBaseDamage = 3;
    [SerializeField] int maxBaseDamage = 6;
    // lots of opportunities to modify damage based on weapon, strength, etc. 
    // can also add logic for hit chances

    [SerializeField] float attackFrequency = 3f; //how frequently will we check for something to attack
    [SerializeField] float attackRange = 1.5f; // how close do we need to be to attack
    [SerializeField] float animationDelay = 0.5f; // how long to wait on the animation before actually doing the damage



    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AttackLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int ComputeDamage()
    {
        int baseDamage = Random.Range(minBaseDamage, maxBaseDamage + 1);
        // here you could handle weapong specific info, any damage multipliers, etc.
        return baseDamage;
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackFrequency);
            // collect potential targets
            GameObject[] opponentList = GameObject.FindGameObjectsWithTag(this.opponentTag);
            bool attackMade = false;
            int i = 0;
            while (i < opponentList.Length && !attackMade)
            {
                // check distance to this opponent
                Vector3 opponentPos = opponentList[i].transform.position;
                float distance = Vector3.Distance(opponentPos, transform.position);
                Debug.Log(distance);
                if (distance <= attackRange)
                {
                    // opponent is close enough -- make sure it can receive our damage
                    DamageTaker opp = opponentList[i].GetComponent<DamageTaker>();
                    if (opp)
                    {
                        Debug.Log("doing damage");
                        // it's close enough and it can take damage
                        animator.SetTrigger("Attack");
                        yield return new WaitForSeconds(animationDelay);
                        opp.TakeDamage(ComputeDamage());
                        attackMade = true;
                    }
                }
                i++;
            }
        }
    }
}
