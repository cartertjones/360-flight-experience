using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizController : MonoBehaviour
{
    //get quiz ui
    [SerializeField]
    private GameObject quizUI;
    [SerializeField]
    private GameObject[] answerButtons;
    private TextMeshProUGUI[] answerText;
    [SerializeField]
    private TextMeshProUGUI questionText;
    public TextMeshProUGUI postAnswerText;

    //quiz questions
    [SerializeField]
    private Question[] quizQuestions;

    private GameObject[] toggledContent;
    private float delay = 3f;

    [SerializeField]
    private VideoPlayerManager videoPlayerManager;

    private int correctAnswer;
    [SerializeField]
    private int correctPoints;

    [SerializeField]
    private PlayerPrefManager ppm;

    [SerializeField]
    private CustomSceneManager sm;

    [SerializeField]
    private ParticleSystem particleSystem;

    [SerializeField]
    private AudioSource[] audioSources;

    private void Start()
    {
        if(ppm.GetScene() == "Conclusion") {
            quizUI.SetActive(false);
        }
        Debug.Log($"answerButtons.Length: {answerButtons.Length}");

        answerText = new TextMeshProUGUI[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
            {
                // Use GetComponentInChildren<TextMeshProUGUI>() to search the entire hierarchy
                answerText[i] = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

                if (answerText[i] != null)
                {
                    Debug.Log($"answerText[{i}] is not null. Text: {answerText[i].text}");
                }
                else
                {
                    Debug.LogError($"TextMeshProUGUI component is null for answer button {i}");
                }
            }
            else
            {
                Debug.LogError($"answerButtons[{i}] is null");
            }
        }
        Debug.Log($"answerText.Length: {answerText.Length}");

        toggledContent = new GameObject[answerButtons.Length + 2];
        for(int i = 0; i < answerButtons.Length; i++) {
            toggledContent[i] = answerButtons[i];
        }
        toggledContent[toggledContent.Length - 2] = questionText.transform.gameObject;
        toggledContent[toggledContent.Length - 1] = postAnswerText.transform.gameObject;
        foreach (GameObject obj in toggledContent)
        {
            obj.SetActive(false);
        }
    }


    //check if any question aligns with the current timestamp
    public void UpdateTimestamp(int timestamp) {
        foreach(Question q in quizQuestions) {
            if(q.timestampInSec == timestamp) {
                ProckQuestion(q);
            }
        }

        if(timestamp >= videoPlayerManager.GetLength() + 1) {
            ProckQuestion(quizQuestions[quizQuestions.Length - 1]);
        }
    }
    //when video timestamp hits a matching question timestamp
        //pause video
        //put answers in a random order
            //make sure to set the correct answer to correct
    private void ProckQuestion(Question q) {
        quizUI.SetActive(true);
        videoPlayerManager.Pause();
        //set question header to question text
        questionText.text = q.questionText;
        //put answers from question in random order
        ShuffleArray(q.answers);
        //assign answers to the 4 buttons
        for (int i = 0; i < q.answers.Length && i < answerText.Length; i++)
        {
            if (q.answers[i] != null)
            {
                if (answerText[i] != null)
                {
                    answerText[i].text = q.answers[i].answerText;

                    if (q.answers[i].isCorrect)
                    {
                        correctAnswer = i;
                    }
                }
                else
                {
                    Debug.LogError($"TextMeshProUGUI component is null for answer button {i}");
                }
            }
            else
            {
                Debug.LogError($"Answer object is null for answer {i}");
            }
        }
        //display ui
        foreach(GameObject obj in toggledContent) {
            obj.SetActive(true);
        }
        toggledContent[toggledContent.Length - 1].SetActive(false);
        if(q.answers.Length == 2)
        {
            //true or false question
            answerButtons[2].SetActive(false);
            answerButtons[3].SetActive(false);
        }
        if(q.answers.Length == 1)
        {
            answerButtons[1].SetActive(false);
            answerButtons[2].SetActive(false);
            answerButtons[3].SetActive(false);
        }
        postAnswerText.text = q.postAnswerText;
    }

    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        System.Random rng = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    //on button click
    //if clicked answer is correct
    //change button color to green
    //play celebratory sound
    //increase score
    //delay, then reset ui body to blank
    public void OnClick(int answer)
    {
        Button clickedButton = answerButtons[answer].GetComponent<Button>();
        ColorBlock defaults = clickedButton.colors;
        ColorBlock colorBlock = defaults;

        if(ppm.GetTimestamp() >= videoPlayerManager.GetLength()) {
            sm.LoadScene(2, "360Video");
        }
        else {
            if (answer == correctAnswer)
            {
                colorBlock.normalColor = Color.green;
                colorBlock.highlightedColor = Color.green;
                // Assign the modified ColorBlock back to the button
                clickedButton.colors = colorBlock;

                ppm.IncreaseScore(correctPoints);
                // Play sound
                foreach(AudioSource audioSource in audioSources) {
                    audioSource.Play();
                }
                particleSystem.Play();
            }
            else
            {
                colorBlock.normalColor = Color.red;
                colorBlock.highlightedColor = Color.red;
                // Assign the modified ColorBlock back to the button
                clickedButton.colors = colorBlock;
                // Play sound
            }

            // Hide other buttons
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i != answer)
                {
                    answerButtons[i].SetActive(false);
                }
            }

            // Display post-answer text
            postAnswerText.transform.gameObject.SetActive(true);

            // Set UI body to blank after a moment
            StartCoroutine(DeactivateObjectsDelayed(defaults));
        }
    }

    private IEnumerator DeactivateObjectsDelayed(ColorBlock defaults)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in toggledContent)
        {
            obj.SetActive(false);
        }

        quizUI.SetActive(false);
        videoPlayerManager.Play();

        // Reset button colors to default
        foreach (GameObject answerButton in answerButtons)
        {
            Button button = answerButton.GetComponent<Button>();
            button.colors = defaults;
        }
    }

}
