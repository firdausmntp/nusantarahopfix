using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button buttonA, buttonB, buttonC, buttonD;
    public GameObject quizPanel;
    public GameObject feedbackPanel;
    public TextMeshProUGUI feedbackText;
    public float delayBeforeActivate = 2f;
    public GameObject readyPanel;
    public GameObject goPanel;
    public GameObject player;
    public GameObject progressBar;

    private string correctOption;

    void Start()
    {
        // Nonaktifkan semua root object kecuali Main Camera, Canvas, EventSystem, dan QuizManager
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            if (obj != this.gameObject && obj.name != "Main Camera" && obj.name != "Canvas" && obj.name != "EventSystem" && obj.name != "PlatformSpawner")
            {
                obj.SetActive(false);
            }
        }

        // Nonaktifkan ProgressBar jika ditemukan di dalam Canvas
        GameObject progressBar = GameObject.Find("Canvas/ProgressBar");
        if (progressBar != null)
            progressBar.SetActive(false);

        // Tampilkan panel quiz, sembunyikan feedback
        quizPanel.SetActive(true);
        feedbackPanel.SetActive(false);

        // Aktifkan auto resize soal
        if (questionText != null)
        {
            questionText.enableAutoSizing = true;
            questionText.fontSizeMax = 24f;
            questionText.fontSizeMin = 12f;
        }

        this.gameObject.SetActive(true);
        StartQuiz();

        // Nonaktifkan semua script di Main Camera
        GameObject mainCamera = GameObject.Find("Main Camera");
        if (mainCamera != null)
        {
            MonoBehaviour[] scripts = mainCamera.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }

    }

    public void StartQuiz()
    {
        StartCoroutine(FetchSoalFromAPI());
    }

    IEnumerator FetchSoalFromAPI()
    {
        string url = "https://semenjana.biz.id/api/soal.php";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            SoalResponse response = JsonUtility.FromJson<SoalResponse>(FixJson(request.downloadHandler.text));
            TampilkanSoal(response.data);
        }
        else
        {
            Debug.LogWarning("Gagal ambil soal dari API, fallback ke lokal.");
            LoadSoalFromLocal();
        }
    }

    void LoadSoalFromLocal()
    {
        string[] options = { "data/umum" };
        string chosenFile = options[Random.Range(0, options.Length)];
        TextAsset json = Resources.Load<TextAsset>(chosenFile);

        if (json != null)
        {
            string jsonWrapper = "{\"list\":" + json.text + "}";
            SoalListWrapper wrapper = JsonUtility.FromJson<SoalListWrapper>(jsonWrapper);

            if (wrapper.list != null && wrapper.list.Count > 0)
            {
                SoalData random = wrapper.list[Random.Range(0, wrapper.list.Count)];
                TampilkanSoal(random);
            }
            else
            {
                questionText.text = "Soal kosong.";
                Debug.LogError("File lokal kosong.");
            }
        }
        else
        {
            questionText.text = "File soal tidak ditemukan.";
            Debug.LogError("File lokal tidak ditemukan.");
        }
    }

    void TampilkanSoal(SoalData soal)
    {
        quizPanel.SetActive(true);
        feedbackPanel.SetActive(false);

        questionText.text = soal.pertanyaan;
        correctOption = soal.jawaban_benar;

        SetButton(buttonA, "A", soal.pilihan.A);
        SetButton(buttonB, "B", soal.pilihan.B);
        SetButton(buttonC, "C", soal.pilihan.C);
        SetButton(buttonD, "D", soal.pilihan.D);
    }

    void SetButton(Button btn, string optionKey, string optionText)
    {
        TextMeshProUGUI textUI = btn.GetComponentInChildren<TextMeshProUGUI>();
        textUI.text = optionKey + ". " + optionText;
        textUI.color = Color.white;
        textUI.enableAutoSizing = true;
        textUI.fontSizeMin = 8f;
        textUI.fontSizeMax = 22f;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => CheckAnswer(optionKey));
    }

    void CheckAnswer(string pilihan)
    {
        if (pilihan == correctOption)
        {
            feedbackText.text = "Jawaban benar";
            feedbackText.color = Color.green;
            feedbackPanel.SetActive(true);
            StartCoroutine(HandleQuizCompletion());
        }
        else
        {
            feedbackText.text = "Jawaban salah";
            feedbackText.color = Color.red;
            feedbackPanel.SetActive(true);
        }
    }

    IEnumerator HandleQuizCompletion()
    {
        yield return new WaitForSeconds(2f); // Tampilkan feedback dulu

        feedbackPanel.SetActive(false);
        quizPanel.SetActive(false);

        // Tampilkan Ready Panel
        if (readyPanel != null)
        {
            readyPanel.SetActive(true);
            yield return new WaitForSeconds(1.5f); // Durasi Ready
            readyPanel.SetActive(false);
        }

        // Tampilkan Go Panel
        if (goPanel != null)
        {
            goPanel.SetActive(true);

            yield return new WaitForSeconds(1f); // Durasi Go
            goPanel.SetActive(false);
            player.SetActive(true);

            progressBar.SetActive(true);
        }

        // Aktifkan semua object lain (kecuali Quiz dan Feedback)
        yield return new WaitForSeconds(delayBeforeActivate);

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            if (obj != this.gameObject && obj != quizPanel && obj != feedbackPanel && obj != readyPanel && obj != goPanel)
            {
                if (obj.name != "Main Camera") // biarkan Main Camera tetap aktif
                    obj.SetActive(true);
            }
        }
        // Aktifkan kembali semua script di Main Camera setelah quiz selesai
        GameObject mainCamera = GameObject.Find("Main Camera");
        if (mainCamera != null)
        {
            MonoBehaviour[] scripts = mainCamera.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }
        }

    }


    [System.Serializable]
    public class SoalResponse
    {
        public string status;
        public string message;
        public SoalData data;
    }

    [System.Serializable]
    public class SoalData
    {
        public int id;
        public string pertanyaan;
        public Pilihan pilihan;
        public string jawaban_benar;
        public string kategori;
    }

    [System.Serializable]
    public class Pilihan
    {
        public string A;
        public string B;
        public string C;
        public string D;
    }

    [System.Serializable]
    public class SoalListWrapper
    {
        public List<SoalData> list;
    }

    string FixJson(string rawJson)
    {
        return rawJson.Replace("\"pilihan\":{", "\"pilihan\":{\n")
                      .Replace("},\"jawaban_benar", "\n},\"jawaban_benar");
    }
}
