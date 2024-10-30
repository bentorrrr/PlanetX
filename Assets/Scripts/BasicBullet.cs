using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
	private float speed = 10f;
	private Rigidbody2D rb;
	private Vector2 fireDirection = Vector2.right;
	public int damage = 1;

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
				enemy.TakeDamage(damage);
			}
			gameObject.SetActive(false);
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
			gameObject.SetActive(false);
		}
	}
}
