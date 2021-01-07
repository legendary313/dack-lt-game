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

    private void setupShopView()
    {
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
