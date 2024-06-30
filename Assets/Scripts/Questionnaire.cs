using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Dimension { get; set; }
}

public class Questionnaire : MonoBehaviour
{
//_____________ Inspector Vars _____________
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private ToggleGroup answerToggleGroup;
    [SerializeField] private List<Toggle> answerToggles;
    [SerializeField] private GameObject Endcanvas;
    [SerializeField] private GameObject questionsCanvas;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button submitButton;

//_____________ Private Vars _____________
    private int currentQuestionIndex = 0;
    private static int QUESTIONSNUM = 30;
    private List<string> answers = new List<string>(new string[QUESTIONSNUM]);
    private List<int> selecteIndecies = new List<int>(new int[QUESTIONSNUM]);

    Question[] questions = new Question[]
        {
            new() { Id = 0, Text = "I lost myself in this experience.", Dimension = "FA" },
            new() { Id = 1, Text = "I was so involved in this experience that I lost track of time.", Dimension = "FA" },
            new() { Id = 2, Text = "I blocked out things around me when I was using the VR game.", Dimension = "FA" },
            new() { Id = 3, Text = "When I was using the VR game, I lost track of the world around me.", Dimension = "FA" },
            new() { Id = 4, Text = "The time I spent using the VR game just slipped away.", Dimension = "FA" },
            new() { Id = 5, Text = "I was absorbed in this experience.", Dimension = "FA" },
            new() { Id = 6, Text = "During this experience I let myself go.", Dimension = "FA" },
            new() { Id = 7, Text = "I felt frustrated while using this VR game.", Dimension = "PU" },
            new() { Id = 8, Text = "I found this VR game confusing to use.", Dimension = "PU" },
            new() { Id = 9, Text = "I felt annoyed while using the VR game.", Dimension = "PU" },
            new() { Id = 10, Text = "I felt discouraged while using this VR game.", Dimension = "PU" },
            new() { Id = 11, Text = "Using this VR game was taxing.", Dimension = "PU" },
            new() { Id = 12, Text = "This experience was demanding.", Dimension = "PU" },
            new() { Id = 13, Text = "I felt in control while using this VR game.", Dimension = "PU" },
            new() { Id = 14, Text = "I could not do some of the things I needed to do while using the VR game.", Dimension = "PU" },
            new() { Id = 15, Text = "This VR game was attractive.", Dimension = "AE" },
            new() { Id = 16, Text = "This VR game was aesthestically appealing.", Dimension = "AE" },
            new() { Id = 17, Text = "I liked the graphics and images of the VR game.", Dimension = "AE" },
            new() { Id = 18, Text = "The VR game appealed to my visual senses.", Dimension = "AE" },
            new() { Id = 19, Text = "The screen layout of the VR game was visually pleasing.", Dimension = "AE" },
            new() { Id = 20, Text = "Using the VR game was worthwhile.", Dimension = "RW" },
            new() { Id = 21, Text = "I consider my experience a success.", Dimension = "RW" },
            new() { Id = 22, Text = "This experience did not work out the way I had planned.", Dimension = "RW" },
            new() { Id = 23, Text = "My experience was rewarding.", Dimension = "RW" },
            new() { Id = 24, Text = "My experience was rewarding.", Dimension = "RW" },
            new() { Id = 25, Text = "I continued to use the VR game out of curiosity.", Dimension = "RW" },
            new() { Id = 26, Text = "The content of the VR game incited my curiosity.", Dimension = "RW" },
            new() { Id = 27, Text = "I was really drawn into this experience.", Dimension = "RW" },
            new() { Id = 28, Text = "I felt involved in this experience.", Dimension = "RW" },
            new() { Id = 29, Text = "This experience was fun.", Dimension = "RW" },
        };

    private int[] ans = {1, 2, 3, 4, 5};

    void Start()
    {
        for (int i = 0; i < answers.Count; i++)
        {
            answers[i] = "";
            selecteIndecies[i] = -1;
        }
        questions = ShuffleArray(questions);
        UpdateQuestion();

        nextButton.onClick.AddListener(NextQuestion);
        prevButton.onClick.AddListener(PrevQuestion);

        foreach (var toggle in answerToggles)
        {
            toggle.onValueChanged.AddListener(delegate {
                AnswerSelected(toggle);
            });
        }
    }
    void UpdateQuestion()
    {
        if (questions.Length == 0) return;

        questionText.text = questions[currentQuestionIndex].Text;
        answerToggleGroup.SetAllTogglesOff();

        if (currentQuestionIndex < answers.Count && selecteIndecies[currentQuestionIndex] != -1)
        {
            answerToggles[selecteIndecies[currentQuestionIndex]].isOn = true;
        }

        nextButton.gameObject.SetActive(currentQuestionIndex < questions.Length - 1);
        prevButton.gameObject.SetActive(currentQuestionIndex > 0);
    }

    void NextQuestion()
    {
        if (currentQuestionIndex < questions.Length - 1)
        {
            if (answers[currentQuestionIndex] == "") return;
            currentQuestionIndex++;
            UpdateQuestion();
        }
    }

    void PrevQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            UpdateQuestion();
        }
    }

    void AnswerSelected(Toggle selectedToggle)
    {
        if (selectedToggle.isOn)
        {
            if (currentQuestionIndex == questions.Length - 1)
            {
                submitButton.gameObject.SetActive(true);
            }
            int selectedIndex = answerToggles.IndexOf(selectedToggle);
            string selectedAnswer = answerToggles[selectedIndex].GetComponentInChildren<Text>().text;

            selecteIndecies[currentQuestionIndex] = selectedIndex;
            answers[currentQuestionIndex] = selectedAnswer;
        }
    }

    public void SaveToCSV()
    {
        Debug.Log("SavetoCSV");
        string name = PlayerPrefs.GetString("name");
        string email = PlayerPrefs.GetString("email");
        string filePath = Path.Combine(Application.persistentDataPath, $"{name}.csv");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("id,Question,dimension,Answer");

            for (int i = 0; i < questions.Length; i++)
            {
                int id = questions[i].Id;
                string question = questions[i].Text;
                string dimension = questions[i].Dimension;
                int answer = i < selecteIndecies.Count ? selecteIndecies[i] + 1 : -1;
                writer.WriteLine($"\"{id}\",\"{question}\",\"{dimension}\",\"{answer}\"");
            }
            writer.WriteLine($"email, {email}");
            writer.WriteLine($"first timer, {PlayerPrefs.GetFloat("first timer")}");
            writer.WriteLine($"second timer, {PlayerPrefs.GetFloat("second timer")}");
            writer.WriteLine($"third timer {PlayerPrefs.GetFloat("third timer")}");
            writer.WriteLine($"Mistakes, {PlayerPrefs.GetFloat("mistakes")}");

            // writer.WriteLine($"Score for voices, {PlayerPrefs.GetInt("score")}");
            // writer.WriteLine($"Score fo signs, {PlayerPrefs.GetInt("sign score")}");
        }

        questionsCanvas.SetActive(false);
        Endcanvas.SetActive(true);
        StartCoroutine(Delay(5f));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Endcanvas.SetActive(false);
        SceneManager.LoadScene(1);
    }

    static T[] ShuffleArray<T>(T[] array)
    {
        System.Random random = new System.Random();
        return array.OrderBy(x => random.Next()).ToArray();
    }
}

