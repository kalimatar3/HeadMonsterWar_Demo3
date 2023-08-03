using UnityEngine;

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
        base.Start();
        this.LoadGameData();
    }
    protected virtual string GetSaveName(string name)
    {
        return Lsmanager.SAVE + "_" + name;
    }
    public virtual void LoadGameData()
    {        
        string JsonString = SaveSystem.GetString(this.GetSaveName("DataManager"));
        DataManager.Instance.PlayerDataFromJson(JsonString);
        Debug.Log("jsonstring : " + JsonString);
    }
    public virtual void SaveGame()
    {
        string JsonString = JsonUtility.ToJson(DataManager.Instance);
        SaveSystem.SetString(this.GetSaveName("DataManager"),JsonString);
        Debug.Log(JsonString);
    }
    public virtual void ClearData()
    {
        DataManager.Instance.ClearJson();
        this.SaveGame();
    }
}
