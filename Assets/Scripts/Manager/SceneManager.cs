using System.Collections;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
    Scene_Base curScene;

    protected override void Init()
    {
        base.Init();

        // 처음으로 시작하는 씬 로딩
        StartCoroutine(GetCurScene().LoadingRoutine());
    }

    public Scene_Base GetCurScene()
    {
        if (curScene == null)
            curScene = FindAnyObjectByType<Scene_Base>();

        return curScene;
    }
    public T GetCurScene<T>() where T : Scene_Base
    {
        return GetCurScene() as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        // 씬이 변경되기 전 정리할 작업

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);

        while (oper.isDone == false)
        {
            // 로딩이 완료 되면서 진행할 작업. 로딩바 같은 거
            yield return null;
        }

        // 씬 로딩 작업
        yield return GetCurScene().LoadingRoutine();
    }
}
