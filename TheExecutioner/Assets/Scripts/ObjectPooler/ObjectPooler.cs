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
        //[Range(0, 999)]  [SerializeField] private int poolSize;
        //[SerializeField] private GameObject objectToPool;
        //static instanstance as we only want one cop
        public static ObjectPooler Instance;
        //Create a queue of PoolObjects later once we have the class
        //[Header("Pool Holders")]
        //public List<GameObject> pool = new List<GameObject>();
        
        //make this a dictionary 
        public List<PoolType> MasterPool;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            Invoke("FillPools",3f);
        }
        #region Object Pooler

        private void FillPools()
        {
            for (int i = 0; i < MasterPool.Count ; i++)
            {
                CreatePool(MasterPool[i]);
            }
        }
        private void CreatePool(PoolType type)
        {
            for (int i = 0; i < type.AmountToPool; i++)
            {
                var pooledObject = Instantiate(type.PrefabToPool, type.PoolHolder.transform);
                pooledObject.SetActive(false);
                type.ObjectPool.Add(pooledObject);
            }
        }
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
        public GameObject GetObject(PoolObjectType type)
        {
            PoolType currentPool = GetPoolType(type);
            List<GameObject> pool = currentPool.ObjectPool;
        
            GameObject returnObject = null;
            Debug.Log(pool.Count);
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