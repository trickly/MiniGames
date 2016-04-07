using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
    public float rotationsPerSecond = 0.1f;
    public bool ________________;
    public int levelShown = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int currLevel = Mathf.FloorToInt(Player.S.shieldLevel); 
                                                            
        if (levelShown != currLevel)
        {
            levelShown = currLevel;
            Material mat = this.GetComponent<Renderer>().material;
            // Adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0); 
        }
        // Rotate the shield a bit every second
        float rZ = (rotationsPerSecond * Time.time * 360) % 360f; 
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
