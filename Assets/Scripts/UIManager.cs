using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text speedText;
    public Text healthText;
    public Text fireRateText;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.health.ToString();
        speedText.text = "Speed: " + player.moveSpeed.ToString("F2");
        fireRateText.text = "Fire Rate: " + player.fireRate.ToString("F2");
    }
}
