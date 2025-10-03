using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactive_Item : MonoBehaviour
{
    [Header("Talking Type")]
    public TextMeshProUGUI SpekernameText, SpeakingText;
    public string Speakername;
    public int SpeakingProgress = 0;
    public List<string> Yapping;

    [Header("|Player Checker|")]
    public float PlayerCheckerRadius = 2f;
    public GameObject Player, Interacted_Butt, GameManagers;
    public bool ToggleTalking;


    void Start()
    {
        ToggleTalking = false;
        GameManagers = GameObject.Find("GameManagers");

        if (Interacted_Butt != null)
            Interacted_Butt.SetActive(false);
        // Find UI texts
        GameObject SpekernameTextobj = GameObject.Find("Speakername");
        if (SpekernameTextobj != null)
            SpekernameText = SpekernameTextobj.GetComponent<TextMeshProUGUI>();

        GameObject SpekerTextobj = GameObject.Find("TalkingDes");
        if (SpekerTextobj != null)
            SpeakingText = SpekerTextobj.GetComponent<TextMeshProUGUI>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (Player == null) return;

        if (IsplayerClose())
        {
            if (Interacted_Butt != null)
                Interacted_Butt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!ToggleTalking) // Start conversation
                {
                    SpeakingProgress = 0;
                    Starttalking();
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
            if (Interacted_Butt != null)
                Interacted_Butt.SetActive(false);

            if (ToggleTalking) // If player walks away mid-talk
                Stoptalking();
        }
    }


    private bool CheckProximity(string tag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
                return true;
        }
        return false;
    }

    public bool IsplayerClose()
    {
        return CheckProximity("Player");
    }


    public void Starttalking()
    {
        ToggleTalking = true;
        GameManagers.GetComponent<GameManager>().Istalking = true;

        Player.GetComponent<MovementController>().IsMoveable = false;

        if (SpekernameText != null) SpekernameText.text = Speakername;
        if (SpeakingText != null) SpeakingText.text = Yapping[SpeakingProgress];
    }


    public void Stoptalking()
    {
        GameManagers.GetComponent<GameManager>().Istalking = false;
        Player.GetComponent<MovementController>().IsMoveable = true;

        SpeakingProgress = 0;
        ToggleTalking = false;

        if (SpekernameText != null) SpekernameText.text = "";
        if (SpeakingText != null) SpeakingText.text = "";
    }
}
