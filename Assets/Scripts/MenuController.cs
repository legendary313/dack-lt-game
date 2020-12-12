using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject shopMenu;
    private void Start(){

    }

    private void Update()
    {
    }

    public void showShopMenu()
    {
        shopMenu.SetActive(true);
    }

    public void hideShopMenu()
    {
        shopMenu.SetActive(false);
    }

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void quitGame()
    {
        Application.Quit();
    }


}
