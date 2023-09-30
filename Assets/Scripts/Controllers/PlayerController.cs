using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst;

    [SerializeField] private MovementController playerMovement = null; public MovementController MoveController { get { return playerMovement; } }

	private void Awake() {
        Inst = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (direction != Vector2.zero) {
            MoveController.Move(direction, Input.GetKey(KeyCode.LeftShift));
        }
    }
}
