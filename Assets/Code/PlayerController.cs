using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; //permet de manipuller des niveaux

//package pour l'IA
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Agent
{
    public float vitesse = 5f;
    public PlatformSpawner spawner;
    public ScoreController scoreManager;
    //public Camera localCamera;
    

    private Rigidbody2D rb;
    private float limiteEcranX = 5f; //delimite l'écran
    private SpriteRenderer spriteRenderer; //pour afficher le bonhomme
    
    private float hauteurMaxAtteinte;//pour pouvoir comparer les scores des agent et leurs ddonner des reocmpenses

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //recup le moteur physique pour le joueur
        spriteRenderer = GetComponent<SpriteRenderer>();//recup le sprite

        /*
        float hauteurCamera = localCamera.orthographicSize; //obtiens la hauteur de la camera
        limiteEcranX = hauteurCamera * localCamera.aspect; //multiplier au ratio 
        */
    }

 

    void Update()
        {
            Vector2 position = transform.localPosition;

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

            //si le joueur depasse le centre de l'écran
            if(position.y > 0f)
            {
                float deplacementY = position.y;
                position.y = 0f;

                AddReward(deplacementY); //recompense car il monte

                if(spawner != null)
                {
                    spawner.MouvementPlatforme(deplacementY);//on fait descendre le monde
                }

                if(scoreManager != null)
                {
                    scoreManager.AjouterScore(deplacementY);
                }
            }

            


            // Met à jour la position
            transform.localPosition = position;

        /*
            float limiteBasEcran = Camera.main.transform.position.y - Camera.main.orthographicSize;

            if(position.y < limiteBasEcran - 1.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //unity recharche le jeux du début
        }
        */
        }

           /*
    Ancien code pour le controle du joueur 
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
        
    }*/

    public override void OnEpisodeBegin()
    {
        //remet le joueur a sa position initale
        transform.localPosition = new Vector3(0,0,0);
        rb.linearVelocity = Vector3.zero;

        hauteurMaxAtteinte = transform.localPosition.y;
        /*
        if(localCamera != null)
        {
            localCamera.transform.localPosition = new Vector3(0,0,localCamera.transform.localPosition.z);
        }
        */
        if(spawner != null)
        {
            spawner.ResetSpawner();//recree l'environnement de 0
        }

        if(scoreManager != null) scoreManager.ResetScore();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //envoie des variables 

        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        //pour la position x et y 

        sensor.AddObservation(rb.linearVelocityY);
        //pour sa vitesse
    
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //l'IA a trois choix 0= rien , 1 = gauche et 2 = droite
        int moveAction = actions.DiscreteActions[0];
        //direction décider gauche ou droite en fonction du signe 
        float moveInput = 0f;

        if (moveAction == 1) {
            moveInput = -1f;
            spriteRenderer.flipX = true;
        }
        if (moveAction == 2) {
            moveInput = 1f;
            spriteRenderer.flipX = false;
            }
        
        rb.linearVelocity = new Vector2(moveInput * vitesse, rb.linearVelocityY);

        //float limiteBasseEcran = localCamera.transform.localPosition.y - localCamera.orthographicSize;

        //limite de la mort 
        if(transform.localPosition.y < -6f)//fix
        {
            SetReward(-1f); //punit l'agent car tomber
            EndEpisode(); //cut de la partie qui redemarre OnEpisodeBegin

        }
        /*
        if(transform.localPosition.y > hauteurMaxAtteinte)
        {
            float difference = transform.localPosition.y - hauteurMaxAtteinte;

            AddReward(difference);//On lui ajoute cette difference en tant que reward

            hauteurMaxAtteinte = transform.localPosition.y;
        }
        */
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = 0; // on ne fait rien

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.qKey.isPressed)
        {
            discreteActions[0] = 1; //gauche
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            discreteActions[0] = 2; //droite
        }
    }
}
