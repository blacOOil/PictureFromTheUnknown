using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject DefaultCanvas;
    public List<GameObject> SubCanvas;
    public int stateofMenu = 0;

    // Start is called before the first frame update
    void Start()
    {
        DefaultCanvas.SetActive(true);
       
        foreach (GameObject obj in SubCanvas)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (stateofMenu-1 > 0 )
        {
            DefaultCanvas.SetActive(false);
            SubCanvas[stateofMenu].SetActive(true);
        }
        else
        {
            CloseallCanvas();
        }
    }
   public void CloseallCanvas()
    {
        foreach (GameObject obj in SubCanvas)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
    public void UpdateState(int StateNumber)
    {
        stateofMenu = StateNumber;
    }
    public void BackButton()
    {
        stateofMenu = 0;
    }
   
}
