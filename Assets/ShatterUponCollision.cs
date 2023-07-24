using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterUponCollision : MonoBehaviour
{
    // learned from https://www.youtube.com/watch?v=s_v9JnTDCCY
    // Start is called before the first frame update
    public float cubeSize = 5.0f;
    public int cubesInRow = 3;
    public int cubesInHeight = 14;

    public Material cubeMaterial;

    private float cubesPivotDistance;
    private Vector3 cubesPivot;

    void Start()
    {
        // find pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        // use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
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
        // make object disappear
        gameObject.SetActive(false);

        // create all pieces
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInHeight; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        // recreate the point light under goal
        Object.Instantiate(transform.Find("Point Light").gameObject);
    }
    void createPiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        // add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = 0.25f;

        // set gravity
        piece.GetComponent<Rigidbody>().useGravity = false;

        // set piece material
        piece.GetComponent<Renderer>().material = cubeMaterial;
        
        // add point light to center
        Light lightComp = piece.AddComponent<Light>();
        lightComp.color = Color.white;
        lightComp.intensity = 200.0f;
        lightComp.range = 10.0f;
    }
}
