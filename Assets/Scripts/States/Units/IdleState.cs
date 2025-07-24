using UnityEngine;

namespace States.Units
{
    public class IdleState: IUnitState
    {
        
        public void Enter(Unit unit)
        {
            unit.animator.SetTrigger("idle");
           // unit.animator.SetBool("isIdle",true);
            unit.attackController.SetIdleMaterial();
        }

        public void Update(Unit unit )
        {
            //verifica si hay un target disponible
            if (unit.attackController.targetToAttack != null)
            {
                // -- transiciona a Follow state -- //
                //unit.animator.SetBool("isFollowing",true);
                //usando el patron state
                unit.ChangeState(new FollowState());
            }        
        }

        public void Exit(Unit unit )
        {
        }
    }
}
