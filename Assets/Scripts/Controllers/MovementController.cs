using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
	[SerializeField] private Animator _anim = null; public Animator animator { get { return _anim; } }

	[SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float turnSpeed = 7f;

    public Action<Vector3> NewPosition;

	public float currentSpeed { get; private set; } = 0f;

	private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
	}

	public void Move(Vector2 direction, bool isRunning = false) {

		if (direction != Vector2.zero) {
			currentSpeed = isRunning ? runSpeed : moveSpeed;
			rigidBody.AddForce(new Vector3(direction.x, 0f, direction.y) * currentSpeed - rigidBody.velocity, ForceMode.VelocityChange);
			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(direction.x, 0f, direction.y), turnSpeed * Time.fixedDeltaTime, 0f), Vector3.up);

			NewPosition?.Invoke(rigidBody.position);
		}
		else {
			currentSpeed = 0f;
			rigidBody.velocity = Vector3.zero;
		}

		_anim.SetFloat("speed", currentSpeed);
	}

}
