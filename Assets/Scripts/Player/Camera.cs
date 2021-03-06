using Photon.Pun;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    [SerializeField]
    float mouseSensitivity = 100f;
    [SerializeField]
    Transform playerBody;
    
    PhotonView photonView;
    Camera _camera;

    float xRotation = 0f;

    bool mouseLock = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        _camera = GetComponent<Camera>();
    }

    
    void Update()
    {
        _camera.gameObject.SetActive(photonView.IsMine);

        Look();
    }

    private void Look()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (mouseLock == true)
            {
                Cursor.lockState = CursorLockMode.None;
                mouseLock = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                mouseLock = true;
            }
        }

        if (mouseLock)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
