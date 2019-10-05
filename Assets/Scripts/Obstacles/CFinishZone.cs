using UnityEngine.SceneManagement;

public class CFinishZone : CAbstractCollisionZone
{
    protected override void ProcessPlayerCollision()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
