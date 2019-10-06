using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Analytics;
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

    public bool WasReloaded { get; private set; }

    int LevelIndex { get; set; }
    int Tries { get; set; }

    public void FirstLevel() {
        AnalyticsEvent.FirstInteraction();
        OnStartLevel(LevelIndex, false);
        SceneManager.LoadScene(2);
    }

    public void NextLevel() {
        var current = SceneManager.GetActiveScene().name;
        var lvlNumber = int.Parse(Regex.Match(current, @"(\d+)").Value);
        lvlNumber++;

        var sceneName = $"Level{lvlNumber}";
        if (Application.CanStreamedLevelBeLoaded(sceneName)) {
            OnStartLevel(LevelIndex, false);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            ToMenu();
        }
    }

    public void ToMenu() {
        Tries = 0;
        SceneManager.LoadScene("Menu");
    }

    public void ReloadCurrent() {
        OnStartLevel(LevelIndex, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnStartLevel(int levelIndex, bool reload) {
        LevelIndex = levelIndex;
        WasReloaded = reload;
        if ( WasReloaded ) {
            Tries = 0;
            AnalyticsEvent.GameOver($"Level{LevelIndex}");
        } else {
            Tries++;
        }
        var dict = new Dictionary<string, object> { { "tries", Tries } };
        AnalyticsEvent.LevelStart($"Level{LevelIndex}", dict);
    }
}
