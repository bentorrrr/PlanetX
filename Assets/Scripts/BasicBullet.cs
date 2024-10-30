using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
	private float speed = 10f;
	private Rigidbody2D rb;
	private Vector2 fireDirection = Vector2.right;
	public int damage = 1;

	[SerializeField] private ParticleSystem hitParticle;
	private ParticleSystem hitParticleInstance;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		rb.velocity = fireDirection * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Minions enemy = collision.gameObject.GetComponent<Minions>();
			if (enemy != null)
			{
				SpawnHitParticles();
				enemy.TakeDamage(damage);
			}
			Destroy(gameObject);
		}
		else if (collision.gameObject.CompareTag("Boss"))
		{
			Boss boss = collision.gameObject.GetComponent<Boss>();
			if (boss != null)
			{
				boss.TakeDamage(damage);
			}
			gameObject.SetActive(false);
		}
		else if (collision.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}

	private void SpawnHitParticles()
	{
		hitParticleInstance = Instantiate(hitParticle, transform.position, Quaternion.identity);
	}
}
