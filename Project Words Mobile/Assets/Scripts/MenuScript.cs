using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public Animator transitionCanvasPrefab;
    public float transitiontime = 1.0f;
    public GameObject wordCategory;

    public void LoadScene(string target)
    {
        //We save the wordcategory to re-use it in the other scene

        PlayerPrefs.SetInt("WordCategory", int.Parse(wordCategory.GetComponent<Text>().text));

        StartCoroutine(LoadAsynchronously(target));
    }

    IEnumerator LoadAsynchronously(string target)
    {

        //Playanimation
        transitionCanvasPrefab.SetTrigger("Start");
        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(target, LoadSceneMode.Single);

        //AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(target, LoadSceneMode.Single);
        //sceneOperation.allowSceneActivation = true;
        //while (!sceneOperation.isDone)
        //{
        //    float progress = Mathf.Clamp01(sceneOperation.progress / 0.9f);
        //    slider.value = progress;
        //    yield return null;
        //}
    }
}
