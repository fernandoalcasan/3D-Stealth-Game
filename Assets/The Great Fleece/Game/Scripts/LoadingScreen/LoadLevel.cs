using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    private Image _loadingBar;

    private WaitForEndOfFrame _waitFEOF;

    private void Start()
    {
        _waitFEOF = new WaitForEndOfFrame();
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        while(!asyncLoad.isDone)
        {
            float loaded = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            _loadingBar.fillAmount = loaded;

            yield return _waitFEOF;
        }
    }
}
