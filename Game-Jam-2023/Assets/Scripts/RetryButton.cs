using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameObject gameOver, player;

	public void OnPointerClick(PointerEventData eventData)
	{
		Time.timeScale = 1;
		HealthBar.HB.SetMaxHealth(100);
		gameOver.SetActive(false);
		player.SetActive(true);
		Scene s = SceneManager.GetActiveScene();
		SceneManager.LoadScene(s.name);
	}
}