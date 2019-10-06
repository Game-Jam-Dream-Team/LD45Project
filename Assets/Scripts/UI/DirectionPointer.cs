using UnityEngine;

public class DirectionPointer: MonoBehaviour {
	public float TimeToChange;
    private bool _isHidden = false;

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
        _isHidden = false;
        Lines[_index].enabled = true;
	}

	public void Hide() {
        _isHidden = true;

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
        if (_isHidden)
        {
            return;
        }

		_index++;
		if ( _index == Lines.Length ) {
			_index = 0;
		}
		Hide();
		Show();
	}
}