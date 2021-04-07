using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float range;
    public int heading = 1;
    public float speed;
    private Vector3 origin;

    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speed * heading, 0);
        origin = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(heading == 1)
        {
            if(gameObject.transform.position.x >= origin.x + range)
            {
                Destroy(gameObject);
            }
        } else if(heading == -1)
        {
            if(gameObject.transform.position.x <= origin.x - range)
            {
                Destroy(gameObject);
            }
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
