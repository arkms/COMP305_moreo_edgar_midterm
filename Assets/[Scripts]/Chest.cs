using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] Sprite spriteOpen;
    SpriteRenderer draw;
    bool playerPressedE = false;
    bool playerInside = false;

    [Header("Starts")]
    [SerializeField] GameObject startPrefab;
    [SerializeField] float starRateGenerator;
    [SerializeField] float starInitForce;
    [SerializeField] float starLife;
    private float nextStar = 0f;

    void Start()
    {
        draw = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerPressedE)
        {
            nextStar -= Time.deltaTime;
            if (nextStar < 0f)
            {
                GameObject go = Instantiate(startPrefab, transform.position, Quaternion.identity);
                Vector2 pushDir = new Vector3(Random.Range(-0.3f, 0.3f), 1f);
                go.GetComponent<Rigidbody2D>().AddForce(pushDir * starInitForce, ForceMode2D.Impulse);
                Destroy(go, starLife);

                nextStar = starRateGenerator;
            }
        }
        else
        {
            if (playerInside && Input.GetKeyDown(KeyCode.E))
            {
                draw.sprite = spriteOpen;
                playerPressedE = true;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }

    }
}
