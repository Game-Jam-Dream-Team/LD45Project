using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public enum tutorialButtonType { playerInit, objectGrabbed}
    public tutorialButtonType Type;
    int counter = 0;
    PlayerMoveScript player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        if (Type == tutorialButtonType.playerInit)
        {
            PlayerSpawn.OnPlayerInitFinish.AddListener(() => gameObject.SetActive(true));
        }
        else
        {

            PlayerMoveScript.OnObjectgrabbed.AddListener(() => 
            {
                counter++;
                if (counter == 2)
                {
                    gameObject.SetActive(true);
                    GetComponent<SpriteRenderer>().enabled = true;
                }
            });
        }

        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMoveScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Type == tutorialButtonType.objectGrabbed)
        {
            transform.Translate(player.playerSpeed*Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);

        }

    }
}
