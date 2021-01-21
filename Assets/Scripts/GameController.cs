using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private GameObject buttonR;
    [SerializeField] private GameObject buttonL;
    [SerializeField] private GameObject buttonBack;
    [SerializeField] private GameObject powerBar;
    [SerializeField] private GameObject unlockedSkinsNotification;
    private AudioSource audioSource;
    public AudioClip backgroundSound;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        buttonR.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/2,Screen.height);
        buttonL.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/2,Screen.height);
        buttonBack.GetComponent<RectTransform>().position = new Vector2(Screen.width*0.1f,Screen.height*0.9f);
        powerBar.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/1.5f,powerBar.GetComponent<RectTransform>().rect.height);
    }

    // Update is called once per frame
    void Update()
    {   

    }

    public void changeFloorText(string floorName)
    {
        floorText.text = floorName;
        string[] floors = floorName.Split(' ');
        int floorNumber = Int32.Parse(floors[1]);
        if(PlayerPrefs.HasKey("HighestFloor"))
        {
            if(floorNumber>PlayerPrefs.GetInt("HighestFloor"))
            {
                PlayerPrefs.SetInt("HighestFloor",floorNumber);
                if(floorNumber == 2)
                {
                    unlockedSkinsNotification.SetActive(true);
                    StartCoroutine(disactiveNotification());
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighestFloor",floorNumber);
        }
    }

    IEnumerator disactiveNotification()
    {
        yield return new WaitForSeconds(5);
        unlockedSkinsNotification.SetActive(false);
    }

    public void backButton()
    {
        player.gameObject.GetComponent<PlayerBehaviour>().SavePlayerPositionData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}   
