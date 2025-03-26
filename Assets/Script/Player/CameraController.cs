using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public MovementController movementController;

    public bool IsHoldCam = false;

    public float zoomSpeed = 2f; // Speed of zooming
    public float minFOV = 10f; // Minimum zoom
    public float maxFOV = 80f; // Maximum zoom

    private Texture2D screenCapture;
   
    
    [Header("Photp Taker")]
    [SerializeField] private Image PhotoDisplayArea ;

    public GameObject PictureShowcase,GameUi,gameManager,Polaroid;
    public bool IsShowingPic;
    public List<GameObject> WorldUiList;
    public RayCasting RayCasting;

    [Header("Film Session")]
    public List<GameObject> filmCaptured;
    public List<Sprite> SpriteList;
    public float  Filmlimite,CurrentFilmed;
    public Sprite FilmSprited;
    public GameObject FilmPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Polaroid.SetActive(false);
        FindWorldUi();
        WorldUiList = new List<GameObject>();
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        IsShowingPic = false;
    }
    public void FindWorldUi()
    {
        GameObject[] FoundObjects = GameObject.FindGameObjectsWithTag("WorldUi");
            WorldUiList.AddRange(FoundObjects); 
    }
    public void DeactivateAllObjects(List<GameObject> objectList)
    {
        foreach (GameObject obj in objectList)
        {
            if (obj != null) // Check if the object is not destroyed
            {
                obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        IsHoldCam = movementController.IsInFPS;
        AdjustFocalLength();
        if (!IsHoldCam)
        {
            Polaroid.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0) && IsHoldCam)
        {
            PhotoCapture();
            IsShowingPic = true;
        }
        else
        {
            IsShowingPic = false;
        }
        if(IsShowingPic == false)
        {
          //  PictureShowcase.SetActive(false);
        }
        else
        {
            PictureShowcase.SetActive(true);
        }
    }
    void PhotoCapture()
    {
        StartCoroutine(CapturePhoto());
        SaveFilming();
    }

    IEnumerator CapturePhoto()
    {
        GameUi.SetActive(false);
        DeactivateAllObjects(WorldUiList);
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        GameUi.SetActive(true);
       // ShowPhoto();
       
    }
    void ShowPhoto()
    {
        Polaroid.SetActive(true);
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f,0.0f,screenCapture.width,screenCapture.height),new Vector2(0.5f,0.5f),100.0f);
        PhotoDisplayArea.sprite = photoSprite;
       
    }

    void AdjustFocalLength()
    {
        if (virtualCamera != null)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // Get scroll input
            if (scrollInput != 0)
            {
                virtualCamera.m_Lens.FieldOfView -= scrollInput * zoomSpeed;
                virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minFOV, maxFOV);
            }
        }
    }
    public void ClearingTask()
    {
        gameManager.GetComponent<GameManager>().TaskListCleared++;
    }
    public void SaveFilming()
    {
        if(CurrentFilmed != Filmlimite)
        {
            ShowPhoto();
            GameObject SavedPic = Instantiate(FilmPrefab, gameObject.transform.position, Quaternion.identity);
            SavedPic.transform.SetParent(gameObject.transform);
           
            Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
            SavedPic.GetComponent<FilmSingle>().FilmSprite = photoSprite;
            filmCaptured.Add(SavedPic);
            CurrentFilmed++;
        }
    }
}
