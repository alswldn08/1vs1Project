using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [Header("WeaponUI")]
    public Text countRifle;
    public Text countGlock;
    private Rifle rifle;
    private Glock glock;

    void Start()
    {
        rifle = GetComponent<Rifle>();
        glock = GetComponent<Glock>();

        InitializeUI();
    }

    public void InitializeUI()
    {
        if (rifle != null && countRifle != null)
        {
            countRifle.text = rifle.data.currentBullet + "/" + rifle.data.maxBullet;
        }
        if (glock != null && countGlock != null)
        {
            countGlock.text = glock.data.currentBullet + "/" + glock.data.maxBullet;
        }
    }

    public void UpdateWeapon(Weapon newWeapon)
    {
        CountBullet(newWeapon);
    }

    public void CountBullet(Weapon weapon)
    {
        if (weapon is Rifle && countRifle != null)
        {
            countRifle.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
        }
        else if (weapon is Glock && countGlock != null)
        {
            countGlock.text = weapon.data.currentBullet + "/" + weapon.data.maxBullet;
        }
    }
}
