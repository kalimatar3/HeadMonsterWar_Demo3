using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MyBehaviour
{
    protected override void Start()
    {
        base.Start();
        SceneManager.LoadScene("Gameplay");
    } 
}
