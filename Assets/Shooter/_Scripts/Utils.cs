using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoundsTest
{
    center, // Is the center of the GameObject on screen?
    onScreen, //  bounds on screen?
    offScreen // bounds off screen?
}

public class Utils : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Creates bounds that encapsulate the two Bounds passed in.
    public static Bounds BoundsUnion(Bounds b0, Bounds b1)
    {
        if (b0.size == Vector3.zero && b1.size != Vector3.zero)
        { 
            return (b1);
        }
        else if (b0.size != Vector3.zero && b1.size == Vector3.zero)
        {
            return (b0);
        }
        else if (b0.size == Vector3.zero && b1.size == Vector3.zero)
        {
            return (b0);
        }
        b0.Encapsulate(b1.min); 
        b0.Encapsulate(b1.max);
        return (b0);
    }
    public static Bounds CombineBoundsOfChildren(GameObject go)
    {
        Bounds b = new Bounds(Vector3.zero, Vector3.zero);
        if (go.GetComponent<Renderer>() != null)
        {
            b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);
        }
        if (go.GetComponent<Renderer>() != null)
        {
            b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);
        }
        foreach (Transform t in go.transform)
        { 
            b = BoundsUnion(b, CombineBoundsOfChildren(t.gameObject)); 
        }
        return (b);
    }
    static public Bounds camBounds
    { 
        get
        {
            if (_camBounds.size == Vector3.zero)
            {
                SetCameraBounds();
            }
            return (_camBounds);
        }
    }
    static private Bounds _camBounds;
    public static void SetCameraBounds(Camera cam = null)
    {
        if (cam == null) cam = Camera.main;
        Vector3 topLeft = new Vector3(0, 0, 0);
        Vector3 bottomRight = new Vector3(Screen.width, Screen.height, 0);
        Vector3 boundTLN = cam.ScreenToWorldPoint(topLeft);
        Vector3 boundBRF = cam.ScreenToWorldPoint(bottomRight);
        boundTLN.z += cam.nearClipPlane;
        boundBRF.z += cam.farClipPlane;
        Vector3 center = (boundTLN + boundBRF) / 2f;
        _camBounds = new Bounds(center, Vector3.zero);
        _camBounds.Encapsulate(boundTLN);
        _camBounds.Encapsulate(boundBRF);
    }
    public static Vector3 ScreenBoundsCheck(Bounds bnd, BoundsTest test = BoundsTest.center)
    {
        return (BoundsInBoundsCheck(camBounds, bnd, test));
    }
    // Checks to see whether Bounds lilB are within Bounds bigB
    public static Vector3 BoundsInBoundsCheck(Bounds bigB, Bounds lilB, BoundsTest test
    = BoundsTest.onScreen)
    {
        Vector3 pos = lilB.center;
        Vector3 off = Vector3.zero;
        switch (test)
        {
            case BoundsTest.center:
                if (bigB.Contains(pos))
                {
                    return (Vector3.zero);
                }
                if (pos.x > bigB.max.x)
                {
                    off.x = pos.x - bigB.max.x;
                }
                else if (pos.x < bigB.min.x)
                {
                    off.x = pos.x - bigB.min.x;
                }
                if (pos.y > bigB.max.y)
                {
                    off.y = pos.y - bigB.max.y;
                }
                else if (pos.y < bigB.min.y)
                {
                    off.y = pos.y - bigB.min.y;
                }
                if (pos.z > bigB.max.z)
                {
                    off.z = pos.z - bigB.max.z;
                }
                else if (pos.z < bigB.min.z)
                {
                    off.z = pos.z - bigB.min.z;
                }
                return (off);
            case BoundsTest.onScreen:
                if (bigB.Contains(lilB.min) && bigB.Contains(lilB.max))
                {
                    return (Vector3.zero);
                }
                if (lilB.max.x > bigB.max.x)
                {
                    off.x = lilB.max.x - bigB.max.x;
                }
                else if (lilB.min.x < bigB.min.x)
                {
                    off.x = lilB.min.x - bigB.min.x;
                }
                if (lilB.max.y > bigB.max.y)
                {
                    off.y = lilB.max.y - bigB.max.y;
                }
                else if (lilB.min.y < bigB.min.y)
                {
                    off.y = lilB.min.y - bigB.min.y;
                }
                if (lilB.max.z > bigB.max.z)
                {
                    off.z = lilB.max.z - bigB.max.z;
                }
                else if (lilB.min.z < bigB.min.z)
                {
                    off.z = lilB.min.z - bigB.min.z;
                }
                return (off);
            case BoundsTest.offScreen:
                bool cMin = bigB.Contains(lilB.min);
                bool cMax = bigB.Contains(lilB.max);
                if (cMin || cMax)
                {
                    return (Vector3.zero);
                }
                if (lilB.min.x > bigB.max.x)
                {
                    off.x = lilB.min.x - bigB.max.x;
                }
                else if (lilB.max.x < bigB.min.x)
                {
                    off.x = lilB.max.x - bigB.min.x;
                }
                if (lilB.min.y > bigB.max.y)
                {
                    off.y = lilB.min.y - bigB.max.y;
                }
                else if (lilB.max.y < bigB.min.y)
                {
                    off.y = lilB.max.y - bigB.min.y;
                }
                if (lilB.min.z > bigB.max.z)
                {
                    off.z = lilB.min.z - bigB.max.z;
                }
                else if (lilB.max.z < bigB.min.z)
                {
                    off.z = lilB.max.z - bigB.min.z;
                }
                return (off);
        }
        return (Vector3.zero);
    }
    // This function will iteratively climb up the transform.parent tree
    // until it either finds a parent with a tag != "Untagged" or no parent
    public static GameObject FindTaggedParent(GameObject go)
    {
      // If this gameObject has a tag
        if (go.tag != "Untagged")
        { 
          // then return this gameObject
            return (go);
        }
        // If there is no parent of this Transform
        if (go.transform.parent == null)
        { 
          // We've reached the top of the hierarchy with no interesting tag
          // So return null
            return (null);
        }
        // Otherwise, recursively climb up the tree
        return (FindTaggedParent(go.transform.parent.gameObject)); 
    }
    // This version of the function handles things if a Transform is passed in
    public static GameObject FindTaggedParent(Transform t)
    { 
        return (FindTaggedParent(t.gameObject));
    }
    // Returns a list of all Materials on this GameObject or its children
    static public Material[] GetAllMaterials(GameObject go)
    {
        List<Material> mats = new List<Material>();
        if (go.GetComponent<Renderer>() != null)
        {
            mats.Add(go.GetComponent<Renderer>().material);
        }
        foreach (Transform t in go.transform)
        {
            mats.AddRange(GetAllMaterials(t.gameObject));
        }
        return (mats.ToArray());
    }
}
