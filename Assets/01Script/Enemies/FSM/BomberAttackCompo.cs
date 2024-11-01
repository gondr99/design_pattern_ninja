using UnityEngine;

public class BomberAttackCompo : MonoBehaviour, IEntityComponent
{
    
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private float _fireAngle = 45f;
    [SerializeField] private float _cooldown;
    
    private float _lastAtkTime;
    private EnemyBomber _bomber;
    
    
    public void Initialize(Entity entity)
    {
        _bomber = entity as EnemyBomber;
        Debug.Assert(_bomber != null, "Check! Bomber Attack component wrong! ");
    }

    public bool CanAttack() => _lastAtkTime + _cooldown < Time.time;

    public void Attack()
    {
        _lastAtkTime = Time.time;
        
        // (1 / CosΘ) * Mathf.Sqrt( (0.5f * g * distance^2) / (distance * TanΘ + yOffset) );
        float angle = _fireAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angle);
        float tan = Mathf.Tan(angle);
        float gravity = Physics2D.gravity.magnitude;
        Vector2 direction = transform.position - _bomber.target.transform.position;

        float distance = Mathf.Abs(direction.x);
        float yOffset = direction.y;
        
        float vZero = (1 / cos) * Mathf.Sqrt( (0.5f * gravity * Mathf.Pow(distance,2)) / (distance * tan + yOffset));
        
        float xDirection = -Mathf.Sign(direction.x);
        Vector2 velocity = new Vector3( xDirection*vZero * Mathf.Cos(angle), vZero * Mathf.Sin(angle));

        Bomb bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);

        bomb.ThrowBomb(velocity, 4f);
    }
}
