using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPointCtrl : MyBehaviour
{
    [SerializeField] protected Transform point;
    public Transform Point => point;
    [SerializeField] protected Text message;
    public Text Message => message;
    [SerializeField] protected Transform target;
    public Transform TarGet => target;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMessage();
    }  
    protected void LoadMessage()
    {
        foreach(Transform element in this.transform )
        {
            if(element.GetComponent<Text>())
            {
                message = element.GetComponent<Text>();
            }
        }
    }
    public void Sendmessage(string Message)
    {
        if(this.Message == null) return;
        this.message.text = Message;
    }
    public void SetPointPos(Vector3 Pos)
    {
        this.TarGet.position = Pos;
    }
}
 