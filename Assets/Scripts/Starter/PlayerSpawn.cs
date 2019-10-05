using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerSpawn : StartStep {
	public Transform Root;

	Transform _target;
	Vector3 _endPosition;

	public void Init() {
		var go = GameObject.FindGameObjectWithTag("player");
		go.GetComponent<Collider2D>().enabled = false;
		_target = go.transform;
		_endPosition = _target.position;
		_target.SetParent(Root);
		_target.localPosition = Vector3.zero;
	}

	protected override void Perform(float t) {
		var startPosition = Root.transform.position;
		_target.position = Vector3.Lerp(startPosition, _endPosition, t);
	}

	protected override void OnFinish() {
		_target.GetComponent<Collider2D>().enabled = true;
	}
}