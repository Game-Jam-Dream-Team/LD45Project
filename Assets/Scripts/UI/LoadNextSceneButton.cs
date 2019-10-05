using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadNextSceneButton : MonoBehaviour {
	void Start() {
		GetComponent<Button>().onClick.AddListener(LoadNextScene);
	}

	void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
