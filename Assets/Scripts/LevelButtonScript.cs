using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonScript : MonoBehaviour
{
    private int sceneNum;


    public int SceneNum
    {
        get { return sceneNum; }
        set { sceneNum = value; }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneNum);
    }
}
