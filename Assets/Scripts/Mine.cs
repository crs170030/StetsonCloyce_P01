using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] AudioClip _spawnSound = null;

    void Awake()
    {
        AudioHelper.PlayClip2D(_spawnSound, 1f);
    }
}
