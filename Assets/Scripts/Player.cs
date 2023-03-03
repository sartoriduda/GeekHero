using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float hp;
    public float maxHP = 100;
    public float moveSpeed;
    public Rigidbody2D rig2D;
    float moveX;
    float moveY;
    bool isMoving;
    [SerializeField] Animator anim;
    public Image heart;
    public int enemiesDefeat = 0;
    public GameObject[] enemiesScene;
    public bool catchFlower = false;
    public GameObject door;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        hp = maxHP;
        enemiesDefeat = 0;
        enemiesScene = GameObject.FindGameObjectsWithTag("Enemy");
        door.SetActive(false);
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        Move();
        Animation();
        Attack();
        UpdateUI();

        if(hp <= 0)
        {
            SceneManager.LoadScene("SceneGameOver");
        }

        if (enemiesDefeat >= enemiesScene.Length && catchFlower)
        {
            door.SetActive(true);
        } 
    }

    void Move()
    {
        rig2D.MovePosition(transform.position + new Vector3(moveX, moveY, 0) * Time.deltaTime * moveSpeed);
    }

    void Animation()
    {
        if (moveX == 0 && moveY == 0)
        {
            isMoving = false;
        } else {
            isMoving = true;
        }

        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("Horizontal", moveX);
        anim.SetFloat("Vertical", moveY);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isAttacking");
        }
    }

    void UpdateUI()
    {
        heart.fillAmount = hp/maxHP;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            SceneManager.LoadScene("SceneGameOver");
        }
        else if (collision.gameObject.tag == "Flower")
        {
            catchFlower = true;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Potion")
        {
            hp = maxHP;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Door")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
