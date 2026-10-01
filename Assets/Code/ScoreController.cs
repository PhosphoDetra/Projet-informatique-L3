using UnityEngine;
using TMPro;//pour pouvoir modifier le mesh/text

public class ScoreController : MonoBehaviour
{

    public Transform player;

    private TMP_Text scoreTxt;
    
    private float score = 0.0f; //pour avoir en mémoire la hauteur max

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scoreTxt = GetComponent<TMP_Text>();

        if(scoreTxt != null) 
        {
            UpdateScoreDisplay();
        }
    }

    // le Joueur appellera cette fonction quand le décor descend
    public void AjouterScore(float points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    // le Joueur appellera cette fonction quand il meurt
    public void ResetScore()
    {
        score = 0.0f;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreTxt != null) 
        {
        scoreTxt.text = "Score: " + Mathf.RoundToInt(score * 10.0f).ToString();
        }
    }
}
