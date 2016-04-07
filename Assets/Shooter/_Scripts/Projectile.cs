using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    [SerializeField] private WeaponType _type;
    // This public property masks the field _type & takes action when it is set
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }
    private float Speed;
    private bool isReady;
    private Vector3 _direction;
    void Awake()
    {
        Speed = 20f;
        isReady = false;

        InvokeRepeating("CheckOffscreen", 2f, 2f);
    }

    public void SetDirection(Vector3 direction) {
        _direction = direction.normalized;
        isReady = true;

    }
    void Update() {
        if (isReady)
        {
            Vector3 position = transform.position;
            position += _direction * Speed * Time.deltaTime;
            transform.position = position;

        }
    }

    public void SetType(WeaponType eType)
    {
        // Set the _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        GetComponent<Renderer>().material.color = def.projectileColor;
    }
    void CheckOffscreen()
    {
        if (Utils.ScreenBoundsCheck(GetComponent<Renderer>().bounds, BoundsTest.offScreen) !=
        Vector3.zero)
        {
            Destroy(this.gameObject);
        }
    }

}
