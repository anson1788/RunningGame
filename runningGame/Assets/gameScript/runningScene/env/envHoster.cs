using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envHoster : envHosterVarBasic
{

    protected gameSingleton gameSingletonObj;
    // Start is called before the first frame update
    void Start()
    {
       gameSingletonObj = gameSingleton.instance;
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
        for(int i=0;i<gameSingletonObj.roadPreview;i++){
            GameObject floor = (GameObject)Instantiate(floorPref, new Vector3(0, 0, i*32), Quaternion.identity);
            floor.name = "floor_"+i;
            Bounds b = gameSingletonObj.getBoundFromComplexObj(floor);
            print("sizeZ "+ i + " : "+b.size.z); 
            gameSingletonObj.screenFloorList.Add(floor);
            GameObject plane = (GameObject)Instantiate(planePref, new Vector3(0, -0.01f, i*32), Quaternion.identity);
            gameSingletonObj.screenFloorPlaneList.Add(plane);
        }

        float pos = 32f/32.1f;
        pos = pos * 10f;
        pos = pos;
        GameObject floorA = (GameObject)Instantiate(floorPref, new Vector3(pos, 0, 0), Quaternion.identity);
       
        yield return 0;
    }
  
  
}
