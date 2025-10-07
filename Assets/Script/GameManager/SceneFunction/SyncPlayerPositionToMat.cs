using UnityEngine;

public class SyncPlayerPositionToMat : MonoBehaviour
{
    public static int POSid = Shader.PropertyToID("_Position");
    public static int Sizeid = Shader.PropertyToID("_size");

    public MovementController movementController;

    public float DefaultSize;
    public Material WallMat;
    public Camera Camera;
    public LayerMask LayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DefaultSize = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        var view = Camera.WorldToViewportPoint(transform.position);
        WallMat.SetVector(POSid,view);

        if(movementController.IsInFPS == false)
        {
            WallMat.SetFloat(Sizeid, DefaultSize);
           
        }
        else
        {
            WallMat.SetFloat(Sizeid, 0);
        }

    }
}
