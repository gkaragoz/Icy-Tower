using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    
    public static void PlayJumpSound() {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource auidoSource = soundGameObject.AddComponent<AudioSource>();
        auidoSource.PlayOneShot(GameAssets.instance.jumpSound);

    }
    public static void PlayComboJumpSound() {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource auidoSource = soundGameObject.AddComponent<AudioSource>();
        auidoSource.PlayOneShot(GameAssets.instance.comboJumpSound);

    }
}
