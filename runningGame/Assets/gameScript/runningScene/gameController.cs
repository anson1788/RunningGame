using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : gameControllerGui
{
   // Start is called before the first frame update 
    public GameObject character;
    void Start(){
         
   }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
             character.GetComponent<Animator>().Play("Armature|jump");
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
                       character.GetComponent<Animator>().Play("Armature|jump");
            }
        }
        updateCustom();
    }
    void OnGUI(){
        onGUICustom();
        //print("gameController!");
    }
}
