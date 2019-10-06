using UnityEngine;

public class CFinishZone : CAbstractCollisionZone
{
    private GameObject ResultPanel;
    private static GameObject ResultPanelPrefab;

    protected override void ProcessPlayerCollision(PlayerMoveScript player)
    {
        player.gameObject.SetActive(false);
        FinishLevel();
    }

    private void FinishLevel()
    {
        if (ResultPanelPrefab == null)
        {
            ResultPanelPrefab = Resources.Load<GameObject>("ResultPanel");
        }

        if (ResultPanel == null)
        {
            ResultPanel = Instantiate(ResultPanelPrefab);
        }

        ResultPanel.SetActive(true);
    }
}
