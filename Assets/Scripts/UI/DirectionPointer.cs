using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DirectionPointer: MonoBehaviour {
	public float Length;

	LineRenderer _line;

	LineRenderer Line {
		get {
			if ( !_line ) {
				_line = GetComponent<LineRenderer>();
			}
			return _line;
		}
	}

	public void Show() {
		Line.enabled = true;
	}

	public void Hide() {
		Line.enabled = false;
	}

	void Update()
    {
		var position = new Vector2(transform.position.x, transform.position.y);
        var mousePos = (Vector2)DirectionUtils.GetCurrentMousePosition();
		Line.SetPosition(0, position);
        var playerToMousePosVector = (mousePos - position).normalized;
        Line.SetPosition(1, position + (-playerToMousePosVector * Length));
	}
}