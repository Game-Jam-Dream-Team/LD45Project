using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DirectionPointer: MonoBehaviour {
	public float Length;

	LineRenderer _line;

	void Awake() {
		_line = GetComponent<LineRenderer>();
	}

	void Update()
    {
		var position = new Vector2(transform.position.x, transform.position.y);
        var mousePos = (Vector2)DirectionUtils.GetCurrentMousePosition();
		_line.SetPosition(0, position);
        var playerToMousePosVector = mousePos - position;
        _line.SetPosition(1, position + (-playerToMousePosVector * Length));
	}
}