using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMinion : MonoBehaviour
{
	public GameObject bulletPrefab;
	public Transform bulletPosition;
	public float fireRate = 1.0f;
	private float timer;

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			Fire();
			timer = fireRate;
		}
	}

	private void Fire()
	{
		GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, bulletPosition.rotation);
		BasicEnemyBullet enemyBullet = bullet.GetComponent<BasicEnemyBullet>();

		if (enemyBullet != null)
		{
			Vector2 fireDirection = -transform.right;
			enemyBullet.SetFireDirection(fireDirection);
		}
	}
}
