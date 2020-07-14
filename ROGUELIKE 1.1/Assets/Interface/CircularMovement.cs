using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour {

	[SerializeField]
	Transform rotationCenter;

	[SerializeField]
	float rotationRadius, angularSpeed;

	float posX, posY, angle = 0f;

	// Update is called once per frame
	void Update () {
        rotationCenter = Camera.main.transform;
        rotationRadius = 20f;
        angularSpeed = -0.01f;

        posX = rotationCenter.position.x + Mathf.Cos (angle) * rotationRadius;
		posY = rotationCenter.position.y + Mathf.Sin (angle) * rotationRadius;
		transform.position = new Vector3 (posX, posY, transform.position.z);
		angle = angle + Time.deltaTime * angularSpeed;

		if (angle >= 360f)
			angle = 0f;
	}
}
