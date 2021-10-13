using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
    
     //Bu class scriptin kullanıldığı öğenin istenilen bir diğer öğeyi yatay eksende istenilen mesafe ile takip etmesini sağlar. 
     


{
    public Transform simitTransform; //Scriptin konum belirlemek için istediği Transform öğesi.
    public float OffSet = 6f; //Spriptin kullanıldığı objenin istenilen cisimden uzaklığıı belirten değişken.
    

    void Update()
    {
        Vector3 CamPos = transform.position; //Scriptin kullanıldığı öğenin transformunu anlık olarak tutan değişken.

        CamPos.x = simitTransform.position.x; //Scriptin kullanıldığı öğenin x eksenini konum belirlemek için kullanılan öğenin x eksenine eşitleyen satır.
        CamPos.x += OffSet; // Scriptin kullanıldığı öğenin takip etmesin istenilen ögeden yatay eksendeki uzaklığını belirleyen satır.

        transform.position = CamPos; //Scrpitin kullanıldığı öğenin pozisyonunu takip etmesi istenilen ögenin anlık konumuna eşitleyen satır.

    }
}
