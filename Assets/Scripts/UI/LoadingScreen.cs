using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreenObject;
    public Slider progressBar;
    public float minimumLoadingTime = 3f; // Th?i gian t?i thi?u ?? thanh slider ??t 0.9

    public static int sceneToLoad; // ID c?a Scene c?n t?i (???c truy?n t? MainMenu)

    private void Start()
    {
        progressBar.value = 0;
        StartCoroutine(SwitchToSceneAsync(sceneToLoad));
    }

    IEnumerator SwitchToSceneAsync(int id)
    {
        float startTime = Time.time;

        // T?i scene không ??ng b?
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
        asyncLoad.allowSceneActivation = false; // D?ng vi?c chuy?n c?nh t? ??ng

        // Coroutine ?? t?ng giá tr? c?a slider
        StartCoroutine(UpdateSlider());

        while (!asyncLoad.isDone)
        {
            // Khi t?i g?n hoàn thành (0.9f là giá tr? g?n hoàn thành)
            if (asyncLoad.progress >= 0.9f)
            {
                // ??m b?o slider ??t 0.9 trong th?i gian t?i thi?u
                if (Time.time - startTime < minimumLoadingTime)
                {
                    progressBar.value = 0.9f; // ??t giá tr? t?i ?a
                }
                else
                {
                    progressBar.value = 1f; 
                    asyncLoad.allowSceneActivation = true; 
                }
            }
            yield return null;
        }
    }

    IEnumerator UpdateSlider()
    {
        float startTime = Time.time;
        float endTime = startTime + minimumLoadingTime;

        while (Time.time < endTime)
        {
            float elapsed = Time.time - startTime;
            progressBar.value = Mathf.Lerp(0f, 0.9f, elapsed / minimumLoadingTime);
            yield return null;
        }
        progressBar.value = 0.9f; 
    }
}
