using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskData
{
    public string TasknameTemp = "Test", TaskTargetTemp = "Test";
}
    public class GameManager : MonoBehaviour
{
    public GameObject TalkingUi,TaskUI,TaskPrefab,TaskClearerPrefab;
    public bool Istalking,IstoggleESC;
    public GameObject TaskPaper,TaskChecklist;
    public int TaskListCleared = 0,TaskNumber;

    private int previousTaskNumber = -1, previousTaskClear = -1;
    private List<GameObject> TaskList,ClearedList;

    public List<TaskData> TaskDatas = new List<TaskData>();
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
        // Talking UI
        TalkingUi.SetActive(Istalking);

        // ESC menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IstoggleESC = !IstoggleESC;
        }
        TaskUI.SetActive(IstoggleESC);

        // Task spawning
        if (TaskNumber != previousTaskNumber)
        {
            TaskUpdate();
            previousTaskNumber = TaskNumber;
        }

        // Cleared tasks
        if (TaskListCleared != previousTaskClear)
        {
            ClearPreviousClear();
            TaskClearUpdate();
            previousTaskClear = TaskListCleared;
        }
    }

    public void TaskUpdate()
    {
        // 1. Clear all existing task UI
        ClearPreviousTasks();

        // 2. Spawn a UI element for each task in TaskDatas
        for (int i = 0; i < TaskDatas.Count; i++)
        {
            TaskData task = TaskDatas[i];

            GameObject newTask = Instantiate(TaskPrefab, TaskPaper.transform);
            newTask.GetComponent<TaskTarget>().taskname = task.TasknameTemp;
            newTask.GetComponent<TaskTarget>().Targetname = task.TaskTargetTemp;

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
