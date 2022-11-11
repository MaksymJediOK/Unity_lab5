using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Transform cameraPosition;
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;
    private CharacterController characterController;
    private Scene currentScene;

    private Vector3 movementVector;
    void Start()
    {   
        characterController = GetComponent<CharacterController>();
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        // Create a temporary reference to the current scene.


        // Retrieve the name of this scene.

        //movementVector.x = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        // movementVector.z = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        Vector3 movementVertical = transform.forward * Input.GetAxis("Vertical");
        Vector3 movementHorizontal = transform.right * Input.GetAxis("Horizontal");

        movementVector = movementHorizontal + movementVertical;
        movementVector.Normalize();
        movementVector = movementVector * movementSpeed * Time.deltaTime;

        characterController.Move(movementVector);

        Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if (invertX)
        {
            mouseVector.x = -mouseVector.x;
        }
        if (invertY)
        {
            mouseVector.y = -mouseVector.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseVector.x, transform.rotation.eulerAngles.z);
        cameraPosition.rotation = Quaternion.Euler(cameraPosition.rotation.eulerAngles + new Vector3(mouseVector.y, 0f, 0f));
        string sceneName = currentScene.name;
        string soundS = sceneName == "Two" ? "Wood" : "Stone";

        if (sceneName == "Two")
        {
            if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
            {

                if (!FindObjectOfType<AudioManager>().IsPlaying(soundS))
                {
                    FindObjectOfType<AudioManager>().Play(soundS);
                }
            }
            else
            {
                FindObjectOfType<AudioManager>().Stop(soundS);
            }
        }
       
    }
}
