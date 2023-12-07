using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public AnimationCurve fadeCurve;
    public string colorPropertyName = "_BaseColor";
    private Renderer rend;

    [SerializeField]
    public ASyncLoader loader;
    [SerializeField]
    public CustomSceneManager sm;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        loader = GameObject.Find("CustomSceneManager").GetComponent<ASyncLoader>();
        sm = GameObject.Find("CustomSceneManager").GetComponent<CustomSceneManager>();

        rend.enabled = false;

        if (fadeOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0, -1);
    }

    public void FadeOut(int sceneIndex)
    {
        Fade(0, 1, sceneIndex);
    }

    public void Fade(float alphaIn, float alphaOut, int sceneIndex)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, sceneIndex));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut, int sceneIndex)
    {
        rend.enabled = true;

        float timer = 0;
        while (timer <= fadeDuration)
        {
            float alpha = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration));
            Color newColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            rend.material.SetColor(colorPropertyName, newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor(colorPropertyName, newColor2);

        if (alphaOut == 0)
            rend.enabled = false;
        else
        {
            loader.LoadLevel(sceneIndex);
        }
    }
}