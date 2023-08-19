using System.Collections;
using UnityEngine;
using DG.Tweening;
public class CameraFollow : followObj
{
    [SerializeField] public Vector3 DefaultCamPOS,FireCamPOS,CutSceneCamPos;
    [SerializeField] protected Quaternion DefaultCamROS;
    protected override void Start()
    {
        base.Start();
        this.StartCoroutine(TutorialCam());
        this.Forcus(this.transform.parent,0);
    }
    protected override void follow()
    {           
        if(DataManager.Instance.FistCamMove == false) return;
        if(Obj == null) return;
        if(Obj == PlayerController.Instance.transform)
        {
            Vector3 newPos = Vector3.Lerp(this.transform.parent.position, Obj.transform.position + DefaultCamPOS , this.smooth * Time.deltaTime);
            this.transform.parent.position = newPos;
            if(InputManager.Instance.Shootingstick.Horizontal !=0 ||InputManager.Instance.Shootingstick.Vertical != 0)
            {
                Vector3 NewPOs =  Vector3.Lerp(this.transform.parent.position, Obj.transform.position + FireCamPOS,smooth /2 * Time.deltaTime);
                this.transform.parent.position = NewPOs;
            }
        }
        else
        {
            this.ForcustoBoss();
        }
    }
    protected IEnumerator TutorialCam()
    {
        yield return new WaitUntil(predicate:()=>
        {
            if(DataManager.Instance == null) return false;
            return true;
        });
        if(!DataManager.Instance.FistCamMove)
        {
            yield return new WaitUntil(predicate:()=>
            {
                if(PlayerController.Instance.transform != Obj) return false;
                return true;
            });
            this.transform.parent.DOMove(Obj.transform.position + CutSceneCamPos ,3f);
            yield return new WaitForSeconds(5f);
            this.transform.parent.DOMove(Obj.transform.position + DefaultCamPOS,5f);
        }
    }
    protected void ForcustoBoss()
    {
        Vector3 Direction = (Obj.transform.position - PlayerController.Instance.transform.position).normalized;
        if(Direction.magnitude != 0) this.transform.parent.forward = Direction;
        Vector3 newPos = Vector3.Lerp(this.transform.parent.position, Obj.transform.position - Direction * 10 + Vector3.up * 5, this.smooth * Time.deltaTime);
        this.transform.parent.position = newPos;
    }
    public virtual void Forcus(Transform obj,float time)
    {
        StartCoroutine(this.Forcusing(obj,time));
    }
    protected  IEnumerator Forcusing(Transform obj,float time)
    {
        this.Obj = obj;
        yield return new WaitForSeconds(time);
        this.Obj = PlayerController.Instance.transform;
        this.transform.parent.rotation = DefaultCamROS;
    }
}
