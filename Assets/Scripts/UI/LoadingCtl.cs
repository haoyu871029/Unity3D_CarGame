using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingCtl : MonoBehaviour
{
    public static LoadingCtl self;
    private void Awake()
    {
        self = this;
    }

    public void Loading()
    {
        GetComponent<Animation>().Play("FadeOut");
        Invoke("ChangeScene", 1f);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(0);      // 更換到編號0的場景 也可以寫SceneManager.LoadScene("Main")
    }
}
