using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadNextSceneButton : MonoBehaviour {
	void Start() {
		GetComponent<Button>().onClick.AddListener(LoadFirstScene);
	}

	void LoadFirstScene() => SceneController.Instance.FirstLevel();
}
