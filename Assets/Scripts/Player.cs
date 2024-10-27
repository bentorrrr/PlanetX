using System.Threading;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float defaultFireRate = 1f;
    public float fireRate = 1f;
    public int health = 3;

    private float timer = 0;
    private float speedX, speedY;
    
    private Rigidbody2D rb;
    [SerializeField] private Transform bulletPosition;

    void Start()
    {
        fireRate = defaultFireRate;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
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
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speedX, speedY).normalized * moveSpeed;
    }
    
    private IEnumerator SpeedPowerUp(float speedMultiplier)
    {
        moveSpeed*=speedMultiplier;
        yield return new WaitForSeconds(5);
        moveSpeed/=speedMultiplier;
        Debug.Log("SpeedPowerUpEnd");
    }

    private IEnumerator FireRatePowerUp(float rate)
    {
        fireRate = defaultFireRate / rate;
        yield return new WaitForSeconds(5);
        fireRate = defaultFireRate;
        Debug.Log("FireRatePowerUpEnd");
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
