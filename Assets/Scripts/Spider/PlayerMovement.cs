using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        float move = 0f;
        float rotate = 0f;

        if (Keyboard.current.wKey.isPressed)
            move = 1f;
        else if (Keyboard.current.sKey.isPressed)
            move = -1f;

        if (Keyboard.current.aKey.isPressed)
            rotate = -1f;
        else if (Keyboard.current.dKey.isPressed)
            rotate = 1f;

        Vector3 movement = transform.forward * move * moveSpeed * Time.deltaTime;
        float rotation = rotate * rotationSpeed * Time.deltaTime;

        transform.position += movement;
        transform.Rotate(0, rotation, 0);
    }
}
