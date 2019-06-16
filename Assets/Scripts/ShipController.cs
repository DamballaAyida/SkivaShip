using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipController : MonoBehaviour
{
    public float maxSpeed = 10f;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    public GameObject gameController;
    public bool vivo;
    public GameObject enemyGenerator;
    
    // Start is called before the first frame update
    void Start()
    {
        vivo = true;

        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (vivo) { 
            float moveh = Input.GetAxis("Horizontal");
            rigidbody2D.velocity = new Vector2(moveh * maxSpeed, rigidbody2D.velocity.y);
            float movev = Input.GetAxis("Vertical");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, movev * maxSpeed);
        }
        else
        {
            rigidbody2D.gravityScale = 0.4f;
        }
    }
    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            vivo = false;
            enemyGenerator.SendMessage("CancelGenerator");
            UpdateState("Explosion");
            gameController.GetComponent<GameController>().gameState = GameController.GameState.Ended;
        }
        if (collision.tag == "Hundido")
        {
            vivo = false;
            enemyGenerator.SendMessage("CancelGenerator");
            //gameController.GetComponent<GameController>().gameState = GameController.GameState.Ended;
            gameController.GetComponent<GameController>().gameState = GameController.GameState.Hundido;
        }

    }
    public void Revive()
    {
        vivo = true;
    }
}
