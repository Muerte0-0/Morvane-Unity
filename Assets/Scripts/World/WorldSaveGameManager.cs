using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
	public static WorldSaveGameManager Instance { get; private set; }

	[Header("World Index")]
	[SerializeField] private int worldSceneIndex = 1;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void StartNewGame()
	{
		SceneManager.LoadScene(worldSceneIndex);
	}
}