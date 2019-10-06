using System;
using UnityEngine;

[Serializable]
public class PlayerSpawn : StartStep {
	public Transform Root;
	public AudioSource Sound;

    float tilt = 3f;



    Transform _target;
	Vector3 _endPosition;

	public void Init() {
		var go = GameObject.FindGameObjectWithTag("player");
		go.GetComponent<Collider2D>().enabled = false;
		go.GetComponentInChildren<DirectionPointer>().Hide();

		foreach ( var renderer in go.GetComponentsInChildren<Renderer>() ) {
			renderer.enabled = false;
		}

        go.GetComponent<RotatePlayerToCursor>().enabled = false;

		_target = go.transform;
		_endPosition = _target.position;
		_target.SetParent(Root);
		_target.localPosition = Vector3.zero;

    }

	protected override void Perform(float t) {
		var startPosition = Root.transform.position;
		_target.position = Vector3.Lerp(startPosition, _endPosition, t);

        _target.transform.Rotate(Vector3.forward * tilt);

    }

	protected override void OnStart() {
		foreach ( var renderer in _target.GetComponentsInChildren<Renderer>() ) {
			renderer.enabled = true;
		}
		Sound.Play();
	}

	protected override void OnFinish() {
		_target.GetComponent<Collider2D>().enabled = true;
		_target.GetComponentInChildren<DirectionPointer>().Show();
        _target.GetComponent<RotatePlayerToCursor>().enabled = true;

        foreach (GameObject s in GameObject.FindGameObjectsWithTag("spawner")) { s.GetComponent<ObstaclesSpawner>()?.Activate(); }
    }
}