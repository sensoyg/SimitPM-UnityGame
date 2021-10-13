using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    public GameObject[] powers; //Süper güçlerin tanımlandığı değişken.
    void Start()
    {
        StartCoroutine(spawn()); // "spawn" isimli IEnumarator fonksiyonumuzu çalıştırmaya başlayan satır.
    }

    // Özel güçlerimizi spawn etmemizi sağlayam fonksiyon.
    public IEnumerator spawn() 
    {
        yield return new WaitForSeconds(22); // Özel güçlerimizin oyun başladıktan 22 saniye sonra gelmesini istediğimiz için fonksiyonunun 22 saniye beklemesini söyleyen satır.
        while (true) { // Fonksiyon sadece 1 kere çalışmaması için bir while(true) yapısı kullandık.
            int rndm = Random.Range(0, 3); // Sciptimiz 3 özel güçten birini rastgele seçmesi için gereken satır.
            Instantiate(powers[rndm], new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity); //Özel güçlerimizin nerede konumlanacağına karar veren fonksiyon.
            yield return new WaitForSeconds(Random.Range(10, 30)); // Süper güçlerimizin kaçar saniye aralıklarla geleceğine karar veren satır.
        }
    }
    
    
}
