
using UnityEngine;

public class CameraF : MonoBehaviour
{

    public Transform target; //joueur qui sera suivie

    // Update is called once per frame
    void LateUpdate()
    {
        if(target.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3(transform.position.x,target.position.y,transform.position.z);
            transform.position = newPos;
        }
    }
}
