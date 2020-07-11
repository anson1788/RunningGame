using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSingleton : MonoBehaviour
{

    public static gameSingleton instance;
    public int roadPreview = 4;

    public List<GameObject> buildingTypeList;

    public List<GameObject> screenFloorList = new List<GameObject>();
    public List<GameObject> screenFloorPlaneList = new List<GameObject>();

    public List<GameObject> screenCollider = new List<GameObject>();
    public List<List<GameObject>> leftBuilding = new List<List<GameObject>>();
    public List<List<GameObject>> rightBuilding = new List<List<GameObject>>();
    
    protected bool isLoading = true;
    // Start is called before the first frame update
    void Start()
    {   
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bounds getBoundFromComplexObj(GameObject obj){
        Bounds b = new Bounds(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        Renderer[] render = obj.GetComponentsInChildren<Renderer>();
            
        for(int j=0; j<render.Length;j++)
        {
            Renderer filter = render[j];
            if(j==0){
                b = filter.bounds;
            }else{
                b.Encapsulate(filter.bounds);
            }
                
        }
        return b;
    }
}
