using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public float mouseSens = 100f;

    InputAction look;
    Camera activeCamera;

    float xRotation = 0f;
    float yRotation = 0f;


    void Start() {
        //Lock the cursor to the middle of the screen and find mouse InputAction
        activeCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        look = InputSystem.actions.FindAction("Look");
        
    }

    void Update() {

        Vector2 lookValue = look.ReadValue<Vector2>();

        float mouseX = lookValue.x * mouseSens * Time.deltaTime;
        float mouseY = lookValue.y * mouseSens * Time.deltaTime;

        //Controls rotation around x axis (Up | Down)
        xRotation -= mouseY;

        //Clamp the rotation to prevent over rotating (Flipping the camera)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        //Controls rotation around y axis (Left | Right)
        yRotation += mouseX;

        //Apply both rotation, only transform player on y rotation but both y and x for camera.
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        activeCamera.gameObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
