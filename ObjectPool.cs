using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public GameObject objectToPool;
    public int amountToPool;
    public bool isExpandable = false;
    
    private List<GameObject> pooledObjects;
    
    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(true);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        // No non-active GameObject found
        if (isExpandable)
        {
            // Create a new object
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(true);
            pooledObjects.Add(obj);
            return obj;
        }
        else
        {
            return null;
        }
    }
}
