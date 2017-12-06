using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParallax2D : MonoBehaviour {
	public float backgroundSize;
	public float parallaxSpeedX;
	public float parallaxSpeedY;
	

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 10;
	private int leftIndex;
	private int rightIndex;
private float lastCameraX;
private float lastCameraY;
	private void Start() {
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		lastCameraY = cameraTransform.position.y;

		layers = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			layers[i] = transform.GetChild(i);

			leftIndex = 0;
			rightIndex = layers.Length-1;
	}

private void Update() {
	float deltaX = cameraTransform.position.x - lastCameraX;
	transform.position += Vector3.right * (deltaX * parallaxSpeedX);
	lastCameraX = cameraTransform.position.x;

	float deltaY = cameraTransform.position.y - lastCameraY;
	transform.position += Vector3.up * (deltaY * parallaxSpeedY);
	lastCameraY = cameraTransform.position.y;

	if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone)) {
		ScrollLeft();
	}
	if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone)) {
		ScrollRight();
	}
}

	private void ScrollLeft() {
		var newPos = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
		// Parallax offset
		newPos.y += transform.position.y;
		newPos.z = transform.position.z;
		layers[rightIndex].position = newPos;
		leftIndex = rightIndex;
		rightIndex--;
		if (rightIndex < 0)
			rightIndex = layers.Length-1;
	}

	private void ScrollRight() {
		var newPos = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
		newPos.y += transform.position.y;
		newPos.z = transform.position.z;
		layers[leftIndex].position = newPos;
		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == layers.Length)
			leftIndex = 0;
	}
}
