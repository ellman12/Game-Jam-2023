using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
	[Header("Button")]
	[SerializeField] private GameObject mainMenu, player;
	[Header("Sound Effects")]
	[SerializeField] private AudioSource menuMusic;

	private void Start()
	{
		player.SetActive(false);
		
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		mainMenu.SetActive(false);
		player.SetActive(true);
		menuMusic.Stop();
	}
}