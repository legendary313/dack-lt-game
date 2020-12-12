using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Input = UnityEngine.Input;

public class InputPlayer : MonoBehaviour
{
    
    private PlayerBehaviour player;
    [SerializeField] Image powerBar;
    private float inputForce;
    private float holdTime;
    [SerializeField] float jumpDir;

    [SerializeField] private Animator playerAnim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        powerBar.fillAmount = 0f;
        playerAnim = GameObject.FindGameObjectWithTag("Player").transform.Find("Skin").GetComponent<Animator>();
    }


    public void OnPointerDown()
	{
		powerBar.fillAmount = 0f;
		StartCoroutine ("StartCounting");
	}

	public void OnPointerUp()
	{
		StopCoroutine ("StartCounting");
		if (holdTime < 0.3f)
			player.Jump(2f*player.jumpHigh,jumpDir * player.jumpFar);
		else
			player.Jump(holdTime * 20f*player.jumpHigh,jumpDir * player.jumpFar);
        
		powerBar.fillAmount = 0f;
        Debug.Log(jumpDir);
	}

	IEnumerator StartCounting()
	{   
        playerAnim.SetBool("Charging",true);
        while (true)
        {
            for (holdTime = 0f; holdTime <= 1f; holdTime += Time.deltaTime) {
                powerBar.fillAmount = holdTime;
                yield return new WaitForSeconds (Time.deltaTime);
            }
        
            for (holdTime = 1f; holdTime >= 0f; holdTime -= Time.deltaTime) {
                powerBar.fillAmount = holdTime;
                yield return new WaitForSeconds (Time.deltaTime);
            }
        }
	}
}
