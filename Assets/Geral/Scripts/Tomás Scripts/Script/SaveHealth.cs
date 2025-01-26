using UnityEngine;

public class SaveHealth : MonoBehaviour
{
    public static int Health
    {
        get => PlayerPrefs.GetInt("Health", 3);
        set => PlayerPrefs.SetInt("Health", value);
    }
    public static int Health1
    {
        get => PlayerPrefs.GetInt("Health1", 3);
        set => PlayerPrefs.SetInt("Health1", value);
    }
    public static int HealthIA1
    {
        get => PlayerPrefs.GetInt("HealthIA1", 3);
        set => PlayerPrefs.SetInt("HealthIA1", value);
    }
    public static int HealthIA2
    {
        get => PlayerPrefs.GetInt("HealthIA2", 3);
        set => PlayerPrefs.SetInt("HealthIA2", value);
    }
    public static int HealthIA3
    {
        get => PlayerPrefs.GetInt("HealthIA3", 3);
        set => PlayerPrefs.SetInt("HealthIA3", value);
    }

    public static void Save() { PlayerPrefs.Save(); }
    public static void Delete() { PlayerPrefs.DeleteAll(); }
}
