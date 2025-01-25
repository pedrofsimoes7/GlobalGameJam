using UnityEngine;

public class GameManager : MonoBehaviour
{
	
	private static GameManager instance;
	
	public static GameManager getInstacia()
	{
		if(instance == null)	
			instance = new GameManager();
		return instance;
	}
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
