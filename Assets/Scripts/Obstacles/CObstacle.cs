using UnityEngine.SceneManagement;

public class CObstacle : CAbstractCollisionZone
{
    protected override void ProcessPlayerCollision()
    {
        SceneController.Instance.ReloadCurrent();
    }
}
