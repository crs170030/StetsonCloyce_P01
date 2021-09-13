using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    [SerializeField] int _maxHealth = 3;
    //[SerializeField] Text _treasureUI = null;
    [SerializeField] ParticleSystem _deathParticles = null;
    [SerializeField] AudioClip _deathSound = null;
    /*
    //bad solution: change every tank piece indivdually. Better to change the body material itself in real time?
    [SerializeField] GameObject turret = null;
    [SerializeField] GameObject body = null;
    [SerializeField] GameObject left_tred = null;
    [SerializeField] GameObject right_tred = null;

    [SerializeField] Material defaultPaint = null;
    [SerializeField] Material defaultTires = null;
    */

    int _currentHealth;
    //int _treasureCount;
    public bool damagable = true;

    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    void Start()
    {
        _currentHealth = _maxHealth; //reset health at start
        //_treasureCount = 0;
    }

    // Update is called once per frame
    
    /*
    public void ChangeColor(bool reset, Material glowUp)
    {
        if (reset)
        {
            turret.GetComponent<MeshRenderer>().material = defaultPaint;
            body.GetComponent<MeshRenderer>().material = defaultPaint;
            left_tred.GetComponent<MeshRenderer>().material = defaultTires;
            right_tred.GetComponent<MeshRenderer>().material = defaultTires;
        }
        else
        {
            turret.GetComponent<MeshRenderer>().material = glowUp;
            body.GetComponent<MeshRenderer>().material = glowUp;
            left_tred.GetComponent<MeshRenderer>().material = glowUp;
            right_tred.GetComponent<MeshRenderer>().material = glowUp;
        }
    }*/
    /*
    public void IncreaseTreasure(int amount)
    {
        _treasureCount += amount;
        _treasureUI.text = "Treasure = " + _treasureCount;
        Debug.Log("Treasure = " + _treasureCount);
    }*/

    public void ChangeSpeed(float amount)
    {
        _tankController.MaxSpeed += amount;
        Debug.Log("MaxSpeed is now " + _tankController.MaxSpeed);
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        if(damagable) //only decrease health if it is not invincible
            _currentHealth -= amount;

        Debug.Log("Player's health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (damagable) //if tank bleeds, kill it
        {
            gameObject.SetActive(false);
            //play particles, man
            _deathParticles = Instantiate(_deathParticles, transform.position, Quaternion.identity);
            //play sounds
            AudioHelper.PlayClip2D(_deathSound, 1f);
        }
    }
}
