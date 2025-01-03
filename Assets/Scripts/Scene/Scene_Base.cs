using System.Collections;
using UnityEngine;

public abstract class Scene_Base : MonoBehaviour
{
    /// <summary>
    /// 씬이 로딩되면서 진행할 작업
    /// </summary>
    public abstract IEnumerator LoadingRoutine();
}
