using Units;
using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
     AttackController attackController;

     NavMeshAgent agent;

     public float attackingDistance = 1f;
     
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>(); //para acceder el controlador
        agent=animator.transform.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //deberia la unidad transicionar al estado idle?

        if (attackController.targetToAttack == null)
        {
            animator.SetBool("isFollowing",false);
        }
        else
        { //if there is no other direct command to move 
            if (animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
            {
                //mover unidad hacia el enemigo
        
                agent.SetDestination(attackController.targetToAttack.position);
                animator.transform.LookAt(attackController.targetToAttack);
        
                //deberia la unidad transicionar al estado ataque?
                // float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position); //tomar la distancia entre unidad y enemigo
                // if (distanceFromTarget < attackingDistance)
                // {
                //    // agent.SetDestination(animator.transform.position);
                //     animator.SetBool("isAttacking", true);//Move to Attacking State
                // }
            }
        }
  
    }


}
