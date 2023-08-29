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
    }

    public GameObject GetInstance(string prefabName,Object prefab)
    {
        
        //先检测一下缓存字典里是否注册了该预制体队列的模型
        if(objectPoolDic.ContainsKey(prefabName))
        {
            //要判断一下队列是否为空,利用空池触发机制
            if(objectPoolDic[prefabName].Count>0)
            {
                
                return objectPoolDic[prefabName].Dequeue();
            }
            else
            {
                
                var newGameObject=Instantiate((GameObject)prefab);
                newGameObject.SetActive(true);
                
                return newGameObject;
            }
        }
        else
        {
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
