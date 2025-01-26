using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
