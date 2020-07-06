using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSingleton : MonoBehaviour
{

    public static gameSingleton instance;
    public int roadPreview = 4;

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
}
