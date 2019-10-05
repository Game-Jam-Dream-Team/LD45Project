using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemSpawn : StartStep {
	class Entry {
		public Transform Transform;
		public Vector3 Position;
		public float StartTime;
	}

	public Transform Root;

	Entry[] _entries;

	public void Init() {
		var objects = GameObject.FindGameObjectsWithTag("object");
		_entries = objects.Select(ToEntry).ToArray();
		for ( var i = 0; i < _entries.Length; i++ ) {
			var entry = _entries[i];
			entry.Transform.SetParent(Root);
			entry.Transform.localPosition = Vector3.zero;
			entry.StartTime = Mathf.Lerp(0, 1, ((float)i)/_entries.Length);
		}
	}

	protected override void OnStart() {
		foreach ( var entry in _entries ) {
			entry.Transform.SetParent(null);
		}
	}

	protected override void Perform(float t) {
		var startPosition = Root.transform.position;
		foreach ( var entry in _entries ) {
			if ( t < entry.StartTime ) {
				continue;
			}
			var entryTime = (t - entry.StartTime) / (1 - entry.StartTime);
			entry.Transform.position = Vector3.Lerp(startPosition, entry.Position, entryTime);
		}
	}

	Entry ToEntry(GameObject go) {
		var trans = go.transform;
		return new Entry { Transform = trans, Position = trans.position };
	}
}