using UnityEngine;

namespace CityBuilder.Scripts
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour
	{

		public float MinZoom = 2.5f;
		public float MaxZoom = 15f;
		
		private Camera _camera;

		private Vector3 _mouseOriginPoint;
		private bool _dragging;

		private void Awake()
		{
			_camera = GetComponent<Camera>();
		}

		private void LateUpdate()
		{
			_camera.orthographicSize  = Mathf.Clamp(
				_camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * _camera.orthographicSize,
				MinZoom,
				MaxZoom
			);
			if (Input.GetMouseButton(2))
			{
				Vector3 currentMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
				if (!_dragging)
				{
					_dragging = true;
					_mouseOriginPoint = currentMousePosition;
				}
				Vector3 offset = currentMousePosition - transform.position;
				transform.position = _mouseOriginPoint - offset;
			}
			else
			{
				_dragging = false;
			}
		}
	}
}
