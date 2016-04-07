using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    static public Enemy S;
    public float speed = 10f; 
    public float fireRate = 0.3f; 
    public float health = 10;
    public int score = 100;
    public int showDamageForFrames = 2; //# of framses to show damage
    public float powerUpDropChance = 1f; // chance to drop a power-up
    public bool ________________;
    public bool destroyed;
    public Color[] originalColors;
    public Material[] materials;// aall the Materials of this & its children
    public int remainingDamageFrames = 0; // damage frames left
    public Bounds bounds; 
    public Vector3 boundsCenterOffset;
    public int typeEnemy;
    public Weapon weapon;
    public bool weaponUser;
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate field named fireDelegate.
    public WeaponFireDelegate fireDelegate;


    void Awake()
    {
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
        InvokeRepeating("CheckOffscreen", 0f, 2f);
        destroyed = false;
        
        weapon.SetType(WeaponType.blaster);
        weaponUser = Main.S.SetEnemyWeapons(this); 
        TurnOffWeapons();
    }
    void TurnOffWeapons() {
        if ((PlayerPrefs.GetInt("Enemy 1Shoot") == 1) && typeEnemy == 0 && GameObject.Find("Game Level").GetComponent<Text>().text.Equals("Gold"))
        {
            TurnOnWeapon(true);
        }
        else if ((PlayerPrefs.GetInt("Enemy 2Shoot") == 1) && typeEnemy == 1 && GameObject.Find("Game Level").GetComponent<Text>().text.Equals("Gold"))
        {
            TurnOnWeapon(true);

        }
        else if ((PlayerPrefs.GetInt("Enemy 3Shoot") == 1) && typeEnemy == 2 && GameObject.Find("Game Level").GetComponent<Text>().text.Equals("Gold"))
        {
            TurnOnWeapon(true);

        }
        else if ((PlayerPrefs.GetInt("Enemy 4Shoot") == 1) && typeEnemy == 3 && GameObject.Find("Game Level").GetComponent<Text>().text.Equals("Gold"))
        {
            TurnOnWeapon(true);

        }
        else if ((PlayerPrefs.GetInt("Enemy 5Shoot") == 1) && typeEnemy == 4 && GameObject.Find("Game Level").GetComponent<Text>().text.Equals("Gold"))
        {
            TurnOnWeapon(true);

        }
        else
        {
            TurnOnWeapon(false);
        }

    }
    void TurnOnWeapon(bool on) {
        foreach (GameObject weaponPart in GameObject.FindGameObjectsWithTag("Enemy Weapons"))
        {
            weaponPart.SetActive(on);
        }
    }

    void Update()
    {
       
        Move();

        if (remainingDamageFrames > 0)
        {
            remainingDamageFrames--;
            if (remainingDamageFrames == 0)
            {
                UnShowDamage();
            }
        }
    }
    void Shoot() {
      //  weapon.FireEnemy();
        Invoke("Shoot", fireRate); // 3

    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
    void CheckOffscreen()
    {
        if (bounds.size == Vector3.zero)
        {
            bounds = Utils.CombineBoundsOfChildren(this.gameObject);
            boundsCenterOffset = bounds.center - transform.position;
        }
        bounds.center = transform.position + boundsCenterOffset;
        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen);
        if (off != Vector3.zero)
        {
            if (off.y < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject;
        switch (other.tag)
        {
            case "ProjectilePlayer":
                Projectile p = other.GetComponent<Projectile>();
                // Enemies don't take damage unless they're onscreen
                // This stops the player from shooting them before they are visible
                bounds.center = transform.position + boundsCenterOffset;
                if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds,
                BoundsTest.offScreen) != Vector3.zero)
                {
                    Destroy(other);
                    break;
                }
                // Hurt this Enemy
                ShowDamage();
                // Get the damage amount from the Projectile.type & Main.W_DEFS
                health -= Main.W_DEFS[p.type].damageOnHit;
                Debug.Log(health);
                if (health <= 0 && destroyed == false)
                {
                    destroyed = true;
                    Debug.Log("health<=0 TRUE");
                    // Tell the Main singleton that this ship has been destroyed
                    Main.S.ShipDestroyed(this);
                    // Destroy this Enemy
                    Debug.Log("destroyed");
                    Destroy(this.gameObject);
                }
                Destroy(other);
                break;
        }
    }
    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        remainingDamageFrames = showDamageForFrames;
    }
    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
    }
}
