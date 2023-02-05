using System;
using UnityEngine;

public class WallEnemyProjectile : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private int damage;

	[NonSerialized] public Transform target;
	[NonSerialized] public Vector3 endPos;

	private void Update()
	{
		if (target == null)
			transform.position += Vector3.down * (speed * Time.deltaTime);
		else
			transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			HealthBar.HB.TakeDamage(damage);

		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			HealthBar.HB.TakeDamage(damage);

		Destroy(gameObject);
	}
}