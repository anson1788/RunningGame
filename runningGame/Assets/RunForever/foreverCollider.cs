using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foreverCollider : MonoBehaviour
{


     float maxJumpHeight = 10.0f;
     float groundHeight;
     Vector3 groundPos;
     float jumpSpeed = 20.0f;
     float fallSpeed = 15.0f;
     public bool inputJump = false;
     public bool grounded = true;
 

    // Start is called before the first frame update
    void Start()
    {
        groundPos = transform.localPosition;
        groundHeight = transform.localPosition.y;
        //print("aa" + transform.localPosition.y);
        maxJumpHeight = transform.localPosition.y + maxJumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
          if(inputJump==false){
            inputJump = true;
            StartCoroutine("Jump");
            GetComponent<Animator>().Play("Armature|jump");
            GetComponent<Rigidbody>().freezeRotation = true;
          }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Animator>().Play("Armature|falling");
            GetComponent<Rigidbody>().freezeRotation = true;
        }
         if(transform.localPosition == groundPos)
             grounded = true;
         else
             grounded = false;
    }

    void OnTriggerEnter(Collider other){
      
        //Debug.Log("enter");
       //groundPos = transform.position;
       //inputJump = true;
       //StartCoroutine("Jump");
       //print(other.GetComponent<Collider>().gameObject.name);
  
    }
    void OnTriggerExit(Collider other){
       //Debug.Log("out");
    }

     IEnumerator Jump(){
         while(true)
         {
             if(transform.localPosition.y >= maxJumpHeight)
                 inputJump = false;
             if(inputJump)
                 transform.Translate(Vector3.up * jumpSpeed * Time.smoothDeltaTime);
             else if(!inputJump)
             {
                 transform.Translate(Vector3.down * fallSpeed * Time.smoothDeltaTime);
                 if(transform.localPosition.y < groundPos.y){
                     transform.localPosition = groundPos;
                     StopAllCoroutines();
                 }
             }
          
         yield return new WaitForEndOfFrame();
         }
     }
}
