using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DirectionPointer: MonoBehaviour {
	public float Length;

	LineRenderer _line;

	void Awake() {
		_line = GetComponent<LineRenderer>();
	}

	void Update() {
		var position = DirectionUtils.GetMouseDirectionTo(transform.position);
		var inversedPosition = new Vector2(-position.x, -position.y);
		var target = new Vector3(inversedPosition.x, inversedPosition.y, 0);
		_line.SetPosition(1, target * Length);
	}
}