using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    static public Player S;
    public float gameRestartDelay = 4f;
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [SerializeField] private float _shieldLevel = 1;
    public Weapon[] weapons;
    public bool __________;
    public Bounds bounds;

    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate field named fireDelegate.
    public WeaponFireDelegate fireDelegate;

    // Use this for initialization
   
    void Awake()
    {
        S = this;
        bounds = Utils.CombineBoundsOfChildren(this.gameObject);
    }
    void Start()
    {
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
    }
    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        bounds.center = transform.position;
        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.onScreen);
        if (off != Vector3.zero)
        {
            pos -= off;
            transform.position = pos;
        }
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Use the fireDelegate to fire Weapons
        // First, make sure the Axis("Jump") button is pressed
        // Then ensure that fireDelegate isn't null to avoid an error
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        { 
            fireDelegate();
        }
    }

    internal static void isEnabledAndactive()
    {
        throw new NotImplementedException();
    }

    public GameObject lastTriggerGo = null;
    void OnTriggerEnter(Collider other)
    {
        // Find the tag of other.gameObject or its parent GameObjects
        GameObject go = Utils.FindTaggedParent(other.gameObject);
        // If there is a parent with a tag
        if (go != null)
        {
            // Make sure it's not the same triggering go as last time
            if (go == lastTriggerGo)
            {
                return;
            }
            lastTriggerGo = go;
            if (go.tag == "Enemy")
            {
                // If the shield was triggered by an enemy
                // Decrease the level of the shield by 1
                shieldLevel--;
                // Destroy the enemy
                Destroy(go);
            }
            else if (go.tag == "PowerUp")
            {
                // If the shield was triggerd by a PowerUp
                AbsorbPowerUp(go);
            }
            else if (go.tag == "ProjectileEnemy") {
                shieldLevel--;
                Destroy(go);
            }
            else
            {
            }
        } else
        {
            // Otherwise announce the original other.gameObject
        }
   }
    
    public float shieldLevel
    {
        get
        {
            return (_shieldLevel); 
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4); 
            if (value < 0)
            { 
                Destroy(this.gameObject);
                Main.S.gameSounds[3].Play();
              //  Main.S.
                Main.S.DelayedRestart(gameRestartDelay); //DEAD

            }
        }
     
    }
    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield: // If it's the shield
                shieldLevel++;
                break;
            default: // If it's any Weapon PowerUp
                     // Check the current weapon type
                if (pu.type == weapons[0].type)
                {
                    // then increase the number of weapons of this type
                    Weapon w = GetEmptyWeaponSlot(); // Find an available weapon
                    if (w != null)
                    {
                        // Set it to pu.type
                        w.SetType(pu.type);
                    }
                }
                else
                {
                    // If this is a different weapon
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
        pu.AbsorbedBy(this.gameObject);
    }
    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }
    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}