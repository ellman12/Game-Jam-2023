using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private GameObject mainMenu;

	public void OnPointerClick(PointerEventData eventData)
	{
		mainMenu.SetActive(false);
	}
}