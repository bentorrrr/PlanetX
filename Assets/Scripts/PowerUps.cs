using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum PowerUpType
    {
        Speed,
        FireRate,
        BulletSize,
    }

	private Rigidbody2D rb;
    public float floatSpeed = 5f;
	public PowerUpType type;
    public float fireRateDivider = 2f;
    public float bulletSizeMultiplier = 2f;
    public float speedMultiplier = 2f;
    private GameObject player;
    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		if (type == PowerUpType.Speed || type == PowerUpType.FireRate)
            player = GameObject.FindGameObjectWithTag("Player");
    }
	private void FixedUpdate()
	{
		rb.velocity = -(Vector2.right * floatSpeed);
	}

	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("Triggered");
			switch (type)
            {
                case PowerUpType.Speed:
                    player.GetComponent<Player>().SetSpeed(speedMultiplier);
                    this.gameObject.SetActive(false);
                    break;
                case PowerUpType.FireRate:
                    player.GetComponent<Player>().SetFireRate(fireRateDivider);
                    this.gameObject.SetActive(false);
                    break;
                case PowerUpType.BulletSize:
                    break;
            }
        }
    }

}
