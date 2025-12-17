using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player Object")]
    public Transform Player; 

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z); // Sets camera to players position
    }
}