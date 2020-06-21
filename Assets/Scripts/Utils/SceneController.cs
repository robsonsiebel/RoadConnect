using UnityEngine.SceneManagement;

public enum GameScene { Main }

public static class SceneController
{

    public static void RequestLoadScene(GameScene sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    public static void RequestLoadAdd(GameScene sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad.ToString(), LoadSceneMode.Additive);
    }

    public static void RequestLoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}