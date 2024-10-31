using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
	public int health;
	public int damage;
	private bool isDead;
	private bool isActive = false;
	private bool isFlashing = false;

	public Color rageColor;
	public Color flashColor;
	public Color regularColor;
	public float flashDuration = 0.1f;
	public int numberOfFlashes = 1;

	public List<GameObject> bulletPosition = new List<GameObject>();
	public GameObject Phase1Engine;
	public GameObject Phase2Engine;
	private SpriteRenderer sp;
	[SerializeField] private ParticleSystem destroyedParticle;
	private ParticleSystem destroyedParticleInstance;

	private Animator anim;
	public Slider healthSlider;
	private GameObject player;

	public Transform onscreenPosition;
	public float entrySpeed = 5f;

	private WaveSpawner waveSpawner;

	void Start()
	{
		waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
		anim = GetComponent<Animator>();
		sp = GetComponentInChildren<SpriteRenderer>();
		if (sp == null)
		{
			Debug.LogError("SpriteRenderer not found on Boss or any of its children.");
		}
		isActive = false;
		healthSlider.gameObject.SetActive(false);
	}

	void Update()
	{
		if (!isActive) return;

		if (health <= 50)
		{
			waveSpawner.spawnInterval = 3f;
			if (!waveSpawner.bossFifty)
			{
				waveSpawner.spawnTimer = waveSpawner.spawnInterval;
			}
			waveSpawner.bossFifty = true;
		}

		if (health <= 25)
		{
			anim.SetTrigger("StageTwo");
			sp.color = rageColor;
			Phase1Engine.SetActive(false);
			Phase2Engine.SetActive(true);
		}

		if (health <= 0)
		{
			anim.SetTrigger("Death");
			StartCoroutine(SpawnDestroyedParticles());
		}

		healthSlider.value = health;
	}

	public void TakeDamage(int damage)
	{
		if (!isActive) return;

		health -= damage;
		Debug.Log("Taking Damage" + health);

		if (!isFlashing)
		{
			StartCoroutine(IFlash());
		}

		if (health <= 0)
		{
			Player player = FindObjectOfType<Player>();
			player.IncrementDestroyedEnemyCount();
		}
	}

	private IEnumerator IFlash()
	{
		if (isFlashing) yield break;

		isFlashing = true;
		int flash = 0;
		while (flash < numberOfFlashes)
		{
			sp.color = flashColor;
			Debug.Log("FlashCOlor");
			yield return new WaitForSeconds(flashDuration);
			flash++;
		}
		sp.color = regularColor;
		isFlashing = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isActive || isDead) return;

		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<Player>().TakeDamage(damage);
		}
	}

	public void ActivateBoss()
	{
		if (!isActive)
		{
			StartCoroutine(BossEnterCoroutine());
		}
	}

	private IEnumerator BossEnterCoroutine()
	{
		Debug.Log("Active Boss");
		float step = entrySpeed * Time.deltaTime;
		while (Vector2.Distance(transform.position, onscreenPosition.position) > 0.1f)
		{
			transform.position = Vector2.MoveTowards(transform.position, onscreenPosition.position, step);
			yield return new WaitForSeconds(5f);
		}

		transform.position = onscreenPosition.position;
		healthSlider.gameObject.SetActive(true);
		isActive = true;
	}

	private IEnumerator SpawnDestroyedParticles()
	{
		for (int i = 0; i < 5; i++)
		{
			float randInterval = Random.Range(0.2f, 0.3f);
			yield return new WaitForSeconds(randInterval);
			Vector3 randomOffset = new Vector3(
				Random.Range(-1f, 1f),
				Random.Range(-1f, 1f),
				Random.Range(-1f, 1f)
			);
			destroyedParticleInstance = Instantiate(destroyedParticle, transform.position + randomOffset, Quaternion.identity);
		}

		healthSlider.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}
}
