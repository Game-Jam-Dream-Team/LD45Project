using UnityEngine;

public class SceneControllerInterface : MonoBehaviour
{
    private SceneController _sceneController;

    private void Awake()
    {
        _sceneController = SceneController.Instance;
    }

    public void ToMenu()
    {
        _sceneController.ToMenu();
    }

    public void ReloadCurrent()
    {
        _sceneController.ReloadCurrent();
    }

    public void NextLevel()
    {
        _sceneController.NextLevel();
    }
}
