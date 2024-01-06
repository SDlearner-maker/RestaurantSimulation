using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGuide : MonoBehaviour
{
    public void Gotoguidescene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
