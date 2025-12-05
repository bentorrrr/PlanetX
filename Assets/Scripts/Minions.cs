using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Minions : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int health = 1;

    private Rigidbody2D rb;
	private SpriteRenderer sp;

	public Color flashColor;
	public Color regularColor;
	public float flashDuration = 0.1f;
	public int numberOfFlashes = 1;

	public int score;

	[SerializeField] private ParticleSystem destroyedParticle;
	private ParticleSystem destroyedParticleInstance;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		sp = GetComponentInChildren<SpriteRenderer>();
	}
    private void FixedUpdate()
    {
        rb.velocity = -(transform.right * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BackWall"))
		{
			WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
			waveSpawner.SomeoneIsKilled();
			Destroy(gameObject);
		}
    }

    public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			SpawnDestroyedParticles();
			Player player = FindObjectOfType<Player>();
			player.IncrementDestroyedEnemyCount();
			player.score += score;
			WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
			waveSpawner.SomeoneIsKilled();
			Destroy(gameObject);
		}
		StartCoroutine(IFlash());
	}

	private IEnumerator IFlash()
	{
		int flash = 0;
		while (flash < numberOfFlashes)
		{
			sp.color = flashColor;
			yield return new WaitForSeconds(flashDuration);
			sp.color = regularColor;
			yield return new WaitForSeconds(flashDuration);
			flash++;
		}
	}

	private void SpawnDestroyedParticles()
	{
		destroyedParticleInstance = Instantiate(destroyedParticle, transform.position, Quaternion.identity);
	}
}
