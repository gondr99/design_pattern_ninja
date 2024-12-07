using Unity.Behavior;
using UnityEngine;

public class BTEnemy : CommonEnemy
{
    protected BehaviorGraphAgent _btAgent;

    protected override void Awake()
    {
        base.Awake();
        _btAgent = GetComponent<BehaviorGraphAgent>();
    }

    public Transform GetTargetInRadius(float radius)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, _whatIsPlayer);
        if(collider != null)
            return collider.transform;
        return null;
    }
    
    public BlackboardVariable<T> GetVariable<T>(string variableName)
    {
        if (_btAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
        {
            return variable;
        }

        return null;
    }

    public bool SetVariable<T>(string variableName, T value)
    {
        Debug.Assert(value != null, "BlackboardVariable cannot be null");
        BlackboardVariable<T> variable = GetVariable<T>(variableName);

        if (variable == null)
            return false;
        
        variable.Value = value;
        return true;
    }
}
