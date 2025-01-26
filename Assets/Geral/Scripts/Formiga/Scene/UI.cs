using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public AudioSource data;
    public void play(){
        StartCoroutine(PlaySound("SelectMode"));
    }

    IEnumerator PlaySound(string scene)
    {
        MenuSoundManager.PlaySound(MenuSoundType.ButtonClick);
        yield return new WaitForSeconds(0.35f);
        SceneManager.LoadScene(scene);
    }

    public void credits()
    {
        StartCoroutine(PlaySound("Credits"));
    }

    public void backToMenu()
    {
        StartCoroutine(PlaySound("MenuInicio"));
        Time.timeScale = 1f;
    }

    public void loadRegras()
    {
        StartCoroutine(PlaySound("Rules 1"));
    }

    public void loadGame1()
    {
        StartCoroutine(PlaySound("MapaJogo"));
    }

    public void loadGame2()
    {
        StartCoroutine(PlaySound("Mapa"));
    }

    public void exit(){
        MenuSoundManager.PlaySound(MenuSoundType.ButtonClick);
        Application.Quit();

     }
}
