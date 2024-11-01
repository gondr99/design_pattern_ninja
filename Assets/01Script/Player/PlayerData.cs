using System;
using UnityEngine;

public class PlayerData : MonoBehaviour, IEntityComponent, IAfterInitable, ISavable
{
    
    [field: SerializeField] public SaveIDSO IdData { get; private set; }
    
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

    #region Saving data
    [Serializable]
    public struct PlayerSaveData
    {
        public int currentExp;
       //기타등등 저장할 데이터
    }

    public void AddExp(int exp)
    {
        _currentExp += exp;
    }

    public string GetSaveData()
    {
        var saveData = new PlayerSaveData
        {
            currentExp = _currentExp
        };
        return JsonUtility.ToJson(saveData);
    }

    public void RestoreData(string data)
    {
        var parseData = JsonUtility.FromJson<PlayerSaveData>(data);
        _currentExp = parseData.currentExp;
        //다른 데이터 복원시 여기다가
    }
    
    #endregion
}
