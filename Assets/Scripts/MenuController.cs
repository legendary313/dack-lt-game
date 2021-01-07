using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu;

    private AudioSource audioSource;
    public AudioClip selectSound;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
    }

    public void showShopMenu()
    {
        audioSource.PlayOneShot(selectSound);
        shopMenu.SetActive(true);
    }

    public void hideShopMenu()
    {
        audioSource.PlayOneShot(selectSound);
        shopMenu.SetActive(false);
    }

    public void playGame()
    {
        audioSource.PlayOneShot(selectSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void quitGame()
    {
        audioSource.PlayOneShot(selectSound);
        Application.Quit();
    }


}
