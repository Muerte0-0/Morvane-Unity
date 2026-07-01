using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class TitleScreenManager : MonoBehaviour
{
	[Header("Settings")]
	private Coroutine fadeInCoroutine;
	private Coroutine fadeOutCoroutine;
	[SerializeField] private float fadeInDuration = 0.5f;
	[SerializeField] private float fadeOutDuration = 0.5f;

	private void Start()
	{
		foreach (Transform T in gameObject.transform)
		{
			T.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
			T.gameObject.SetActive(true);
		}

		gameObject.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha = 1f;
	}

	public void StartGame()
	{
		NetworkManager.Singleton.StartHost();
	}

	public void ShowCanvas(CanvasGroup canvasToShow)
	{
		FadeCanvas(canvasToShow, false);
	}

	public void HideCanvas(CanvasGroup canvasToHide)
	{
		FadeCanvas(canvasToHide, true);
	}

	private void FadeCanvas(CanvasGroup canvasToFade, bool fadeOut)
	{
		if (fadeOut)
		{
			if (fadeOutCoroutine != null)
				StopCoroutine(fadeOutCoroutine);

			fadeOutCoroutine = StartCoroutine(FadeOutCanvas(fadeOutDuration, canvasToFade));
		}
		else
		{
			if (fadeInCoroutine != null)
				StopCoroutine(fadeInCoroutine);

			fadeInCoroutine = StartCoroutine(FadeInCanvas(fadeInDuration, canvasToFade));
		}
	}

	private IEnumerator FadeInCanvas(float duration, CanvasGroup canvasToFadeIn)
	{
		float timeElapsed = 0f;
		float startAlpha = 0f;

		while (timeElapsed <= duration)
		{
			canvasToFadeIn.alpha = Mathf.Clamp(Mathf.Lerp(startAlpha, 1f, timeElapsed / duration), 0f, 1f);

			timeElapsed += Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator FadeOutCanvas(float duration, CanvasGroup canvasToFadeOut)
	{
		float timeElapsed = 0f;
		float startAlpha = canvasToFadeOut.alpha;

		while (timeElapsed <= duration)
		{
			canvasToFadeOut.alpha = Mathf.Clamp(Mathf.Lerp(startAlpha, 0f, timeElapsed / duration), 0f, 1f);

			timeElapsed += Time.deltaTime;
			yield return null;
		}
	}

	public void StartNewGame()
	{
		WorldSaveGameManager.Instance.StartNewGame();
	}
}
