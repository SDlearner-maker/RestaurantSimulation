using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void Gotogamescene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
