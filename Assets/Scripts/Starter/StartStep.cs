using UnityEngine;

public abstract class StartStep {
	public AnimationCurve Curve;
	public float TotalTime;

	bool _finished;
	float _timer;

	public bool Update() {
		if ( _timer >= 1.0f ) {
			if ( !_finished ) {
				OnFinish();
			}
			_finished = true;
			return true;
		}
		var t = Curve.Evaluate(_timer);
		Perform(t);
		_timer += Time.deltaTime * (1 / TotalTime);
		return false;
	}

	protected abstract void Perform(float t);

	protected virtual void OnFinish() {}
}