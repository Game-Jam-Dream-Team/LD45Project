using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemSpawn : StartStep {
	class Entry {
		public Transform Transform;
		public Vector3 StartPosition;
		public Vector3 EndPosition;
		public float StartTime;
		public bool Started;
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
			entry.StartTime = Mathf.Lerp(0.25f, 1, ((float)i)/_entries.Length);
		}
	}

	protected override void OnStart() {
		foreach ( var entry in _entries ) {
			entry.Transform.SetParent(null);
		}
	}

	protected override void Perform(float t) {
		foreach ( var entry in _entries ) {
			if ( t < entry.StartTime ) {
				continue;
			}
			if ( !entry.Started ) {
				entry.StartPosition = Root.transform.position;
				entry.Started = true;
			}
			var entryTime = (t - entry.StartTime) / (1 - entry.StartTime);
			entry.Transform.position = Vector3.Lerp(entry.StartPosition, entry.EndPosition, entryTime);
		}
	}

	Entry ToEntry(GameObject go) {
		var trans = go.transform;
		return new Entry { Transform = trans, EndPosition = trans.position };
	}
}