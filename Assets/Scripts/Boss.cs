using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int health;
    public int damage;
    private bool isDead;

    public List<GameObject> bulletPosition = new List<GameObject>();



    private Animator anim;
    public Slider healthSlider;
    private GameObject player;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 25)
        {
            anim.SetTrigger("StageTwo");
        }

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            Destroy(this.gameObject);
        }

        healthSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Taking Damage" + health);
        if (health <= 0)
        {
            Player player = FindObjectOfType<Player>();
            player.IncrementDestroyedEnemyCount();
            WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
            waveSpawner.SomeoneIsKilled();
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDead == false)
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void Fire()
    {
        GameObject bullet = ObjectPool.instance.GetPool();

        foreach (Transform t in bullet.transform)
        {
            if (bullet != null)
            {
                bullet.transform.position = t.position;
                bullet.SetActive(true);
            }
        }
    }
}
