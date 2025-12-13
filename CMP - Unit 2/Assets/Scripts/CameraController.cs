using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player; 

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
    }
}