using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : gameControllerGui
{
   // Start is called before the first frame update
   public void Start(){
   }
 
    // Update is called once per frame
    public void Update()
    {
        updateCustom();
    }
    void OnGUI(){
        onGUICustom();
        print("gameController!");
    }
}
