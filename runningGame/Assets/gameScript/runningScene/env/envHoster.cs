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
        StartCoroutine(createFloor(false));
    }

    public void removeObj(){

    }
    public void colliderBoxEnter(Collider other){
        print("time close "+other.gameObject.name);
        StartCoroutine(createFloor(true));
    }
    
    IEnumerator removeOutOfCameraObj(){
        GameObject collider = gameSingletonObj.screenCollider[0];
        gameSingletonObj.screenCollider.RemoveAt(0);
        Destroy(collider);
        yield return 0;
        int remove = 4;
        if(lastFloorIdx>10){
            remove = 6;
        }
        for(int i=0;i<remove;i++){
            GameObject obj = gameSingletonObj.screenFloorList[0];
            gameSingletonObj.screenFloorList.RemoveAt(0);
            Destroy(obj);
            GameObject plane = gameSingletonObj.screenFloorPlaneList[0];
            gameSingletonObj.screenFloorPlaneList.RemoveAt(0);
            Destroy(plane);

            List<GameObject> rBuildingList =  gameSingletonObj.rightBuilding[0];
            for(int j=0;j<rBuildingList.Count;j++){
                GameObject tmp = rBuildingList[j];
                Destroy(tmp);
            }
            gameSingletonObj.rightBuilding[0].Clear();
            gameSingletonObj.rightBuilding.RemoveAt(0);

            List<GameObject>  lBuildingList =  gameSingletonObj.leftBuilding[0];
            for(int j=0;j<lBuildingList.Count;j++){
                GameObject tmp = lBuildingList[j];
                Destroy(tmp);
            }
            gameSingletonObj.leftBuilding[0].Clear();
            gameSingletonObj.leftBuilding.RemoveAt(0);
            yield return 0;
        }

    
    }
    float lastBuildingRight = 0;
    float lastBuildingLeft = 0;
    float lastFloorIdx = 0;
    IEnumerator createFloor(bool istrigger){
        
        for(int i=0;i<8;i++){
            GameObject floor = (GameObject)Instantiate(floorPref, new Vector3(0, 0, (lastFloorIdx+i)*32), Quaternion.identity);
            floor.name = "floor_"+i;
           //Bounds b = gameSingletonObj.getBoundFromComplexObj(floor);
            gameSingletonObj.screenFloorList.Add(floor);
            GameObject planeObj = (GameObject)Instantiate(planePref, new Vector3(0, -0.01f, (lastFloorIdx+i)*32), Quaternion.identity);
            planeObj.name = "plane"+i;
            gameSingletonObj.screenFloorPlaneList.Add(planeObj);
            if(i==4){
                GameObject collider = (GameObject)Instantiate(colliderPref, new Vector3(0, 0, (lastFloorIdx+i)*32), Quaternion.identity);
                collider.name = "collider"+i;
                gameSingletonObj.screenCollider.Add(collider);
            }
         

            int idx = gameSingletonObj.screenFloorList.Count;
           // print("floor count :" + idx);
            GameObject lastItem = gameSingletonObj.screenFloorList[idx - 1];
            Bounds lastObjBounds = gameSingletonObj.getBoundFromComplexObj(lastItem);
            float floorEnd = 0.0f;
            floorEnd = lastObjBounds.center.z + lastObjBounds.size.z/2;
            //print("print last "+floorEnd);

            //float newItemCenterZ = 0;
            int ItemNumber = gameSingletonObj.buildingTypeList.Count;

            List<GameObject> tempList = new List<GameObject>();
            while(true){
                int rIdx = Random.Range(0, ItemNumber-1);
                GameObject pref = gameSingletonObj.buildingTypeList[rIdx];
                Bounds prefBounds = gameSingletonObj.getBoundFromComplexObj(pref);
                if(lastBuildingRight!=0){
                    lastBuildingRight = lastBuildingRight + prefBounds.size.z/2;
                }
                float xPos =  prefBounds.size.x/2 + lastObjBounds.center.x+lastObjBounds.size.x/2;
                GameObject building = (GameObject)Instantiate(pref, new Vector3(-xPos, 0, lastBuildingRight), Quaternion.identity);
                building.name = "building :"+ lastBuildingRight;
               // print("CenterZ :" + lastBuildingRight);
                lastBuildingRight = lastBuildingRight + prefBounds.size.z/2;
                tempList.Add(building);
                if(lastBuildingRight>floorEnd){
                    break;
                }
        
                yield return 0;
            }
            gameSingletonObj.rightBuilding.Add(tempList);
        
    
            tempList = new List<GameObject>();
            while(true){
                int rIdx = Random.Range(0, ItemNumber-1);
                GameObject pref = gameSingletonObj.buildingTypeList[rIdx];
                //pref.transform.Rotate(0, 180, 0);
                //pref.transform.rotation = t;

                Bounds prefBounds = gameSingletonObj.getBoundFromComplexObj(pref);
                if(lastBuildingLeft!=0){
                    lastBuildingLeft = lastBuildingLeft + prefBounds.size.z/2;
                }
                float xPos =  prefBounds.size.x/2 + lastObjBounds.center.x+lastObjBounds.size.x/2;
                
                GameObject building = (GameObject)Instantiate(pref, new Vector3(xPos, 0, lastBuildingLeft), Quaternion.identity);
                building.transform.Rotate(0, 180, 0);
                lastBuildingLeft = lastBuildingLeft + prefBounds.size.z/2;
                tempList.Add(building);
                if(lastBuildingLeft>floorEnd){
                    break;
                }
                
                yield return 0;
            }
             gameSingletonObj.leftBuilding.Add(tempList);
            yield return 0;


        }
        lastFloorIdx = lastFloorIdx + 8;
        
        print("complete");
       
        yield return 0;

        if(istrigger){
            StartCoroutine(removeOutOfCameraObj());
        }
    }
  
  
}
