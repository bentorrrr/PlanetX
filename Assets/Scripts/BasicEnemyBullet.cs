using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBullet : MonoBehaviour
{
	private float speed = 12f;
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

	public void SetFireDirection(Vector2 direction)
	{
		fireDirection = direction.normalized;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Player player = collision.gameObject.GetComponent<Player>();
			if (player != null)
			{
				player.TakeDamage(damage);
			}
			Destroy(gameObject);
		}
		else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("BackWall"))
		{
			Destroy(gameObject);
		}
	}

	private void SpawnHitParticles()
	{
		hitParticleInstance = Instantiate(hitParticle, transform.position, Quaternion.identity);
	}
}