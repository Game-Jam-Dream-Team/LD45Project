using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemSpawn : StartStep {
	class Entry {
		public Transform Transform;
		public Vector3 Position;
	}

	public Transform Root;

	Entry[] _entries;

	public void Init() {
		var objects = GameObject.FindGameObjectsWithTag("object");
		_entries = objects.Select(ToEntry).ToArray();
		foreach ( var entry in _entries ) {
			entry.Transform.SetParent(Root);
			entry.Transform.localPosition = Vector3.zero;
		}
	}

	protected override void Perform(float t) {
		var startPosition = Root.transform.position;
		foreach ( var entry in _entries ) {
			entry.Transform.position = Vector3.Lerp(startPosition, entry.Position, t);
		}
	}

	Entry ToEntry(GameObject go) {
		var trans = go.transform;
		return new Entry { Transform = trans, Position = trans.position };
	}
}