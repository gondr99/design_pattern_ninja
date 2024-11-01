using UnityEngine;

public class EnemyBomber : Enemy
{
    public Player target;
    [SerializeField] private EntityFSMSO _bomberFSM;
    [SerializeField] private float _checkRadius;
    
    [SerializeField] private StateMachine _stateMachine;
    
    public EntityState CurrentState => _stateMachine.currentState;
    
    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        _stateMachine = new StateMachine(_bomberFSM, this);
        
        GetCompo<EntityAnimator>(true).OnAnimationEnd += HandleAnimationEnd;
    }

    private void HandleAnimationEnd()
    {
        CurrentState.AnimationEndTrigger();
    }
    private void OnDestroy()
    {
        GetCompo<EntityAnimator>(true).OnAnimationEnd -= HandleAnimationEnd;
    }

    protected void Start()
    {
        _stateMachine.Initialize(StateName.Idle);
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();
    }

    public void ChangeState(StateName newState)
    {
        _stateMachine.ChageState(newState);
    }

    public EntityState GetState(StateSO state)
    {
        return _stateMachine.GetState(state.stateName);
    }


    public bool CheckPlayerInRadius()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, _checkRadius, _whatIsPlayer);

        if (col != null && col.TryGetComponent(out Player player))
        {
            target = player;
            return true;
        }
        return false;
    }


    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }
}
