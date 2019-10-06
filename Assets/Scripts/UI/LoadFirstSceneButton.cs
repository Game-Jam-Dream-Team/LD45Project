using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadFirstSceneButton : MonoBehaviour {
	void Start() {
		GetComponent<Button>().onClick.AddListener(LoadFirstScene);
	}

	void LoadFirstScene() => SceneManager.LoadScene(1);
}
