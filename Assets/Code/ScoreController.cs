using UnityEngine;
using TMPro;//pour pouvoir modifier le mesh/text

public class ScoreController : MonoBehaviour
{

    public Transform player;

    private TextMeshProUGUI scoreTxt;
    private float topScore = 0.0f; //pour avoir en mémoire la hauteur max

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreTxt = GetComponent<TextMeshProUGUI>();

        //init du score du joueur
        if(player != null)
        {
            topScore = player.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ont regarde si le joueur depasse la position de sont meilleure score si oui ont actualise le score
        if(player != null && player.position.y > topScore)
        {
            topScore = player.position.y;
            scoreTxt.text = "Score: "+ Mathf.RoundToInt(topScore * 10.0f).ToString();
        }
    }
}
