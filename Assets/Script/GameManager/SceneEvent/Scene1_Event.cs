using System.Reflection;
using UnityEngine;

public class Scene1_Event : MonoBehaviour
{
    public int GameStateNum;
    public GameObject GameManagers;
    public bool TaskGiven01;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStateNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateNum == 0)
        {
            IntroState();
            TaskGiven01 = false;
        }
        else if (GameStateNum == 1) 
        {
            ExpositionState();
        }
    }
    public void IntroState()
    {

    }
    public void ExpositionState()
    {
        var gm = GameManagers.GetComponent<GameManager>();

        if (!TaskGiven01)
        {
            gm.TaskDatas.Add(new TaskData { TasknameTemp = "Speaker Picture", TaskTargetTemp = "" });
            gm.TaskDatas.Add(new TaskData { TasknameTemp = "Supporter Picture", TaskTargetTemp = "" });
            gm.TaskDatas.Add(new TaskData { TasknameTemp = "Soldier Picture", TaskTargetTemp = "" });
            gm.TaskDatas.Add(new TaskData { TasknameTemp = "Symbol Picture", TaskTargetTemp = "" });
            gm.TaskDatas.Add(new TaskData { TasknameTemp = "Happy Children Picture", TaskTargetTemp = "" });

            // Tell GameManager how many tasks exist
            gm.TaskNumber = gm.TaskDatas.Count;
          

            TaskGiven01 = true;
        }
    }


}
