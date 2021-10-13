using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayiSpawnerScript : MonoBehaviour
{
    public GameObject dayi; // Dayı engelimizin tanımlandığı satır.
    public GameObject marti; // Martı engelimizin tanımlandığı satır.
    private float y; // Martılarımızın spawnlanacakları yükseklikleri ayarlayan değişken.
    
    void Start()
    {
        
            StartCoroutine(spawnDayi()); // "spawnDayi" isimli IEnumarator fonksiyonumuzu çalıştırmaya başlayan satır.
            StartCoroutine(spawnMarti()); // "spawnMarti" isimli IEnumarator fonksiyonumuzu çalıştırmaya başlayan satır.


    }

    
    // Dayı engelimizi spawn etmemizi sağlayan fonksiyon.
    public IEnumerator spawnDayi()
    {
        
            while (true) // Fonksiyon sadece 1 kere çalışmaması için bir while(true) yapısı kullandık.
        {

                Instantiate(dayi, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity); // Dayımızın nerede konumlanacağına karar veren fonksiyon.
            yield return new WaitForSeconds(Random.Range(6, 8)); // Dayımızın kaçar saniye aralıklarla spawn olacağına karar veren satır.
            }

        }

    // Martı engelimizi spawn etmemizi sağlayan fonksiyon.
    public IEnumerator spawnMarti()
    {
        yield return new WaitForSeconds(20); // Martı engelimizi oyun başladıktan 22 saniye sonra gelmesini istediğimiz için fonksiyonunun 20 saniye beklemesini söyleyen satır.

        while (true) // Fonksiyon sadece 1 kere çalışmaması için bir while(true) yapısı kullandık.
        {
            
                int random = Random.Range(0, 2); // Martımızın spawn olacağı 2 yükseklikten birine karar veren satır.

                // Martı engelimizin spawn olacağı yükseklikleri ayarlayan if yapısı.
                if (random == 1)
                {
                    y = -2.8f;
                }
                else if (random == 0)
                {
                    y = -1f;
                }

                Instantiate(marti, new Vector3(transform.position.x, y, 1), Quaternion.identity); // Martı engelimizin nerede konumlanacağına karar veren fonksiyon.
                yield return new WaitForSeconds(Random.Range(5, 7)); // Martı engelimizin kaçar saniye aralıklarla spawn olacağına karar veren satır.
        }
            
            
        
    }
}
