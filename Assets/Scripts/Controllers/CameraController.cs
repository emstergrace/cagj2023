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
    [SerializeField] private float zoomSpeed = 0.4f;
    [SerializeField] private float rotSpeed = 1f;
    [SerializeField] private Vector3 zoomOutPos = new Vector3(0f, 10.5f, -11.85f);
    [SerializeField] private float zoomOutRot = 47.6f;

    private Vector3 initPosition = Vector3.zero;
    private float initRotEuler = 0f;

	private void Awake() {
        Inst = this;
        initPosition = transform.localPosition;
        initRotEuler = transform.rotation.eulerAngles.x;
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
        if (Input.mouseScrollDelta.y < 0f) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, zoomOutPos, zoomSpeed * Input.mouseScrollDelta.y * -1f);
            transform.rotation = Quaternion.Euler(Mathf.MoveTowards(transform.rotation.eulerAngles.x, zoomOutRot, rotSpeed * Input.mouseScrollDelta.y * -1f), 180f, 0f);
            //transform.rotation = Quaternion.Euler(Mathf.SmoothStep(initRotEuler, zoomOutRot, (transform.rotation.eulerAngles.x - rotSpeed * Input.mouseScrollDelta.y) / zoomOutRot), 180f, 0f);

		}
        else if (Input.mouseScrollDelta.y > 0f) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initPosition, zoomSpeed * Input.mouseScrollDelta.y);
            transform.rotation = Quaternion.Euler(Mathf.MoveTowards(transform.rotation.eulerAngles.x, initRotEuler, rotSpeed * Input.mouseScrollDelta.y), 180f, 0f);
            //transform.rotation = Quaternion.Euler(Mathf.SmoothStep(initRotEuler, zoomOutRot, (transform.rotation.eulerAngles.x - rotSpeed * Input.mouseScrollDelta.y) / zoomOutRot), 180f, 0f);
        }
	}

	private void UpdateCameraPosition(Vector3 pos) {
        cameraCenterPosition.transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
}
