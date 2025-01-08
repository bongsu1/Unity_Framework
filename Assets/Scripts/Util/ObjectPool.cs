using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    PooledObject _prefab;
    int _size;
    int _capacity;
    float _releaseTime;
    Stack<PooledObject> _poolStack;

    /// <summary>
    /// releaseTime을 0보다 크게 하면 autoRelease가 활성활 됩니다
    /// <para></para>
    /// autoRelease : pooledObject가 활성화 되고 releaseTime만큼 시간이 지나면 자동으로 Release가 호출됩니다
    /// </summary>
    public void CreatePool(PooledObject prefab, int size, int capacity, float releaseTime = 0f)
    {
        _prefab = prefab;
        _size = size;
        _capacity = capacity;
        _releaseTime = releaseTime;

        _poolStack = new Stack<PooledObject>(capacity);
        for (int i = 0; i < size; i++)
        {
            PooledObject pooledObj = Manager.Resource.Instantiate(_prefab, transform);
            pooledObj.gameObject.SetActive(false);
            pooledObj.Init(this, releaseTime);
            _poolStack.Push(pooledObj);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (_poolStack.Count > 0)
        {
            PooledObject pooledObj = _poolStack.Pop();
            pooledObj.transform.SetParent(parent);
            pooledObj.transform.position = position;
            pooledObj.transform.rotation = rotation;
            pooledObj.gameObject.SetActive(true);
            return pooledObj;
        }
        else
        {
            PooledObject pooledObj = Manager.Resource.Instantiate(_prefab, parent);
            pooledObj.gameObject.SetActive(false);
            pooledObj.Init(this, _releaseTime);

            pooledObj.transform.position = position;
            pooledObj.transform.rotation = rotation;
            pooledObj.gameObject.SetActive(true);
            return pooledObj;
        }
    }

    public void ReturnPool(PooledObject pooledObj)
    {
        if (_poolStack.Count < _capacity)
        {
            pooledObj.gameObject.SetActive(false);
            pooledObj.transform.SetParent(transform);
            _poolStack.Push(pooledObj);
        }
        else
        {
            Manager.Resource.Destroy(pooledObj.gameObject);
        }
    }
}
