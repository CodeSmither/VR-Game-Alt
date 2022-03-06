using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIOptions : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private GameObject OptionsMenu;
    [SerializeField] private PlayerHealth playerHealth;
    private int Volume = 50;
    [SerializeField] private Text VolumeText;

    private void Awake()
    {
        OptionsMenu = GameObject.Find("OptionsMenu");
        player = GameObject.Find("Player");
        Invoke("MenuDisable",0.1f);
        
        
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
    }
}
