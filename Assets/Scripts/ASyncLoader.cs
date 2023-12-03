using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("UI Elements")]
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI percentageText;
    [SerializeField] private TextMeshProUGUI loadingText;

    private PlayerPrefManager ppm;
    public float distanceFromHeadset = 1.5f;
    
    private void Start()
    {
        mainMenu = GameObject.Find("Main Menu");
        loadingScreen = this.transform.gameObject;
        loadingSlider = loadingScreen.GetComponentInChildren<Slider>();
        percentageText = FindComponentInChildrenByName<TextMeshProUGUI>(loadingScreen, "Percent");
        loadingText = FindComponentInChildrenByName<TextMeshProUGUI>(loadingScreen, "Header");
        ppm = GameObject.Find("PlayerPrefManager").GetComponent<PlayerPrefManager>();
    }
    public void LoadLevel(int leveIndex)
    {
        if(mainMenu)
        {
            mainMenu.SetActive(false);
        }

        //SPECIAL
        if(ppm != null && ppm.GetScene() == "360Video")
        {
            Vector3 headsetPosition = Camera.main.transform.position;
            Vector3 headsetForward = Camera.main.transform.forward;

            transform.position = headsetPosition + distanceFromHeadset * headsetForward;
            transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);            
            transform.rotation = Quaternion.LookRotation(headsetForward);
        }
        loadingScreen.SetActive(true);

        //run async load
        StartCoroutine(LoadLevelASync(leveIndex));
    }

    IEnumerator LoadLevelASync(int levelIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelIndex);

        StartCoroutine(AnimateText());
        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            percentageText.text = FormatAsPercentage(progressValue);
            yield return null;
        }
        StopCoroutine(AnimateText());
        loadingText.text = "Done!";
    }
    
    IEnumerator AnimateText()
    {
        while (true)
        {
            loadingText.text = "Loading";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = "Loading.";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = "Loading..";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = "Loading...";
            yield return new WaitForSeconds(0.5f);
        }
    }
    string FormatAsPercentage(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100f);
        return $"{percentage}%";
    }

    T FindComponentInChildrenByName<T>(GameObject parentObject, string childObjectName) where T : Component
    {
        if (parentObject != null)
        {
            Transform childTransform = parentObject.transform.Find(childObjectName);

            if (childTransform != null)
            {
                // If the child is found, get the component by name
                T foundComponent = childTransform.GetComponent<T>();

                if (foundComponent != null)
                {
                    return foundComponent;
                }
                else
                {
                    Debug.LogWarning("Component '" + typeof(T).Name + "' not found on child '" + childObjectName + "'.");
                    return null;
                }
            }
            else
            {
                Debug.LogWarning("Child '" + childObjectName + "' not found.");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("Parent GameObject is null.");
            return null;
        }
    }
}
