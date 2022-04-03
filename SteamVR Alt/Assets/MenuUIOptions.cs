using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MenuUIOptions : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private GameObject OptionsMenu;
    [SerializeField] private PlayerHealth playerHealth;
    private int Volume = 50;
    [SerializeField] private Text VolumeText;
    [SerializeField] private TotalScore totalScore;

    private void Awake()
    {
        OptionsMenu = GameObject.Find("OptionsMenu");
        player = GameObject.Find("Player");
        Invoke("MenuDisable",0.1f);
        totalScore = gameObject.GetComponent<TotalScore>();
        
    }

    private void MenuDisable()
    {
        if (OptionsMenu.activeInHierarchy == true)
        {
            OptionsMenu.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        VolumeText.text = "Volume: " + Volume.ToString();
    }
    public void EntertheGame()
    {
        player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        OptionsMenu.SetActive(false);
        playerHealth.InGame = true;
        player.GetComponent<NavMeshAgent>().enabled = true;
    }
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
    public void VolumeUp()
    {
        if(Volume < 100)
        Volume += 5;
    }
    public void VolumeDown()
    {   if(Volume > 0)
        {
            Volume -= 5;
        }
    }

    public void Restart()
    {
        playerHealth.Restart();
        totalScore.Restart();
    }
}
