using UnityEngine;

public class RandomBackground : MonoBehaviour {
	public float StepTime;
	public AnimationCurve Curve;

	SpriteRenderer[] _renderers;

	float _timer;
	int _index;

	void Awake() {
		_renderers = GetComponentsInChildren<SpriteRenderer>();
		SelectNextRenderer();
	}

	void Update() {
		_timer += Time.deltaTime * (1 / StepTime);
		var color = _renderers[_index].color;
		color.a = Curve.Evaluate(_timer);
		_renderers[_index].color = color;
		if ( _timer > 1.0f ) {
			SelectNextRenderer();
			_timer = 0;
		}
	}

	void SelectNextRenderer() {
		_index = Random.Range(0, _renderers.Length);
		for ( var i = 0; i < _index; i++ ) {
			_renderers[i].enabled = (i == _index);
		}

	}
}
