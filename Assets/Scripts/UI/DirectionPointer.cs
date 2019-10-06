using UnityEngine;

public class DirectionPointer: MonoBehaviour {
	public float TimeToChange;

	SpriteRenderer[] _lines;

	SpriteRenderer[] Lines {
		get {
			if ( _lines == null ) {
				_lines = GetComponentsInChildren<SpriteRenderer>();
			}
			return _lines;
		}
	}

	int _index = 0;

	public void Show() {
		Lines[_index].enabled = true;
	}

	public void Hide() {
		foreach ( var line in Lines ) {
			line.enabled = false;
		}
	}

	float _timer;

	void Update() {
	    _timer += Time.deltaTime * (1 / TimeToChange);
	    if ( _timer > 1.0f ) {
		    _timer = 0.0f;
		    Change();
	    }
	}

	void Change() {
		_index++;
		if ( _index == Lines.Length ) {
			_index = 0;
		}
		Hide();
		Show();
	}
}