using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        //detect if it's the player
        Player _player = other.gameObject.GetComponent<Player>();

        //if valid:
        if (_player != null)
        {
            Debug.Log("Boss has been touched by :" + _player.name);

            //transform.LookAt(_player.transform);
            //rbBall.AddForce(transform.forward * -bounce);

            //Debug.Log("Enemy has been touched!");
            //levelController.Kill(false);
            //_player.TakeDamage(enemyDamage);
        }
    }
}
