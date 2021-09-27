using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] HealthBase _hb = null;
    [SerializeField] float normalShakeAmount = 0.5f;
    float shakeAmount;
    Vector3 defaultPos;

    void Awake()
    {
        defaultPos = transform.position;
        if (_hb != null)
        {
            //subscribe to damage event
            _hb.Damaged += StartShaking;
        }
        else
        {
            Debug.Log("AAAH! HealthBase is Null!");
        }
    }

    void StartShaking(float damage)
    {
        shakeAmount = normalShakeAmount * damage * damage;
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        for (int i = 0; i < 6; i++)
        {
            transform.localPosition += Random.insideUnitSphere * shakeAmount;
            yield return new WaitForSeconds(.01f);
        }
        transform.position = defaultPos;
    }
}
