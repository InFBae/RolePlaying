using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private BaseScene curScene;
    private LoadingUI loadingUI;
    public BaseScene CurScene
    {
        get 
        {
            if (curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();
            
            return curScene;
        }
    }

    private void Awake()
    {
        LoadingUI ui = GameManager.Resource.Load<LoadingUI>("UI/LoadingUI");
        loadingUI = GameManager.Resource.Instantiate(ui);
        loadingUI.transform.SetParent(transform, false);
    }

    public void LoadScene(string sceneName)
    {       
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.FadeOut();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);

        while (!oper.isDone)
        {
            loadingUI.SetProgress(Mathf.Lerp(0, 0.5f, oper.progress));
            yield return null;
        }

        CurScene.LoadAsync();
        while (CurScene.progress < 1f)
        {
            loadingUI.SetProgress(Mathf.Lerp(0.5f, 1f, CurScene.progress));
            yield return null;
        }
        
        Time.timeScale = 1f;        
        loadingUI.FadeIn();
        yield return new WaitForSeconds(0.5f);
        
    }
}