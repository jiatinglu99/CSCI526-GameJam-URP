using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnDeathPlayer : MonoBehaviour
{
    // learned from https://www.youtube.com/watch?v=s_v9JnTDCCY
    // Start is called before the first frame update
    public float sphereSize = 10.0f;
    public int spheresInRow = 3; // radius
    public int spheresInHeight = 6;

    public Material sphereMaterial;

    private float spheresPivotDistance;
    private Vector3 spheresPivot;
    private bool alreadyShattered = false;

    void Start()
    {
        // find pivot distance
        spheresPivotDistance = sphereSize * spheresInRow / 2;
        // use this value to create pivot vector
        spheresPivot = new Vector3(spheresPivotDistance, spheresPivotDistance, spheresPivotDistance);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.name == "Player")
    //     {
    //         UnityEngine.Debug.Log("Player collided with " + gameObject.name);
    //         Shatter();
    //     }
    // }

    public void Shatter()
    {
        if (alreadyShattered)
        {
            return;
        }
        alreadyShattered = true;
        // make object disappear
        // gameObject.SetActive(false);
        transform.localScale = new Vector3(0, 0, 0);
        // lock location
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // disable flashlight
        GameObject temp = GameObject.Find("/Player/PointLight");
        if (temp != null)
        {
            temp.SetActive(false);
        }
        GameObject.Find("/Player/Flashlight").SetActive(false);

        // create all pieces
        for (int x = 0; x < spheresInRow; x++)
        {
            for (int y = 0; y < spheresInHeight; y++)
            {
                for (int z = 0; z < spheresInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }
    }
    void createPiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // set piece position and scale
        piece.transform.position = transform.position + new Vector3(sphereSize * x, sphereSize * y, sphereSize * z) - spheresPivot;
        // get a random number from 0f to 1f
        float rand = Random.Range(0f, 2f);
        piece.transform.localScale = new Vector3(sphereSize + rand, sphereSize + rand, sphereSize + rand);

        // add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = 0.25f;

        // set gravity
        piece.GetComponent<Rigidbody>().useGravity = true;

        // set piece material
        piece.GetComponent<Renderer>().material = sphereMaterial;
        
        // add point light to center
        // Light lightComp = piece.AddComponent<Light>();
        // lightComp.color = Color.white;
        // lightComp.intensity = 200.0f;
        // lightComp.range = 4.0f;
    }
}
