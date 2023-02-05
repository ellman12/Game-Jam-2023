using System.Collections;
using UnityEngine;

public class WallEnemy : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float shootCooldown;
	[SerializeField] private int damage;
	[SerializeField] private Transform front, target;
	[SerializeField] private WallEnemyProjectile pea;

	private bool cooldownActive;

	private void Update()
	{
		if (target != null)
			transform.up = -(target.position - transform.position);

		if (!cooldownActive)
			StartCoroutine(Shoot());
	}

	private IEnumerator Shoot()
	{
		cooldownActive = true;
		
		WallEnemyProjectile newPea = Instantiate(pea, front.position, Quaternion.identity);

		if (target != null)
		{
			newPea.target = target;
			newPea.transform.up = -(front.position - target.position);
		}
		else
		{
			newPea.transform.up = -(front.position - Vector3.down);
		}
		
		RaycastHit2D hit = Physics2D.Raycast(front.position, newPea.transform.up, 100, layerMask);
		newPea.endPos = hit.point;

		yield return new WaitForSeconds(shootCooldown);
		cooldownActive = false;
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
			HealthBar.HB.TakeDamage(damage);

		Destroy(other.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			HealthBar.HB.TakeDamage(damage);
		
		Destroy(other.gameObject);
	}
}