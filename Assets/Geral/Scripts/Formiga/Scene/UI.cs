using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    public void play(){
        SceneManager.LoadScene("SelectMode");
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MenuInicio");
        Time.timeScale = 1f;
    }

    public void loadGame1()
    {
        SceneManager.LoadScene("MapaJogo");
    }

    public void loadGame2()
    {
        SceneManager.LoadScene("Mapa");
    }

    public void exit(){
        Application.Quit();

     }
}
