using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [Serializable]
    public class PoolType
    {
        public PoolObjectType type;
        public int AmountToPool;
        public GameObject PrefabToPool;
        public GameObject PoolHolder;
        public List<GameObject> ObjectPool = new List<GameObject>();
    }

    public class ObjectPooler : Singleton<ObjectPooler>
    {
        [Header("Pool Properties")]
        public List<PoolType> MasterPool;
        private void Start()
        {
            Invoke("FillPools",3f);
        }
        #region Object Pooler
        /// <summary>
        /// Initialize each object pool 
        /// </summary>
        private void FillPools()
        {
            for (int i = 0; i < MasterPool.Count ; i++)
            {
                CreatePool(MasterPool[i]);
            }
        }
        /// <summary>
        /// Instantiate the target amount of prefabs defined in the inspector
        /// </summary>
        private void CreatePool(PoolType type)
        {
            for (int i = 0; i < type.AmountToPool; i++)
            {
                var pooledObject = Instantiate(type.PrefabToPool, type.PoolHolder.transform);
                pooledObject.SetActive(false);
                type.ObjectPool.Add(pooledObject);
            }
        }
        /// <summary>
        /// Return a reference to the target pool type
        /// </summary>
        private PoolType GetPoolType(PoolObjectType type)
        {
            for (int i = 0; i < MasterPool.Count; i++)
            {
                if (type == MasterPool[i].type)
                {
                    return MasterPool[i];
                }
            }

            return null;
        }
        /// <summary>
        /// Input a pool type to return a game gameobject from that pool
        /// If no gameobject is available instantiate a new one
        /// </summary>
        public GameObject GetObject(PoolObjectType type)
        {
            PoolType currentPool = GetPoolType(type);
            List<GameObject> pool = currentPool.ObjectPool;

            GameObject returnObject;
            if(pool.Count > 0)
            {
                returnObject = pool[pool.Count - 1];
                pool.Remove(returnObject);
                returnObject.SetActive(true);
            }
            else
            {
                returnObject = Instantiate(currentPool.PrefabToPool, currentPool.PoolHolder.transform);
            }
            return returnObject;
        }
        /// <summary>
        /// Pass a gameobject and type to return it the correct pool
        /// </summary>
        public void ReturnObject(GameObject obj,PoolObjectType type)
        {
            obj.SetActive(false);
            PoolType poolList = GetPoolType(type);
            List<GameObject> pool = poolList.ObjectPool;
        
            pool.Add(obj);
        }
        #endregion

        public void IncreasePoolSize()
        {
            MasterPool.Add(null);
        }


    }