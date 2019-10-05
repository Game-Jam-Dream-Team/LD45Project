using System.Linq;
using UnityEngine;

public class GameStarter : MonoBehaviour {
	public ShipMovement Ship;
	public ItemSpawn Item;
	public PlayerSpawn Player;

	void Awake() {
		if ( SceneController.Instance.WasReloaded ) {
			Ship.Target.transform.position = Ship.EndPosition.position;
            Ship.EngineParticles.Stop();
			enabled = false;

            foreach (GameObject s in GameObject.FindGameObjectsWithTag("spawner")) { s.GetComponent<ObstaclesSpawner>()?.Activate(); }

            return;
		}
		Item.Init();
		Player.Init();
	}

	void Update() {
		if ( (Ship.Update() || Item.Update()) && Player.Update() ) {
			enabled = false;
		}
	}
}
