using UnityEngine;

public class PlayerData : MonoBehaviour, IEntityComponent, IAfterInitable
{
    private Player _player;
    [SerializeField] private int _currentExp;
    public void Initialize(Entity entity)
    {
        _player = entity as Player;
    }
    public void AfterInitialize()
    {
        _player.expChannel.OnValueEvent += AddExp;
    }

    public void AddExp(int exp)
    {
        _currentExp += exp;
    }

}
