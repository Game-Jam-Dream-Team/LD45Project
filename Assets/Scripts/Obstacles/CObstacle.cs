using UnityEngine.SceneManagement;

public class CObstacle : CAbstractCollisionZone
{
    protected override void ProcessPlayerCollision(PlayerMoveScript player)
    {
        SceneController.Instance.ReloadCurrent();
    }
}
