using UnityEngine;

public class SimpleFPSController : MonoBehaviour
{
    //public float footstepInterval = 0.5f;  // time between steps when walking
    private AudioSource footstepSource;
    public AudioClip footstepClip;
    //float footstepTimer = 0f;
    public float moveSpeed = 1f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public LevelManager levelManager;

    Vector3 velocity;
    CharacterController controller;
    float xRotation = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        footstepSource = GetComponent<AudioSource>();
        footstepSource.clip = footstepClip;
        velocity.y = -2f;
    }

    void Update()
    {
        if (levelManager.wallDown)
        {
            velocity.y = 0f;
        }
        // Movement
        float h = Input.GetAxis("Horizontal");   // A/D or Left/Right
        float v = Input.GetAxis("Vertical");     // W/S or Up/Down
        Vector3 move = (transform.right * h + transform.forward * v)*moveSpeed;
        //Debug.Log(move);
        controller.Move(move * Time.deltaTime);

        bool isMoving = move.magnitude > 0.01f;
        //Debug.Log("is moving: " + isMoving);

        controller.Move(velocity*Time.deltaTime);

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        HandleFootsteps(isMoving);
    }

    void HandleFootsteps(bool shouldPlay)
    {
        if (footstepSource == null)
        {
            return;
        }
        //Debug.Log("should playe: " + shouldPlay + ". Is playing: " + footstepSource.isPlaying + ". last check: " + (!shouldPlay && footstepSource.isPlaying));
        if (shouldPlay && !footstepSource.isPlaying)
        {
            //Debug.Log("will play");
            footstepSource.Play();
        }
        else if (!shouldPlay && footstepSource.isPlaying)
        {
            //Debug.Log("will stop");
            footstepSource.Stop();
        }
    }
}