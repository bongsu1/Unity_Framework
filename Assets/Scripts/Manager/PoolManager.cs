using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<int, ObjectPool> _pools = new Dictionary<int, ObjectPool>();

    /// <summary>
    /// releaseTime을 0보다 크게 하면 autoRelease가 활성화 됩니다
    /// <para></para>
    /// autoRelease : pooledObject가 활성화 되고 releaseTime만큼 시간이 지나면 자동으로 Release가 호출됩니다
    /// </summary>
    public void CreatePool(PooledObject prefab, int size, int capacity, float releaseTime = 0)
    {
        if (prefab == null)
            return;

        GameObject obj = new GameObject($"Pool_{prefab.name}");
        ObjectPool pool = obj.GetOrAddComponent<ObjectPool>();
        pool.CreatePool(prefab, size, capacity, releaseTime);

        _pools.Add(prefab.GetInstanceID(), pool);
    }
    public void CreatePool(string path, int size, int capacity, float releaseTime = 0)
    {
        PooledObject prefab = Manager.Resource.Load<PooledObject>(path);

        CreatePool(prefab, size, capacity, releaseTime);
    }

    public void DestroyPool(PooledObject prefab)
    {
        if (prefab == null)
            return;

        if (_pools.TryGetValue(prefab.GetInstanceID(), out ObjectPool pool) == false)
            return;

        Manager.Resource.Destroy(pool.gameObject);
        _pools.Remove(prefab.GetInstanceID());
    }
    public void DestroyPool(string path)
    {
        PooledObject prefab = Manager.Resource.Load<PooledObject>(path);

        DestroyPool(prefab);
    }

    public void ClearPool()
    {
        foreach (var pool in _pools.Values)
        {
            Manager.Resource.Destroy(pool.gameObject);
        }

        _pools.Clear();
    }

    public PooledObject GetPool(PooledObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (prefab == null)
            return null;

        if (_pools.TryGetValue(prefab.GetInstanceID(), out ObjectPool pool) == false)
            return null;

        return pool.GetPool(position, rotation, parent);
    }
    public PooledObject GetPool(string path, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        PooledObject prefab = Manager.Resource.Load<PooledObject>(path);

        return GetPool(prefab, position, rotation, parent);
    }
}
