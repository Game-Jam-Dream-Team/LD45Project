using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;

    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("SceneManager");
                var manager = go.AddComponent<SceneController>();
                DontDestroyOnLoad(go);
                _instance = manager;
            }

            return _instance;
        }
    }

    public void FirstLevel() {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        var current = SceneManager.GetActiveScene().name;
        var lvlNumber = int.Parse(Regex.Match(current, @"(\d+)").Value);
        lvlNumber++;

        var sceneName = $"Level{lvlNumber}";
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReloadCurrent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
