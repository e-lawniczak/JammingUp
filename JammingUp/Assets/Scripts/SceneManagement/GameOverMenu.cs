using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    [SerializeField] GameObject text;
    TextMeshProUGUI textMeshPro;
    private int finalScore;
    private int maxCombo;

    private void Awake()
    {
        textMeshPro = text.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        finalScore = PlayerPrefs.GetInt("score");
        maxCombo = PlayerPrefs.GetInt("maxCombo");
        textMeshPro.text = string.Format("Score: {0}\nHighest Combo: {1}", finalScore, maxCombo);
        textMeshPro.alignment = TextAlignmentOptions.Center;
    }
    public void RestartGame(){
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame(){
        Application.Quit();
        
    }
}
