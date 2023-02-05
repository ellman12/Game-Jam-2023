using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
	[Header("Button")]
	[SerializeField] private GameObject mainMenu, player;
	[Header("Sound Effects")]
	[SerializeField] private AudioSource menuMusic;
	[SerializeField] private AudioSource menuBlip;

	private void Start()
	{
		player.SetActive(false);
		menuMusic.loop = true;
		menuMusic.Play();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine(PlayBlip());
	}

	IEnumerator PlayBlip()
    {
		menuBlip.Play();
		menuMusic.Stop();
		yield return new WaitForSeconds(0.1f);
		mainMenu.SetActive(false);
		player.SetActive(true);
	}
}