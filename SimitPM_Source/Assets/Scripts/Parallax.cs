using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    // Arka planımızın yenilenmesi için gereken tanımlamalar.
    private float groundLenght;
    private float startPos;
    public GameObject cam;
    public float parallaxSpeed = 0.04f;

    void Start()
    {
        startPos = transform.position.x;
        groundLenght = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    void Update()
    {
        
        // Kameranın konumunu alarak arkaplanı kamera dışına çıktığında yenileyen satırlar.
        float gecici = cam.transform.position.x;
        float mesafe = cam.transform.position.x * parallaxSpeed;
        transform.position = new Vector3(startPos + mesafe, transform.position.y, transform.position.z);
        if(gecici > startPos + groundLenght)
        {
            startPos += groundLenght;
        }
        else if(gecici < startPos - groundLenght)
        {
            startPos -= groundLenght;
        }
    }
}
