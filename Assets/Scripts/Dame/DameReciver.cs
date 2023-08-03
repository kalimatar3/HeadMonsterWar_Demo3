using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class DameReciver : MyBehaviour
{
    [SerializeField] public float MaxHp,CurrentHp;
    public virtual void DeductHp(float dame)
    {
        CurrentHp -= dame;
        if(CurrentHp <= 0) CurrentHp = 0;
    }
    public virtual void RestoreHp(float dame)
    {
        CurrentHp += dame;
        if(CurrentHp >= MaxHp) CurrentHp = MaxHp;
    }
    public virtual void ReBorn()
    {
        this.CurrentHp = this.MaxHp;
    }
    protected virtual void OnEnable()
    {
        this.ReBorn();
    }
    protected virtual void Dead()
    {
        if(!Candead()) return;
        else StartCoroutine(DelayDeath());
    }
    protected IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(0.5f);
        this.Dying();
    }
    protected virtual void Dying()
    {
        //override
    }
    protected void FixedUpdate()
    {
        this.Dead();
    }
    protected virtual bool Candead()
    {
        if(this.CurrentHp > 0 ) return false;
        return true;
    }
}
