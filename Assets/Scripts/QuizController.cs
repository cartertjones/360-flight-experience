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
    private TextMeshProUGUI questionText;

    //quiz questions
    [SerializeField]
    private Question[] quizQuestions;

    private GameObject[] toggledContent;
    private int delay = 3;

    [SerializeField]
    private VideoPlayerManager videoPlayerManager;

    private int correctAnswer;

    
    private void Start()
    {
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

        questionText = GameObject.Find("Header").GetComponent<TextMeshProUGUI>();

        toggledContent = new GameObject[answerButtons.Length + 1];
        for(int i = 0; i < answerButtons.Length; i++) {
            toggledContent[i] = answerButtons[i];
        }
        toggledContent[toggledContent.Length - 1] = questionText.transform.gameObject;
    }


    //check if any question aligns with the current timestamp
    public void UpdateTimestamp(int timestamp) {
        foreach(Question q in quizQuestions) {
            if(q.timestampInSec == timestamp) {
                ProckQuestion(q);
            }
        }
    }
    //when video timestamp hits a matching question timestamp
        //pause video
        //put answers in a random order
            //make sure to set the correct answer to correct
    private void ProckQuestion(Question q) {
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
    public void OnClick(int answer) {
        if(answer == correctAnswer) {
            Button correctButton = answerButtons[correctAnswer].GetComponent<Button>();
            ColorBlock colorBlock = correctButton.colors;
            colorBlock.normalColor = Color.green;
            correctButton.colors = colorBlock;
            //play sound

            //set ui body to blank after a moment
            StartCoroutine(DeactivateObjectsDelayed());
        }
    } 
    
    private IEnumerator DeactivateObjectsDelayed() {
        yield return new WaitForSeconds(delay);
        foreach(GameObject obj in toggledContent) {
            obj.SetActive(false);
        }
    }

}
