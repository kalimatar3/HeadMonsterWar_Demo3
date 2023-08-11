using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MyBehaviour
{
    protected void OnEnable()
    {
        StartCoroutine(DelayReplayByWin());
    } 
    protected void Replaying()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    protected IEnumerator DelayReplayByWin()
    {        
        SoundSpawner.Instance.Spawn(CONSTSoundsName.Win,Vector3.zero,Quaternion.identity);
        PanelCtrl.Instance.HirePanel("GameplayPanel");
        yield return new WaitForSeconds(5f);
        this.Replaying();
    } 
}
