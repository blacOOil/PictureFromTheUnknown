using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject Current_PlayerChar;

    [Header("Player Ability")]
    public MovementController MoveController;
    public ConversationManager Talking;
    public CameraController Use_Camera;

    [Header("Player Action")]
    public  CamSwicher SwichGameCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
