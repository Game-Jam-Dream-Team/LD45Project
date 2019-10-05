using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CObstacle : CAbstractCollisionZone
{
    protected override void ProcessPlayerCollision(PlayerMoveScript player)
    {
        StartCoroutine(CollisionCoroutine(player));

    }

    private IEnumerator CollisionCoroutine(PlayerMoveScript player)
    {
        player.playerSpriteHide();
        player.playDeathSound();
        yield return new WaitForSeconds(1f);
        SceneController.Instance.ReloadCurrent();
    }
}
