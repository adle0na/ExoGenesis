using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraMovementTest : MonoBehaviour
{
    public Transform objectToFollow;
    public Transform mainCamera;

    public Transform CharacterSpineObj;

    public float followSpeed;
    public float sensitivity;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float clampAngleUp;
    public float clampAngleDown;
    public float smoothness;

    public float rotationX;
    public float rotationY;

    private Vector3 initialPosition;
    public Vector3 dirNormalized;
    public Vector3 finalDir;

    void Start()
    {
        mainCamera = Camera.main.transform;
        transform.rotation = objectToFollow.rotation;

        rotationX = transform.localRotation.eulerAngles.x;
        rotationY = transform.localRotation.eulerAngles.y;

        initialPosition = transform.position;
        dirNormalized = mainCamera.localPosition.normalized;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        InputCameraMovement();
    }

    void FixedUpdate()
    {
        CalcCameraMovement();
    }

    void InputCameraMovement()
    {
        rotationX += -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -clampAngleUp, clampAngleDown);
        
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 20f);

        // SpineObj의 회전을 변경
        Quaternion spineRotation = Quaternion.Euler(rotationX, rotationY, 0);
        CharacterSpineObj.rotation = spineRotation;
    }

    void CalcCameraMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        int layerMask = LayerMask.GetMask("Character");
        if (Physics.Linecast(transform.position, finalDir, out RaycastHit rayHit, ~layerMask))
        {
            finalDistance = Mathf.Clamp(rayHit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }

        // 카메라 줌 인/아웃 조절
        float zoomFactor = Mathf.InverseLerp(0, Mathf.Abs(clampAngleDown), Mathf.Abs(rotationX));
        float targetPosY = Mathf.Lerp(1.15f, 0.8f, zoomFactor);
        float targetPosZ = Mathf.Lerp(-2.3f, -1.2f, zoomFactor);

        float targetPosX = 0.7f;

        // 부드러운 이동
        mainCamera.localPosition = Vector3.Lerp(mainCamera.localPosition, new Vector3(targetPosX, targetPosY, targetPosZ), Time.deltaTime * smoothness);
    }
}