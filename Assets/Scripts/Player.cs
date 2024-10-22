using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fireRate = 1f;
    public int health = 3;

    private float timer = 0;
    private float speedX, speedY;
    
    private Rigidbody2D rb;
    [SerializeField] private Transform bulletPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        timer += Time.deltaTime;

        if (timer > fireRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speedX, speedY);
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
}
