using System.Collections;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPool _pool;
    WaitForSeconds _releaseWait;
    bool _autoRelease = false;

    /// <summary>
    /// releaseTime을 0보다 크게 하면 autoRelease가 활성화 됩니다
    /// <para></para>
    /// autoRelease : pooledObject가 활성화 되고 releaseTime만큼 시간이 지나면 자동으로 Release가 호출됩니다
    /// </summary>
    public void Init(ObjectPool pool = null, float releaseTime = 0)
    {
        _pool = pool;
        if (_autoRelease = releaseTime > 0)
            _releaseWait = new WaitForSeconds(releaseTime);
    }

    private void OnEnable()
    {
        if (_autoRelease)
        {
            StartCoroutine(ReleaseRoutine());
        }
    }

    public void Release()
    {
        if (_pool != null)
        {
            _pool.ReturnPool(this);
        }
        else
        {
            Manager.Resource.Destroy(gameObject);
        }
    }

    IEnumerator ReleaseRoutine()
    {
        yield return _releaseWait;
        Release();
    }
}
