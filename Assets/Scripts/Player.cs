using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject speedUpPrefab;
	public GameObject rateUpPrefab;

	public GameObject defaultBulletPrefab;
	public GameObject upgradedBulletPrefab;
    private GameObject shootingBullet;
    private GameObject defaultEngine;
    private GameObject speedEngine;

	public int powerUpTarget;

	public float moveSpeed = 5f;
    public float defaultFireRate = 0.3f;
    public float fireRate = 0.3f;
    public int health = 3;

	private float timer = 0;
    private float speedX, speedY;
	private bool canControl = false;

	public Color flashColor;
    public Color regularColor;
	public float flashDuration = 0.1f;
    public int numberOfFlashes = 5;
	private bool isInvincible = false;

	private int killCount = 0;

	private Collider2D col;
	private Rigidbody2D rb;
	private SpriteRenderer sp;
	[SerializeField] private WaveSpawner waveSpawner;
	[SerializeField] private Transform bulletPosition;

	[SerializeField] private ParticleSystem destroyedParticle;
	private ParticleSystem destroyedParticleInstance;

	public Transform offscreenStartPosition;
	public Transform onscreenPosition;
	public float entrySpeed = 5f;

	void Start()
    {
        fireRate = defaultFireRate;
        shootingBullet = defaultBulletPrefab;
		defaultEngine = transform.Find("DefaultEngine").gameObject;
		speedEngine = transform.Find("SpeedEngine").gameObject;
		transform.position = offscreenStartPosition.position;

		waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
		rb = GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
		col = GetComponent<Collider2D>();

		waveSpawner.gameObject.SetActive(false);
		StartCoroutine(EnterGameCoroutine());
	}

    void Update()
    {
		if (!canControl) return;

		speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(speedX, speedY).normalized;
        Move(movement);

        timer += Time.deltaTime;

        if (timer > fireRate)
        {
            Fire();
            timer = 0;
        }
    }

    public void SetFireRate(float rate)
    {
        StartCoroutine(FireRatePowerUp(rate));
    }

    public void SetSpeed(float rate)
    {
        StartCoroutine(SpeedPowerUp(rate));
    }

    private void Move(Vector2 movement)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        min.x = min.x + 0.75f;
        max.x = max.x - 0.75f;

        min.y = min.y + 0.75f;
        max.y = max.y - 0.75f;

        Vector2 pos = transform.position;
        pos += movement * moveSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
    
    private IEnumerator SpeedPowerUp(float speedMultiplier)
    {
        moveSpeed*=speedMultiplier;
        speedEngine.SetActive(true);
        defaultEngine.SetActive(false);
		yield return new WaitForSeconds(5);
		speedEngine.SetActive(false);
		defaultEngine.SetActive(true);
		moveSpeed /=speedMultiplier;
        Debug.Log("SpeedPowerUpEnd");
    }

    private IEnumerator FireRatePowerUp(float rate)
    {
        fireRate = defaultFireRate / rate;
        shootingBullet = upgradedBulletPrefab;
		yield return new WaitForSeconds(5);
		shootingBullet = defaultBulletPrefab;
		fireRate = defaultFireRate;
        Debug.Log("FireRatePowerUpEnd");
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(shootingBullet, bulletPosition.position, Quaternion.identity);
    }

	public void TakeDamage(int damage)
	{
		if (isInvincible) return;

		health -= damage;
		if (health <= 0)
		{
			canControl = false;
			defaultEngine.SetActive(false);
			sp.color = new Color(0, 0, 0, 0);
			col.enabled = false;
			StartCoroutine(SpawnDestroyedParticles());
			Debug.LogError("Player has been defeated.");
		}
		else
		{
			StartCoroutine(IFrames());
		}
	}

	private IEnumerator IFrames()
	{
        int flash = 0;
		isInvincible = true;
        while (flash < numberOfFlashes)
        {
            sp.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sp.color = regularColor;
			yield return new WaitForSeconds(flashDuration);
            flash++;
		}
		isInvincible = false;
	}
	public void IncrementDestroyedEnemyCount()
	{
		killCount++;
		if (killCount % powerUpTarget == 0)
		{
            int randPower = Random.Range(0, 2);
            float randYPos = Random.Range(-4.5f, 4.5f);
            Vector3 spawnPos = new Vector3(11, randYPos, 0);
            if (randPower == 0)
			{
				Debug.LogWarning("Spawn PowerUP");
				Instantiate(speedUpPrefab, spawnPos, Quaternion.identity);
			}
            else if (randPower == 1)
			{
				Debug.LogWarning("Spawn PowerUP");
				Instantiate(rateUpPrefab, spawnPos, Quaternion.identity);
			}
		}
	}
	private IEnumerator EnterGameCoroutine()
	{
		float step = entrySpeed * Time.deltaTime;
		while (Vector2.Distance(transform.position, onscreenPosition.position) > 0.1f)
		{
			transform.position = Vector2.MoveTowards(transform.position, onscreenPosition.position, step);
			yield return null;
		}
		transform.position = onscreenPosition.position;
		canControl = true;
		waveSpawner.gameObject.SetActive(true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			TakeDamage(1);
		}
	}

	private IEnumerator SpawnDestroyedParticles()
	{
        for (int i = 0; i < 20; i++)
        {
			float randInterval = Random.Range(0.05f, 0.25f);
			yield return new WaitForSeconds(randInterval);
			Vector3 randomOffset = new Vector3(
				Random.Range(-1f, 1f),
				Random.Range(-1f, 1f),
				Random.Range(-1f, 1f)
			);
			destroyedParticleInstance = Instantiate(destroyedParticle, transform.position + randomOffset, Quaternion.identity);
		}
	}
}
