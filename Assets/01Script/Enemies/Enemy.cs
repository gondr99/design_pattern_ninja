using System;
using Unity.Behavior;
using UnityEngine;

public abstract class Enemy : Entity
{

    [SerializeField] protected float _sightRange = 10f, _wallCheckRange = 1f;
    [SerializeField] protected LayerMask _whatIsPlayer, _whatIsObstacle;
    [SerializeField] protected IntEventChannelSO _expChannel, _goldChannel;


    public RaycastHit2D CheckPlayerInRange()
    {
        return Physics2D.Raycast(transform.position, transform.right, _sightRange, _whatIsPlayer);
    }

    public RaycastHit2D CheckObstacleInFront()
    {
        return Physics2D.Raycast(transform.position, transform.right, _wallCheckRange, _whatIsObstacle);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * _sightRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * _wallCheckRange);
    }
}
