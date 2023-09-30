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

    void FixedUpdate() {
		Vector2 direction = new Vector2(-1f * Input.GetAxisRaw("Horizontal"), -1f * Input.GetAxisRaw("Vertical"));
		bool sprinting = Input.GetKey(KeyCode.LeftShift);
		MoveController.Move(direction, sprinting);
	}
}
