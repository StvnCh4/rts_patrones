using Units;
using UnityEngine;
using UnityEngine.AI;

namespace States.Units
{
    public class FollowState : IUnitState
    {
        AttackController attackController;

        NavMeshAgent agent;
        

        public void Enter(Unit unit)
        {
            unit.animator.SetTrigger("Walk");
            //unit.animator.SetBool("isFollowing", true);
            unit.attackController.SetFollowMaterial();
        }

        public void Update(Unit unit)
        {

            var target = unit.attackController.targetToAttack;
            //deberia la unidad transicionar al estado idle?

            if (target == null)
            {
                unit.ChangeState(new IdleState());
                return;
            }

            if (unit.movement.isCommandedToMove)
            {
                return;
            }
            
            
            if (target != null)
            {
                
                unit.movement.MoveTo(target.position);
                unit.transform.LookAt(target);
            }
            
            
            if (!unit.movement.isCommandedToMove)
            {
                //if there is no other direct command to move 
                //mover unidad hacia el enemigo
            
                unit.movement.MoveTo(target.position);
                unit.transform.LookAt(target);
            
                //deberia la unidad transicionar al estado ataque?
                float distance = Vector3.Distance(unit.transform.position, target.position);
                if (distance <= unit.attackingDistance)
                {
                    unit.ChangeState(new AttackState());
                }
            }
        }
        
        public void Exit(Unit unit)
        {
            unit.agent.ResetPath(); 
        }
        
    }
}


