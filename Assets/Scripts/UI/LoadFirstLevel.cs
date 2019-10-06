using UnityEngine;

public class LoadFirstLevel : MonoBehaviour {
	void Update() {
		if ( Input.anyKey ) {
			SceneController.Instance.FirstLevel();
		}
	}
}
