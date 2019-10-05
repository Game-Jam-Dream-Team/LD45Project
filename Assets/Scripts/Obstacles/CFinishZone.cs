using UnityEngine;

public class CFinishZone : CAbstractCollisionZone
{
    public GameObject ResultPanel;

    protected override void ProcessPlayerCollision()
    {
        FinishLevel();
    }

    private void FinishLevel()
    {
        ResultPanel.SetActive(true);
    }
}
