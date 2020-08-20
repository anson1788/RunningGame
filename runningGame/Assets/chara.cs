using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chara : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public float horizontalSpeed = 0.5F;
     public float verticalSpeed = 0.5F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
         float v = verticalSpeed * Input.GetAxis("Mouse Y");
         transform.Rotate(0, h, 0);
         Camera.main.transform.Rotate(-v, 0, 0);
    }
 

}
