using UnityEngine;
using System.IO;

public class Lsmanager : MyBehaviour
{
    protected static Lsmanager instance;
    public static Lsmanager Instance { get => instance;}
    protected const string SAVE = "save";
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
            Debug.LogWarning(this.gameObject + "Does Existed");
        }
        else instance = this;
    }
    protected override void Start()
    {
        this.LoadGameData();
        base.Start();
    }
    protected virtual string GetSaveName(string name)
    {
        return Lsmanager.SAVE + "_" + name;
    }
    // public virtual void LoadGameData()
    // {        
    //     string JsonString = SaveSystem.GetString(this.GetSaveName("DataManager"));
    //     DataManager.Instance.PlayerDataFromJson(JsonString);
    //     Debug.Log("jsonstring : " + JsonString);
    // }
    // public virtual void SaveGame()
    // {
    //     string JsonString = JsonUtility.ToJson(DataManager.Instance);
    //     SaveSystem.SetString(this.GetSaveName("DataManager"),JsonString);
    //     Debug.Log(JsonString);
    // }
    public void LoadGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
        string jsonString = "";

        if (File.Exists(filePath))
        {
            // Read the JSON file from the specified path
            jsonString = File.ReadAllText(filePath);
            Debug.Log("jsonstring: " + jsonString);
        }
        else
        {
            Debug.Log("No save file found.");
            return;
        }
        
        // Parse the JSON string and load the data
        DataManager.Instance.PlayerDataFromJson(jsonString);
    }

    public void SaveGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");

        // Convert the data to JSON format
        string jsonString = JsonUtility.ToJson(DataManager.Instance);
        
        // Save the JSON string to the specified path
        File.WriteAllText(filePath, jsonString);
        
        Debug.Log("Data saved to: " + filePath);
    }

    public virtual void ClearData()
    {
        DataManager.Instance.ClearJson();
        this.SaveGame();
    }
}
