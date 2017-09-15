using UnityEngine;


[System.Serializable]
public class PlayerWeapon
{
    public string name = "Blaster";
    public int damage = 2;
    public float range = 100f;
    public float fireRate = 15;
    public int maxBullets = 50;
    [HideInInspector]
    public int bullets;
    
    public GameObject wpnGraphics;
    public float reloadTime = 1f;
    public float firingTime;

    public float GetFiringTime()
    {
        firingTime = fireRate / 60;
        return firingTime;
    } 

    public PlayerWeapon()
    {
        bullets = maxBullets;
    }

}
