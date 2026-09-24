using Unity.VisualScripting;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject Platform;//appelle de notre platefrom

    public float distanceMin = 1.5f;
    public float distanceMax = 3.0f;

    private float distanceY = -2.0f; //première plateform ici en -2
    private float limiteEcranX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float hauteurCamera = Camera.main.orthographicSize;
        limiteEcranX = (hauteurCamera * Camera.main.aspect) - 0.5f;

        for(int i = 0; i < 10; i++)
        {
            SpawnPlatform();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main.transform.position.y > distanceY -10.0f)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        float rX = Random.Range(-limiteEcranX,limiteEcranX);
        distanceY += Random.Range(distanceMin,distanceMax);

        Vector2 posXY = new Vector2(rX,distanceY);

        Instantiate(Platform,posXY,Quaternion.identity);
        //crée un clone de notre platform a l'endroit crée aléatoirement selon nos conditions
    }
}
