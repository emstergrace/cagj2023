using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController Inst;

    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private GameObject cameraCenterPosition = null;

    [SerializeField] private int minFOV = 30;
    [SerializeField] private int maxFOV = 60;
    [SerializeField] private int defaultFOV = 60;
    [SerializeField] private int zoomSpeed = 4;
    [SerializeField] private float swivelSpeed = 10f;

    private bool isSwiveling = false;
    private float initMPSwivel = 0f; // init mouse position swivel

	private void Awake() {
        Inst = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        mainCamera.fieldOfView = defaultFOV;

        PlayerController.Inst.MoveController.NewPosition += UpdateCameraPosition;
        UpdateCameraPosition(PlayerController.Inst.MoveController.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero) {
            float newFOV = mainCamera.fieldOfView - Input.mouseScrollDelta.y * zoomSpeed;

            if (newFOV < minFOV) mainCamera.fieldOfView = minFOV;
            else if (newFOV > maxFOV) mainCamera.fieldOfView = maxFOV;
            else mainCamera.fieldOfView = newFOV;
		}
    }

    private void UpdateCameraPosition(Vector3 pos) {
        cameraCenterPosition.transform.position = new Vector3(pos.x, 0f, pos.z);
    }
}
