using UnityEngine;

public class GameStarter : MonoBehaviour {
	public ShipMovement Ship;
	public ItemSpawn Item;
	public PlayerSpawn Player;

	void Awake() {
		Item.Init();
		Player.Init();
	}

	void Update() {
		if ( Ship.Update() && Item.Update() && Player.Update() ) {
			enabled = false;
		}
	}
}
