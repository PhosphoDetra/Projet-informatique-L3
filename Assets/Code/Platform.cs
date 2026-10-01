using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpForce = 10f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.y <= 0f)//si l'objet viens de haut 
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();//recup le moteur physique du joueur

            if(rb != null)
            {
                Vector2 linearVelocity = rb.linearVelocity;
                linearVelocity.y = jumpForce;
                rb.linearVelocity = linearVelocity;
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //si parfois la platform se retrouve en dessous d'un certain niveau elle est automatiquement détruite
        if(transform.localPosition.y < -8.0f)
        {
            Destroy(gameObject);
        }
    }
}
