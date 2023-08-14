using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MyBehaviour
{
    [SerializeField] protected RectTransform Target;
        protected void FixedUpdate()
    {
        Vector3 huongsung = Target.transform.position - transform.position;
        this.transform.up = huongsung.normalized;
        this.Target.up = -huongsung.normalized;
    }

}

