using UnityEngine;

namespace Units
{
    public class AttackController : MonoBehaviour
    {
        public Transform targetToAttack;
        public Material idleStateMaterial;
        public Material followStateMaterial;
        public Material attackStateMaterial;
        private void  OnTriggerEnter(Collider other)
        {
            if ( other.CompareTag("Enemy") && targetToAttack == null)
            {
                targetToAttack = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ( other.CompareTag("Enemy") && targetToAttack != null)
            {
                targetToAttack = null;
            }
        }

        public void SetIdleMaterial()
        {
            GetComponent<Renderer>().material = idleStateMaterial;
        }
        
        public void SetFollowMaterial()
        {
            GetComponent<Renderer>().material = followStateMaterial;
        }
        
        public void SetAttackMaterial()
        {
            GetComponent<Renderer>().material = attackStateMaterial;
        }
        
    }
}
