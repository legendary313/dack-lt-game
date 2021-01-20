using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkinsController : MonoBehaviour
{
    [SerializeField] private GameObject skinObject;
    [SerializeField] private AnimatorOverrideController Jumpking2;
    [SerializeField] private AnimatorOverrideController Jumpking3;
    [SerializeField] private AnimatorOverrideController Jumpking4;
    [SerializeField] private AnimatorOverrideController Jumpking5;

    public void skinChange(int id){
        if(id == 0)
        {
        }
        else if(id == 1)
        {
            skinObject.GetComponent<Animator>().runtimeAnimatorController = Jumpking2 as RuntimeAnimatorController;
        }
        else if(id ==2 )
        {
            skinObject.GetComponent<Animator>().runtimeAnimatorController = Jumpking3 as RuntimeAnimatorController;
        }
        else if(id ==3 )
        {
            skinObject.GetComponent<Animator>().runtimeAnimatorController = Jumpking4 as RuntimeAnimatorController;
        }
        else if(id ==4 )
        {
            skinObject.GetComponent<Animator>().runtimeAnimatorController = Jumpking5 as RuntimeAnimatorController;
        }
    }


}
