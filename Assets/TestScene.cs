using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScene : MonoBehaviour
{
    public bool IsLoaded;

    // Update is called once per frame
    void Update()
    {
        IsLoaded = SceneManager.GetSceneByName("Scene_Global").isLoaded;
    }
}
