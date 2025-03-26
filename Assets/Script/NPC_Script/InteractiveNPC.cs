using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveNPC : MonoBehaviour
{
    [Header("Talking Type")]
    public TextMeshProUGUI SpekernameText, SpeakingText;
    public string Speakername;
    public List<string> Yapping;

    [Header("|Player Checker|")]
    public float PlayerCheckerRadius;
    public GameObject Player,Interacted_Butt,GameManagers;
    public Vector3 StartPosition;
    public bool ToggleTalking;
    public string TaskDescription;

    [Header("|Player|")]
    public bool   IsThisAiTaskGiver,Istaskalreadygiven;
    public string TargetTag, taskname;


    // Start is called before the first frame update
    void Start()
    {
        ToggleTalking = false;
        GameManagers = GameObject.Find("GameManagers");
        StartPosition = (gameObject.transform.position).normalized;
        Player = GameObject.Find("Player");
        Interacted_Butt.SetActive(false);

        GameObject SpekernameTextobj = GameObject.Find("Speakername");
        SpekernameText = SpekernameTextobj.GetComponent<TextMeshProUGUI>();

        GameObject SpekerTextobj = GameObject.Find("TalkingDes");
        SpeakingText = SpekerTextobj.GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null) return;
        if (IsplayerClose())
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            Interacted_Butt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleTalking = !ToggleTalking; // Simplified toggle logic
                Starttalking();
                GiveTask();
            }
            if (Input.GetMouseButton(0) && Player.GetComponent<CamSwicher>().IsInFPS == true)
            {
                Interacted_Butt.SetActive(false);
            }
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(StartPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            Interacted_Butt.SetActive(false);
            ToggleTalking = false;
        }
        if(ToggleTalking == true)
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
        SpekernameText.text = Speakername;
        SpeakingText.text = Yapping[0];
    }
    public void GiveTask()
    {

        GameManagers.GetComponent<GameManager>().TaskNumber++;
    }
}
