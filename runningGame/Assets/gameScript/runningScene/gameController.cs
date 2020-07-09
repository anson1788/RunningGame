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
 
    // Update is called once per frame
    void Update ()
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

        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                clickAction();
            }
        }
        characterGrp.transform.position += new Vector3(0, 0, 50 * Time.deltaTime);
        updateCustom();
    }
    void OnGUI(){
        onGUICustom();
        //print("gameController!");
    }

    void clickAction(){
        if(character.transform.position.y<1){
            character.GetComponent<Animator>().Play("Armature|jump");
            character.GetComponent<Rigidbody>().AddForce(0, 500, 0, ForceMode.Force);
        }
    }
}
