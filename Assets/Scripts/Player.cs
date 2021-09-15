using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    //[SerializeField] Text _treasureUI = null;
   
    //int _treasureCount;
    //public bool damagable = true;

    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    void Start()
    {
        //_currentHealth = _maxHealth; //reset health at start
        //_treasureCount = 0;
    }
    /*
    public void IncreaseTreasure(int amount)
    {
        _treasureCount += amount;
        _treasureUI.text = "Treasure = " + _treasureCount;
        Debug.Log("Treasure = " + _treasureCount);
    }*/

    public void ChangeSpeed(float amount) //does nothing for now
    {
        _tankController.MaxSpeed += amount;
        Debug.Log("MaxSpeed is now " + _tankController.MaxSpeed);
    }
}
