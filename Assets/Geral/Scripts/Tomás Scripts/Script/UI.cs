using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI TextP1, TextP2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveHealth.Delete();
    }

    // Update is called once per frame
    void Update()
    {
        TextP1.text = SaveHealth.Health.ToString();
        TextP2.text = SaveHealth.Health1.ToString();
    }
}
