using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float vitesse = 5f;

    private float limiteEcranX; //delimite l'écran

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //recup le moteur physique pour le joueur


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
            }
            if(Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                horInput = 1f;
            }
        }

        rb.linearVelocity = new Vector2(horInput * vitesse, rb.linearVelocityY); //defniit le nouvelle emplacement de rb (du joueur) via l'input horizontalle
        
    }
}
