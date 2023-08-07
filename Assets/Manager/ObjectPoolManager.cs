using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{

    private Dictionary<string, Queue<GameObject>> objectPoolDic;

    protected override void Awake()
    {
        base.Awake();
        objectPoolDic = new Dictionary<string, Queue<GameObject>>();
        Debug.Log("初始化池子成功");
    }

    public GameObject GetInstance(string prefabName,Object prefab)
    {
        
        if(objectPoolDic.ContainsKey(prefabName))
        {
            if(objectPoolDic[prefabName].Count>0)
            {
                //Debug.Log("取出物体");
                return objectPoolDic[prefabName].Dequeue();
            }
            else
            {
                
                var newGameObject=Instantiate((GameObject)prefab);
                newGameObject.SetActive(true);
                //Debug.Log("生成并拿出物体");
                return newGameObject;
            }
        }
        else
        {
            //Debug.Log("生成物品队列并生产物品再拿出");
            objectPoolDic.Add(prefabName, new Queue<GameObject>());
            var newGameObject=Instantiate((GameObject)prefab);
            newGameObject.SetActive(true);
            return newGameObject;
        }
    }
    public void ReturnObject(string gameObjectName,GameObject targetGameObject)
    {
        if (objectPoolDic.ContainsKey(gameObjectName))
        {
            objectPoolDic[gameObjectName].Enqueue(targetGameObject);
            targetGameObject.SetActive(false);
            targetGameObject.transform.SetParent(gameObject.transform);
        }
        else
        {
            return;
        }
    }
}
