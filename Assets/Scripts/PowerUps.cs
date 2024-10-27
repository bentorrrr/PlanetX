using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum PowerUpType
    {
        Speed,
        FireRate,
        BulletSize,
    }

    public PowerUpType type;
    public float fireRateDivider = 2f;
    public float bulletSizeMultiplier = 2f;
    public float speedMultiplier = 2f;
    private GameObject player;
    void Start()
    {
        if (type == PowerUpType.Speed || type == PowerUpType.FireRate)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.CompareTag("Player"))
        {
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
