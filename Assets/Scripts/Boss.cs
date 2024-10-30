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
    public GameObject Phase1Engine;
    public GameObject Phase2Engine;
    private SpriteRenderer sp;
	[SerializeField] private ParticleSystem destroyedParticle;
	private ParticleSystem destroyedParticleInstance;

    private Animator anim;
    public Slider healthSlider;
    private GameObject player;
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
			StartCoroutine(SpawnDestroyedParticles());
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
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDead == false)
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
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
