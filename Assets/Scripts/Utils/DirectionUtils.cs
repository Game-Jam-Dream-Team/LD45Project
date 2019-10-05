using UnityEngine;

public static class DirectionUtils {
	static Camera _mainCamera;

	static Camera MainCamera {
		get {
			if ( !_mainCamera ) {
				_mainCamera = Camera.main;
			}
			return _mainCamera;
		}
	}

	public static Vector2 GetMouseDirectionTo(Vector3 worldPosition) {
		var screenPosition = MainCamera.WorldToScreenPoint(worldPosition);
		var direction = Input.mousePosition - screenPosition;
		return direction.normalized;
	}
}