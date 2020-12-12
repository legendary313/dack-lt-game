using System.IO;
using UnityEngine;
public static class SaveLoadData
{
    public static void SavePositionData(PlayerBehaviour player){
        PlayerPrefs.SetFloat("x",player.transform.position.x);
        PlayerPrefs.SetFloat("y",player.transform.position.y);
        PlayerPrefs.SetFloat("z",player.transform.position.z);
    }

}
