using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : gameControllerGui
{
   // Start is called before the first frame update 
    public GameObject character;
    public GameObject characterGrp;
    void Start(){
          Application.targetFrameRate = 30;
          
   }
 
    void Update(){
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                clickAction();
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        
        if (Input.GetKeyDown("space"))
        {
             clickAction();
        }
        /*
        RuntimeAnimatorController  ctrller = character.GetComponent<Animator>().runtimeAnimatorController;
        AnimationClip[] clips = ctrller.animationClips;
        for(int i=0;i<clips.Length;i++){
             print("cc" + clips[i].name);
        }
        character.GetComponent<Animator>().Play("Armature|jump");
        */

        
        if(isStart){
            characterGrp.transform.position += new Vector3(0, 0, 50 * Time.deltaTime);
        }
        updateCustom();
    }

    bool isStart = false;
    void OnGUI(){
        onGUICustom();
        //print("gameController!");
    }

    void clickAction(){
          isStart = true;
        if(character.transform.position.y<3){
            character.GetComponent<Animator>().Play("Armature|jump");
            character.GetComponent<Rigidbody>().freezeRotation = true;
            if(character.transform.position.y>0.3){
                character.GetComponent<Rigidbody>().AddForce(0, 200, 0, ForceMode.Force);
            }else{
                character.GetComponent<Rigidbody>().AddForce(0, 700, 0, ForceMode.Force);
            }
        }
    }
}
