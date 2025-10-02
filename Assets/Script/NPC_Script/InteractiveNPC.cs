using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;

public class InteractiveNPC : MonoBehaviour
{
    [Header("Talking Type")]
    public TextMeshProUGUI SpekernameText, SpeakingText;
    public string Speakername;
    public int SpeakingProgress = 0;
    public List<string> Yapping;

    [Header("|Player Checker|")]
    public float PlayerCheckerRadius;
    public GameObject Player, Interacted_Butt, GameManagers;
    public Vector3 StartPosition;
    public bool ToggleTalking;
    public string TaskDescription;

    [Header("|Player|")]
    public bool IsThisAiTaskGiver, IsTaskGiven, Istaskalreadygiven;
    public string TargetTag, taskname;


    // Start is called before the first frame update
    void Start()
    {
        ToggleTalking = false;
        GameManagers = GameObject.Find("GameManagers");
        StartPosition = (gameObject.transform.position).normalized;
        
        Interacted_Butt.SetActive(false);
        Istaskalreadygiven = false;

        GameObject SpekernameTextobj = GameObject.Find("Speakername");
        SpekernameText = SpekernameTextobj.GetComponent<TextMeshProUGUI>();

        GameObject SpekerTextobj = GameObject.Find("TalkingDes");
        SpeakingText = SpekerTextobj.GetComponent<TextMeshProUGUI>();

        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) return;
        if (IsplayerClose())
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            Interacted_Butt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!ToggleTalking) // If conversation hasn't started
                {
                    ToggleTalking = true;
                    SpeakingProgress = 0; // Ensure we start at the beginning
                    Starttalking();

                    if (IsThisAiTaskGiver)
                    {
                        GiveTask();
                    }
                }
                else // Continue conversation
                {
                    if (SpeakingProgress < Yapping.Count - 1)
                    {
                     
                        SpeakingProgress++;
                        Starttalking();
                    }
                    else // End conversation
                    {
                        Stoptalking();
                       
                    }
                }
            }
             
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(StartPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            Interacted_Butt.SetActive(false);
            ToggleTalking = false;
        }
        if (ToggleTalking == true)
        {
            GameManagers.GetComponent<GameManager>().Istalking = true;
        }
        else
        {
            GameManagers.GetComponent<GameManager>().Istalking = false;
        }
    }
    private bool CheckProximity(string tag)
    {
        // Check all layers to capture all objects tagged with the given tag
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
            {

                return true;
            }
        }
        return false;
    }
    public bool IsplayerClose()
    {
        return CheckProximity("Player");
    }

    public void Starttalking()
    {
        Player.GetComponent<MovementController>().IsMoveable = false;
        SpekernameText.text = Speakername;
        SpeakingText.text = Yapping[SpeakingProgress];

    }
    public void Stoptalking()
    {
        Player.GetComponent<MovementController>().IsMoveable = true; 
        SpeakingProgress = 0;
        ToggleTalking = false;
    }
    public void GiveTask()
    {
        if (Istaskalreadygiven == false)
        {
            GameManagers.GetComponent<GameManager>().TasknameTemp = taskname;
            GameManagers.GetComponent<GameManager>().TaskTargetTemp = TargetTag;
            GameManagers.GetComponent<GameManager>().TaskNumber++;

            Istaskalreadygiven = true;
        }

    }
}
