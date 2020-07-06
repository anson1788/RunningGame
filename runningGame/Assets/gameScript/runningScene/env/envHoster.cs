using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envHoster : envHosterVarBasic
{
    // Start is called before the first frame update
    void Start()
    {
       createBuilding();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void createBuilding(){
        StartCoroutine(createFloor());
    }


    IEnumerator createFloor(){
        for(int i=0;i<gameSingleton.instance.roadPreview;i++){
            GameObject spawnObj_Obj = (GameObject)Instantiate(floorPref, new Vector3(0, 0, i*32), Quaternion.identity);
		}
        yield return 0;
    }
  
}
