using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envHoster : envHosterVarBasic
{

    protected gameSingleton gameSingletonObj;
    // Start is called before the first frame update
    public static envHoster instance;
    void Start()
    {
       instance = this;
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
    public void colliderBoxEnter(Collider other){
        print("time close "+other.gameObject.name);
        StartCoroutine(createFloor());
    }


    IEnumerator createFloor(){
        int idx = gameSingletonObj.screenFloorList.Count;
        for(int i=0;i<gameSingletonObj.roadPreview;i++){
            GameObject floor = (GameObject)Instantiate(floorPref, new Vector3(0, 0, (idx+i)*32), Quaternion.identity);
            floor.name = "floor_"+i;
           //Bounds b = gameSingletonObj.getBoundFromComplexObj(floor);
            gameSingletonObj.screenFloorList.Add(floor);
            GameObject planeObj = (GameObject)Instantiate(planePref, new Vector3(0, -0.01f, (idx+i)*32), Quaternion.identity);
            planeObj.name = "plane"+i;
            gameSingletonObj.screenFloorPlaneList.Add(planeObj);
            if(i==2){
                GameObject collider = (GameObject)Instantiate(colliderPref, new Vector3(0, 0, (idx+i)*32), Quaternion.identity);
                collider.name = "collider"+i;
            }
        }
        idx = gameSingletonObj.screenFloorList.Count;
        GameObject lastItem = gameSingletonObj.screenFloorList[idx - 1];
        Bounds lastObjBounds = gameSingletonObj.getBoundFromComplexObj(lastItem);
        float floorEnd = 0.0f;
        floorEnd = lastObjBounds.center.z + lastObjBounds.size.z/2;
        print("print last "+floorEnd);

        float newItemCenterZ = 0;
        int ItemNumber = gameSingletonObj.buildingTypeList.Count;
        while(true){
            int rIdx = Random.Range(0, ItemNumber-1);
            GameObject pref = gameSingletonObj.buildingTypeList[rIdx];
            Bounds prefBounds = gameSingletonObj.getBoundFromComplexObj(pref);
            if(newItemCenterZ!=0){
                newItemCenterZ = newItemCenterZ + prefBounds.size.z/2;
            }
            float xPos =  prefBounds.size.x/2 + lastObjBounds.center.x+lastObjBounds.size.x/2;
            GameObject building = (GameObject)Instantiate(pref, new Vector3(-xPos, 0, newItemCenterZ), Quaternion.identity);
            newItemCenterZ = newItemCenterZ + prefBounds.size.z/2;
            if(newItemCenterZ>floorEnd){
                break;
            }
        }
        newItemCenterZ = 0;
        while(true){
            int rIdx = Random.Range(0, ItemNumber-1);
            GameObject pref = gameSingletonObj.buildingTypeList[rIdx];
            //pref.transform.Rotate(0, 180, 0);
            //pref.transform.rotation = t;

            Bounds prefBounds = gameSingletonObj.getBoundFromComplexObj(pref);
            if(newItemCenterZ!=0){
                newItemCenterZ = newItemCenterZ + prefBounds.size.z/2;
            }
            float xPos =  prefBounds.size.x/2 + lastObjBounds.center.x+lastObjBounds.size.x/2;
            
            GameObject building = (GameObject)Instantiate(pref, new Vector3(xPos, 0, newItemCenterZ), Quaternion.identity);
            building.transform.Rotate(0, 180, 0);
            newItemCenterZ = newItemCenterZ + prefBounds.size.z/2;
            if(newItemCenterZ>floorEnd){
                break;
            }
        }
        print("complete");
        /*
        float pos = 32f/32.1f;
        pos = pos * 10f;
        pos = pos;
        GameObject floorA = (GameObject)Instantiate(floorPref, new Vector3(pos, 0, 0), Quaternion.identity);
        */
        yield return 0;
    }
  
  
}
