using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VRSceneManager : MonoBehaviour
{
    public static VRSceneManager instance;
    public int sceneIndex = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeScenes(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ChangeScenes(VRScenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void ChangeScenes()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
    }
}

[System.Serializable]
public enum VRScenes
{
    Maze,
    Alpha,
    Beta,
    Delta,
    Echo,
    Gamma,
    Nakisa4prez
}
