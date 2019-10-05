using UnityEngine.SceneManagement;

public class CObstacle : CAbstractCollisionZone
{
    protected override void ProcessPlayerCollision()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
