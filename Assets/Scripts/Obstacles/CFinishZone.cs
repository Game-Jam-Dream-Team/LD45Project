﻿using UnityEngine;

public class CFinishZone : CAbstractCollisionZone
{
    private GameObject ResultPanel;
    private static GameObject ResultPanelPrefab;

    protected override void ProcessPlayerCollision(PlayerMoveScript player)
    {
        player.PlayerHide();
        player.PlayWinEffect();
        player.StopMoving();
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

        var source = GetComponent<AudioSource>();
        if ( source ) {
            source.Play();
        }
    }
}
