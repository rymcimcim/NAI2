using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector3 Origin;
	private Vector3 Diference;
	private bool Drag = false;

	public void ZoomIn()
	{
		Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - 1, 2);
	}

	public void ZoomOut()
	{
		Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + 1, 20);
	}

	void LateUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
			if (Drag == false)
			{
				Drag = true;
				Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}
		else
		{
			Drag = false;
		}
		if (Drag == true)
		{
			Camera.main.transform.position = Origin - Diference;
		}
	}
}
