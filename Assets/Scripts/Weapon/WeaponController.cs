using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon weapon;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rifle rifle;
    private Glock glock;
    private WeaponUI weaponUI;
    private Player player;

    void Start()
    {
        weapon.InitSetting();
        rifle = GetComponent<Rifle>();
        glock = GetComponent<Glock>();
        weaponUI = GetComponent<WeaponUI>();
        player = GetComponent<Player>();
        weapon.SetPlayer(player);

        if (rifle != null) rifle.InitSetting();
        if (glock != null) glock.InitSetting();

        weaponUI.InitializeUI();
        weaponUI.UpdateWeapon(weapon);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && rifle != null)
        {
            ChangeWeapon(rifle);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && glock != null)
        {
            ChangeWeapon(glock);
        }

        if (Input.GetKeyDown(KeyCode.R) && weapon.data.currentBullet < weapon.data.maxBullet && !weapon.data.isReloading)
        {
            StartCoroutine(weapon.Reload()); 
        }

        weapon.Shoot(bulletPrefab, firePoint);
    }

    private void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        Debug.Log($"{weapon.GetType().Name}");

        weaponUI.UpdateWeapon(weapon);

        if (player != null)
        {
            weapon.SetPlayer(player);
            player.SetWeapon(weapon);
            //moveMent2d.SetWeapon(weapon);
        }
    }
}
