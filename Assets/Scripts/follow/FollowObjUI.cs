using UnityEngine;

public class FollowObjUI : followObj
{
    [SerializeField] protected Camera ThisCam;
    protected override void follow()
    {
        Vector3 FakeObjPos = (Obj.transform.position - PlayerController.Instance.transform.position).normalized * 10f + PlayerController.Instance.transform.position ;
        Vector3 ObjScreenPos = RectTransformUtility.WorldToScreenPoint(ThisCam,Obj.transform.position);
        Vector2 fakeObjScreenPOs = RectTransformUtility.WorldToScreenPoint(ThisCam,FakeObjPos);
        Vector2 CenterPos = new Vector3(1920/2,1080/2);
        if( Mathf.Abs(ObjScreenPos.x - CenterPos.x ) < (1920)/2 && Mathf.Abs(ObjScreenPos.y - CenterPos.y ) < (1080)/2)
        {
            this.transform.parent.position = ObjScreenPos + new Vector3(0,250,0);
            this.transform.rotation = new Quaternion(180,0,0,0);
        }
        else
        {
            this.transform.parent.position = CenterPos  + (fakeObjScreenPOs - CenterPos).normalized * 350;
            this.transform.up = (fakeObjScreenPOs - CenterPos).normalized;
        }
    }
}
