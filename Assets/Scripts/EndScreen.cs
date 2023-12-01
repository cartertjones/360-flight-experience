using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;
    public PlayerPrefManager ppm;
    public int questionCount;
    public CustomSceneManager sm;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = (ppm.GetScore() + "/" + questionCount);
        slider.value = (float)(ppm.GetScore())/(float)(questionCount);
    }

    public void MainMenu()
    {
        ppm.ResetGame();
        sm.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
