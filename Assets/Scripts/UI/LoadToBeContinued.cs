using UnityEngine;

public class LoadToBeContinued : MonoBehaviour {
	void Update() {
		if ( Input.anyKey ) {
			SceneController.Instance.ToMenu();
		}
	}
}
