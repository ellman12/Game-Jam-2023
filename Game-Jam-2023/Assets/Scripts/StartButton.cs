using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameObject mainMenu, player;

	private void Start()
	{
		player.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		mainMenu.SetActive(false);
		player.SetActive(true);
	}
}