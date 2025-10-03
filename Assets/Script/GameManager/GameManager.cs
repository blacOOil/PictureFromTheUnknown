using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TalkingUi,TaskUI,TaskPrefab,TaskClearerPrefab;
    public bool Istalking,IstoggleESC;
    public GameObject TaskPaper,TaskChecklist;
    public int TaskListCleared = 0,TaskNumber;

    private int previousTaskNumber = -1, previousTaskClear = -1;
    private List<GameObject> TaskList,ClearedList;
    public string TasknameTemp, TaskTargetTemp;

    // Start is called before the first frame update
    void Start()
    {
        TaskUI.SetActive(false);
        TalkingUi.SetActive(false);
        Istalking = false;
        IstoggleESC = false;
        TaskList = new List<GameObject>();
        ClearedList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Istalking == true)
        {
            TalkingUi.SetActive(true);
        }
        else if (Istalking == false)
        {
            TalkingUi.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            IstoggleESC = !IstoggleESC; 
        }
        if(IstoggleESC == true)
        {
            TaskUI.SetActive(true);
        }
        else
        {
            TaskUI.SetActive(false);
        }
        if (TaskNumber != previousTaskNumber)
        {
            ClearPreviousTasks();
            TaskUpdate(); // Call TaskUpdate only when TaskNumber changes
            previousTaskNumber = TaskNumber; // Update the stored value
        }
        if (TaskListCleared != previousTaskClear)
        {
            ClearPreviousClear();
            TaskClearUpdate();
            previousTaskClear = TaskListCleared;
        }
       
    }
    public void TaskUpdate()
    {
        for (int i = 0; i < TaskNumber; i++)
        {
            GameObject newTask = Instantiate(TaskPrefab, TaskPaper.transform);
            newTask.GetComponent<TaskTarget>().taskname = TasknameTemp;
            newTask.GetComponent<TaskTarget>().Targetname = TaskTargetTemp;
            TaskList.Add(newTask);
           
           
        }
    }
    public void TaskClearUpdate()
    {
        for (int i = 0; i < TaskListCleared; i++)
        {
            GameObject newTask = Instantiate(TaskClearerPrefab, TaskChecklist.transform);
            ClearedList.Add(newTask);
        }
    }
    public void ClearPreviousClear()
    {
        if (ClearedList != null)
        {
            foreach (GameObject task in ClearedList)
            {
                if (task != null) Destroy(task);
            }
            ClearedList.Clear();
        }
    }
    public void ClearPreviousTasks()
    {
        if(TaskList != null)
        {
        foreach (GameObject task in TaskList)
        {
            if (task != null) Destroy(task);
        }
        TaskList.Clear();
        }
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
   
}
