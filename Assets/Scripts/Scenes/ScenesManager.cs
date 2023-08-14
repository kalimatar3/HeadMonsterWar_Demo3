using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class ScenesManager : MyBehaviour
{
    
    protected static ScenesManager instance;
    public static ScenesManager Instance { get => instance;}
    [SerializeField] protected Transform LoadingScene;
    [SerializeField] protected Slider LoadingSlider;
    protected override void Awake()
    {
        base.Awake();
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance =this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void Start()
    {
        base.Start();
        ScenesManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
    } 
    public async void LoadScene(string SceneName)
    {
       var Scene = SceneManager.LoadSceneAsync(SceneName);
       Scene.allowSceneActivation = false;
       LoadingScene.gameObject.SetActive(true);
        do
        {
            LoadingSlider.value = Scene.progress;
            await Task.Delay(200);
        } while(!this.Canload());
       Scene.allowSceneActivation = true;
       LoadingScene.gameObject.SetActive(false);
   }
   protected bool Canload()
   {
    if(DataManager.Instance.CurrentMap == null) return false;
    if(MapManager.Instance.ListBossSapwnPos == null) return false;
    return true;
   }
}
