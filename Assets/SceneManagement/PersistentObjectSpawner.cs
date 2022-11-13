using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{

    static List<string> spawnedObjects;
    public GameObject[] persistentObjectPrefabs;

    private void Awake() {
        if(spawnedObjects == null) {
            spawnedObjects = new List<string>();
        }
        foreach(GameObject item in persistentObjectPrefabs)
            if(!spawnedObjects.Contains(item.name)){
                GameObject persistentObject = Instantiate(item);
                spawnedObjects.Add(item.name);
                DontDestroyOnLoad(persistentObject);
            }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
