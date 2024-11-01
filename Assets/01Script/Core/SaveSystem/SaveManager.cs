using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public struct DataCollection
{
    public List<SaveData> dataCollection;
}
[Serializable]
public struct SaveData
{
    public int ID;
    public string Data;
}

public class SaveManager : MonoBehaviour
{
    [SerializeField] private BoolEventChannelSO _saveOrderChannel, _loadOrderChannel;
    [SerializeField] private string _saveDataKey = "saveData";
    [SerializeField] private string _saveFileName = "saveData.dat";
    
    private List<SaveData>  _unUsedData = new List<SaveData>();
    
    private void OnEnable()
    {
        _saveOrderChannel.OnValueEvent += HandleSaveOrder;
        _loadOrderChannel.OnValueEvent += HandleLoadOrder;
    }

    private void OnDisable()
    {
        _saveOrderChannel.OnValueEvent -= HandleSaveOrder;
        _loadOrderChannel.OnValueEvent -= HandleLoadOrder;
    }

    private void HandleLoadOrder(bool isLoadFromFile)
    {
        if (isLoadFromFile)
        {
            string loadData = string.Empty;
            if (LoadDataFromFile(_saveFileName, out loadData))
            {
                RestoreData(loadData);
            }
        }
        else
        {
            string loadData = PlayerPrefs.GetString(_saveDataKey, string.Empty);
            RestoreData(loadData);
        }
    }
    private bool LoadDataFromFile(string gameSaveFileName, out string data)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, gameSaveFileName);
        data = string.Empty;
        if (File.Exists(fullPath) == false)
            return false;
        
        try
        {
            data = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error on loading save file..{ex.Message}");
            return false;
        }
    }
    
    private void HandleSaveOrder(bool isSaveToFile)
    {
        string dataJson = GetDataToSave();
        if (isSaveToFile)
            if (WriteToFile(_saveFileName, dataJson) == false)
            {
                Debug.Log("Failed to save game saved data to " + _saveFileName);
            }
        else
            PlayerPrefs.SetString(_saveDataKey, dataJson);
    }
    
    private bool WriteToFile(string gameSaveFileName, string data)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, gameSaveFileName);
        Debug.Log(fullPath);
        try
        {
            File.WriteAllText(fullPath, data);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }
    
    
    
    private void RestoreData(string loadData)
    {
        IEnumerable<ISavable> savableObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISavable>();
        DataCollection collection = string.IsNullOrEmpty(loadData) ? new DataCollection() : JsonUtility.FromJson<DataCollection>(loadData);

        _unUsedData.Clear();

        if(collection.dataCollection != null)
        {
            foreach (var saveData in collection.dataCollection)
            {
                var savable = savableObjects.FirstOrDefault(savable => savable.IdData.saveID == saveData.ID);

                if (savable != null)
                {
                    savable.RestoreData(saveData.Data);
                }
                else
                {
                    _unUsedData.Add(saveData); //현재 씬에 없다면 버리지 말고 미사용 데이터에 넣어둔다.
                }
            }
        }

    }
    
    private string GetDataToSave()
    {
        IEnumerable<ISavable> savableObjects =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISavable>();
            
        List<SaveData> toSaveData = new List<SaveData>();
        foreach (ISavable savable in savableObjects)
        {
            toSaveData.Add(new SaveData{ ID = savable.IdData.saveID, Data = savable.GetSaveData()});
        }
            
        toSaveData.AddRange(_unUsedData); //이번씬에 사용되지 않았던 데이터들 저장
        DataCollection dataCollection = new DataCollection { dataCollection = toSaveData };
            
        return JsonUtility.ToJson(dataCollection);
    }

}
