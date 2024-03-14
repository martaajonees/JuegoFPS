using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeDuration = 2f;
    float lifeTimer;

    public int ataque = 10;

    public bool shotByPlayer;

    private void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("Bullet choca con " + other.name);
        bool remove = true;
        IDamage damage = other.GetComponent<IDamage>();
        if(damage != null)
        {
           remove = damage.DoDamage(ataque, shotByPlayer);
        }
        if(remove == true) gameObject.SetActive(false);
    }
}
