using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DirectionPointer: MonoBehaviour {
	public float Length;

	LineRenderer _line;

	void Awake() {
		_line = GetComponent<LineRenderer>();
	}

	void Update() {
		var position = transform.position;
		var mouseDirection = DirectionUtils.GetMouseDirectionTo(position);
		var inversedDirection = new Vector2(-mouseDirection.x, -mouseDirection.y);
		var target = new Vector3(inversedDirection.x, inversedDirection.y, 0);
		_line.SetPosition(0, new Vector3(position.x, position.y, 0));
		_line.SetPosition(1, target * Length);
	}
}