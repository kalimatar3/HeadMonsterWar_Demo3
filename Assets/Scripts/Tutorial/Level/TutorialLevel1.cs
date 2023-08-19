using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel1 : TutorialLevel
{
    [SerializeField] protected Transform HpberHolder;
    protected override void Start()
    {
        base.Start();
        HpberHolder.gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        this.StartCoroutine(this.Flow());
    }
    protected IEnumerator Flow()
    {
        yield return new  WaitUntil(predicate:()=>
        {
            if(TutorialUI.Instance == null) return false;
            return true;
        });
        yield return new  WaitUntil(predicate:()=>
        {
           if(CameraManager.Instance.transform.position != PlayerController.Instance.transform.position + CameraManager.Instance.CameraFollow.DefaultCamPOS) return false;
           return true;
        });
        DataManager.Instance.FistCamMove = true;
        PanelCtrl.Instance.ShowPanel("GameplayPanel");
        PanelCtrl.Instance.HirePanel("MainMenuPannel");
        HpberHolder.gameObject.SetActive(true);
        PlayerController.Instance.PlayerReciver.CurrentHp = 60f;
        TutorialUI.Instance.ActiveButton(0);
        TutorialUI.Instance.ActivePanel(0);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.PauseGame();
        yield return new  WaitUntil(predicate:()=>
        {
            Vector3 move = new Vector3(InputManager.Instance.MovingJoystick.Horizontal,0,InputManager.Instance.MovingJoystick.Vertical);
            if(move.magnitude <= 0)  return false;
            return true;
        }); 
        GameManager.Instance.ResumeGame();
        TutorialUI.Instance.DeActivePanel();
        yield return new WaitForSeconds(1f);
        Transform Hpheal = BuffSpawner.Instance.Spawn("HPheal",new Vector3(35,0,-35),Quaternion.identity);
        Hpheal.GetComponentInChildren<BuffDespawn>().DespawnTime = 1000000000000000f;
        TutorialUI.Instance.ActiveTutorialPoint();
        TutorialUI.Instance.SetCollorTutorialPoint(Hpheal,Color.yellow);
        TutorialUI.Instance.SetMassage("Pick it UP",Color.white);
        
        yield return new WaitUntil(predicate:()=>
        {
            if(PlayerController.Instance.PlayerReciver.CurrentHp < 100 ) return false;
            return true;
        });
        TutorialUI.Instance.DeActiveTutorialPoint();
        yield return new WaitForSeconds(1f);
        TutorialUI.Instance.ActivePanel(1);
        TutorialUI.Instance.ActiveButton(1);
        GameManager.Instance.PauseGame();

        yield return new WaitUntil(predicate:()=>
        {
            Vector3 move = new Vector3(InputManager.Instance.Shootingstick.Horizontal,0,InputManager.Instance.Shootingstick.Vertical);
            if(move.magnitude <= 0)  return false;
            return true;
        });
        GameManager.Instance.ResumeGame();
        TutorialUI.Instance.DeActivePanel();
        Transform Enemy = EnemiesSpawner.Instance.Spawn("Enemie_01", PlayerController.Instance.transform.position + new Vector3(0,0,-20),Quaternion.identity);        
        TutorialUI.Instance.ActiveTutorialPoint();
        TutorialUI.Instance.SetCollorTutorialPoint(Enemy,Color.red);
        TutorialUI.Instance.SetMassage("Shoot the Skibidi Toilet!",Color.white);

        yield return new WaitUntil(predicate:()=>
        {
            if(Enemy.gameObject.activeInHierarchy)  return false;
            return true;
        });
        TutorialUI.Instance.DeActiveTutorialPoint();
        yield return new WaitForSeconds(1f);
        PanelCtrl.Instance.ShowPanel("CoinPanel");
        Lsmanager.Instance.SaveGame();
        this.IsDone = true;
    }
}
