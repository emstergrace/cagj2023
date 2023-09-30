using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float turnSpeed = 7f;

    public Action<Vector3> NewPosition;

	private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
	}

	public void Move(Vector2 direction, bool isRunning = false) {
        rigidBody.MovePosition(rigidBody.position + new Vector3(direction.x, 0f, direction.y) * (isRunning ? runSpeed : moveSpeed) * Time.fixedDeltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(direction.x, 0f, direction.y), turnSpeed * Time.fixedDeltaTime, 0f), Vector3.up);

        NewPosition?.Invoke(rigidBody.position);
	}

}
