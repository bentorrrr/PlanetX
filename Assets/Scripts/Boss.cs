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

		isActive = false;
		healthSlider.gameObject.SetActive(false);
	}

    void Update()
    {
		if (isActive == false) return;

		if (health <= 50)
		{
			waveSpawner.spawnInterval = 3f;
			if (waveSpawner.bossFifty == false)
			{
				waveSpawner.spawnTimer = waveSpawner.spawnInterval;
			}
			waveSpawner.bossFifty = true;
		}

		if (health <= 25)
        {
            anim.SetTrigger("StageTwo");
            sp.color = Color.red;
            Phase1Engine.SetActive(false);
            Phase2Engine.SetActive(true);
        }

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            //healthSlider.gameObject.SetActive(false);
            //Destroy(this.gameObject);
			StartCoroutine(SpawnDestroyedParticles());
        }

        healthSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
		if (isActive == false) return;

		health -= damage;
        Debug.Log("Taking Damage" + health);
        if (health <= 0)
		{
			Player player = FindObjectOfType<Player>();
            player.IncrementDestroyedEnemyCount();
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

	private void OnTriggerEnter2D(Collider2D collision)
    {
		if (isActive == false || isDead) return;

		if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

	public void ActivateBoss()
	{
		if (isActive == false)
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
			yield return new WaitForSeconds(3f);
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
