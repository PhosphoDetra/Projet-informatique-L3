using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; //permet de manipuller des niveaux

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float vitesse = 5f;

    private float limiteEcranX; //delimite l'écran
    private SpriteRenderer spriteRenderer; //pour afficher le bonhomme
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //recup le moteur physique pour le joueur
        spriteRenderer = GetComponent<SpriteRenderer>();//recup le sprite

        float hauteurCamera = Camera.main.orthographicSize; //obtiens la hauteur de la camera
        limiteEcranX = hauteurCamera * Camera.main.aspect; //multiplier au ratio 
    }

    void Update()
        {
            Vector2 position = transform.position;

            // Si le joueur sort par la droite, il réapparaît à gauche
            if (position.x > limiteEcranX)
            {
                position.x = -limiteEcranX;
            }
            // Si le joueur sort par la gauche, il réapparaît à droite
            else if (position.x < -limiteEcranX)
            {
                position.x = limiteEcranX;
            }

            // Met à jour la position
            transform.position = position;

            float limiteBasEcran = Camera.main.transform.position.y - Camera.main.orthographicSize;

            if(position.y < limiteBasEcran - 1.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //unity recharche le jeux du début
        }

        }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horInput = 0f;
        if (Keyboard.current != null)
        {
            if(Keyboard.current.leftArrowKey.isPressed || Keyboard.current.qKey.isPressed)
            {
                horInput = -1f;
                spriteRenderer.flipX = true;//retourne le sprite
            }
            if(Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                horInput = 1f;
                spriteRenderer.flipX = false;//le remet comme avant
            }
        }

        rb.linearVelocity = new Vector2(horInput * vitesse, rb.linearVelocityY); //defniit le nouvelle emplacement de rb (du joueur) via l'input horizontalle
        
    }
}
