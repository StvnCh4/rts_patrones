using Units;
using UnityEngine;

public class UnitIdleState : StateMachineBehaviour
{
    AttackController attackController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>(); //para acceder el controlador
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //verifica si hay un target disponible
        if (attackController.targetToAttack != null)
        {
            // -- transiciona a Follow state -- //
            animator.SetBool("isFollowing",true);
        }
    }

    
}
