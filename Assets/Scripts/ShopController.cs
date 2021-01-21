using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject[] shopItems;   
    private int currentSkinID;

    private AudioSource audioSource;
    public AudioClip selectSound;
    private bool activeSkins= false;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        setupShopView();
        
    }

    void Update()
    {
        
    }

    private void checkCurrentSkin()
    {
        if(PlayerPrefs.HasKey("Skin"))
        {
            currentSkinID = PlayerPrefs.GetInt("Skin");
        }
        else{
            currentSkinID = 0;
        }
    }

    private void checkSkinActive()
    {
        if(PlayerPrefs.HasKey("HighestFloor"))
        {
            if(PlayerPrefs.GetInt("HighestFloor") >=2)
            {
                activeSkins = true;
            }
        }
        if(activeSkins == true)
        {
            for(int i=1;i<shopItems.Length;i++)
            {
                shopItems[i].transform.Find("Button").GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
                shopItems[i].transform.Find("Button").GetComponent<Button>().interactable = true;
            }
        }
        else{
            for(int i=1;i<shopItems.Length;i++)
            {
                shopItems[i].transform.Find("Button").GetComponentInChildren<TextMeshProUGUI>().text = "not unlock";
                shopItems[i].transform.Find("Button").GetComponent<Button>().interactable = false;
            }
        }
    }

    private void setupShopView()
    {
        checkSkinActive();
        checkCurrentSkin();
        shopItems[currentSkinID].transform.Find("Button").GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        shopItems[currentSkinID].transform.Find("Button").GetComponent<Button>().interactable = false;
        PlayerPrefs.SetFloat("High",shopItems[currentSkinID].transform.Find("HighBar").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("Far",shopItems[currentSkinID].transform.Find("FarBar").GetComponent<Slider>().value);
    }

    public void equipSkinButton(int SkinID)
    {
        audioSource.PlayOneShot(selectSound);
        shopItems[currentSkinID].transform.Find("Button").GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        shopItems[currentSkinID].transform.Find("Button").GetComponent<Button>().interactable = true;
        PlayerPrefs.SetInt("Skin",SkinID);
        PlayerPrefs.SetFloat("High",shopItems[currentSkinID].transform.Find("HighBar").GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("Far",shopItems[currentSkinID].transform.Find("FarBar").GetComponent<Slider>().value);
        Debug.Log("Cur Skin: " + PlayerPrefs.GetInt("Skin"));
        setupShopView();
    }
}
