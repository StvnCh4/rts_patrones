
using States.Units;
using Units;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnitMovement), typeof(AttackController))]
public class Unit : MonoBehaviour
{
    
    public IUnitState currentState;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public UnitMovement movement;
    [HideInInspector] public AttackController attackController;

    public float attackingDistance = 1f;
    public float stopAttackingDistance = 1.2f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        movement = GetComponent<UnitMovement>();
        attackController = GetComponent<AttackController>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
        ChangeState(new IdleState());
    }
    
    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
    }
    
    private void Update()
    {
        currentState?.Update(this);
    }

    public void ChangeState(IUnitState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
}
