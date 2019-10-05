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
        player.PlayDeathEffect();
        player.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        SceneController.Instance.ReloadCurrent();
    }
}
