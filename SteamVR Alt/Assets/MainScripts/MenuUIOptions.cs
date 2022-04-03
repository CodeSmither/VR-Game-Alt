using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MenuUIOptions : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private GameObject OptionsMenu;
    [SerializeField] private GameObject HowtoPlay;
    [SerializeField] private PlayerHealth playerHealth;
    private int Volume = 50;
    [SerializeField] private Text VolumeText;
    [SerializeField] private TotalScore totalScore;
    private bool ReadInstructions;
    private GameObject QuestionBoard;

    private void Awake()
    {
        OptionsMenu = GameObject.Find("OptionsMenu");
        player = GameObject.Find("Player");
        Invoke("MenuDisable",0.1f);
        totalScore = gameObject.GetComponent<TotalScore>();
        ReadInstructions = false;
        QuestionBoard = GameObject.Find("QuestionBoard");
        
    }

    private void MenuDisable()
    {
        // Disables all Extra UI after it's reference has been collected to prevent errors
        if (OptionsMenu.activeInHierarchy == true)
        {
            OptionsMenu.SetActive(false);
        }
        if (HowtoPlay.activeInHierarchy ==true)
        {
            HowtoPlay.SetActive(false);
        }
        if (QuestionBoard.activeInHierarchy == true)
        {
            QuestionBoard.SetActive(false);
        }
    }
    // sets the volume text to the static volume from the jukebox script
    private void FixedUpdate()
    {
        VolumeText.text = "Volume: " + Volume.ToString();
    }
    // starts the game but also checks that the instructions have been read if they have it then teleports the player into the game
    public void EntertheGame()
    {
        if (ReadInstructions == true)
        {
            player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            OptionsMenu.SetActive(false);
            playerHealth.InGame = true;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
        else if (ReadInstructions == false)
        {
            QuestionBoard.SetActive(true);
            ReadInstructions = true;
        }
        
    }
    // enables the options menu
    public void EnableOptions()
    {
        if(OptionsMenu.activeInHierarchy == false)
        {
            OptionsMenu.SetActive(true);
        }
        else if (OptionsMenu.activeInHierarchy == true)
        {
            OptionsMenu.SetActive(false);
        }
    }
    // enables the HowToPlay screen
    public void EnableHowTo()
    {
        if (HowtoPlay.activeInHierarchy == false)
        {
            HowtoPlay.SetActive(true);
        }
        else if (HowtoPlay.activeInHierarchy == true)
        {
            HowtoPlay.SetActive(false);
        }
    }
    // increases the volume
    public void VolumeUp()
    {
        if(Volume < 100)
        {
            Volume += 5;
            JukeBoxAudio.Volume = Volume / 100f;
        }
        
    }
    // decreases the volume
    public void VolumeDown()
    {   if(Volume > 0)
        {
            Volume -= 5;
            JukeBoxAudio.Volume = Volume / 100f;
        }
    }
    // restarts the game by reseting the player's health and score and ending the game
    public void Restart()
    {
        playerHealth.Restart();
        totalScore.Restart();
    }
}
