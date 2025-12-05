using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject Player;
    public Text healthText;
    public Text scoreText;

    
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.health.ToString();
        scoreText.text = "Score: " + player.score.ToString();
	}
}
