using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{

    // Kamera tarafından görüntülenmeyen öğeleri oyundan silen fonksiyon. Bu sayede sistem kaynağı tüketime minimuma indiriliyor.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
