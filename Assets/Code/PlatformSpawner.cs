using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlatformSpawner : MonoBehaviour
{

    public GameObject Platform;//appelle de notre platefrom

    public float distanceMin = 1.5f;
    public float distanceMax = 3.0f;
    public float limiteEcranX = 5f;

    //public Camera localCamera;
    
    private float distancePlatformBase = -2.0f; //première plateform ici en -2

    private List<GameObject> LPlatforms = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            SpawnPlatform();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(distancePlatformBase < 10.0f)
        {
            SpawnPlatform();
        }

        for(int i = LPlatforms.Count - 1; i >= 0; i--)
        {
            GameObject plat = LPlatforms[i];

            if(plat != null && plat.transform.localPosition.y < -8.0f)
            {
                Destroy(plat);
                LPlatforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        float rX = Random.Range(-limiteEcranX,limiteEcranX);
        distancePlatformBase += Random.Range(distanceMin,distanceMax);

        GameObject newPlatform = Instantiate(Platform, this.transform.parent);
        //crée un clone de notre platform a l'endroit crée aléatoirement selon nos conditions

        newPlatform.transform.localPosition = new Vector3(rX,distancePlatformBase, 0f);

        LPlatforms.Add(newPlatform);
        //ajout pour pouvoir s'en souvenir
    }

    public void MouvementPlatforme(float deplacementY)
    {
        foreach(GameObject plat in LPlatforms)
        {
            if(plat != null)
            {
                plat.transform.localPosition -= new Vector3(0,deplacementY,0);
            }
        }

        distancePlatformBase -= deplacementY;
    }

    public void ResetSpawner()
    {
        foreach (GameObject platform in LPlatforms)
        {
            if(platform != null)
        {
            Destroy(platform);
        }
        }
        LPlatforms.Clear(); // vide la liste

        distancePlatformBase = -2f;//reset la hauteur de la platform de base

        GameObject basePlatform = Instantiate(Platform,this.transform.parent);//la mets dans le bon clone

        basePlatform.transform.localPosition = new Vector3(0f, distancePlatformBase, 0f);

        LPlatforms.Add(basePlatform);

        

        for (int i = 0; i < 7; i++)
        {
        SpawnPlatform();//on s'occupe de faire spanw a nouveau les platform de l'écran de base
        }
    }
}
