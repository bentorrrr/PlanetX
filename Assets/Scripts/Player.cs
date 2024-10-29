using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fireRate = 1f;
    public int health = 3;
    public float rotationSpeed = 5f;

	private float timer = 0;
    private float speedX, speedY;

    public Color flashColor;
    public Color regularColor;
	public float flashDuration = 0.1f;
    public int numberOfFlashes = 5;
	private bool isInvincible = false;

	private int killCount = 0;

	private Rigidbody2D rb;
    private SpriteRenderer sp;
    [SerializeField] private Transform bulletPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
	}

    void Update()
    {
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

    private void Fire()
    {
        GameObject bullet = ObjectPool.instance.GetPool();

        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
        }
    }

	public void TakeDamage(int damage)
	{
		if (isInvincible) return;

		health -= damage;
		if (health <= 0)
		{
			Debug.LogError("Player has been defeated.");
		}
		StartCoroutine(IFrames());
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
		if (killCount % 20 == 0)
		{
            Debug.LogWarning("Spawn PowerUP");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			TakeDamage(1);
		}
	}
}
