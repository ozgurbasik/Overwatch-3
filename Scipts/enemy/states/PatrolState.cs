using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float wait;
    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        PatrolCycle();
    }
    public override void Exit()
    {

    }
    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            wait += Time.deltaTime;
            if (wait > 3)
            {
                if (waypointIndex < enemy.paths.waypoints.Count - 1)
                {
                    waypointIndex++;

                }
                else
                    waypointIndex = 0;
                enemy.Agent.SetDestination(enemy.paths.waypoints[waypointIndex].position);
                wait = 0;
            }
        }
    }
    // Start is called before the first frame update

}
