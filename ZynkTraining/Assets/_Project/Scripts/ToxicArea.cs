// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ToxicArea : MonoBehaviour
// {
//     private Coroutine damageCoroutine;
//     private Player player;

//     private void Start()
//     {
//         player = FindObjectOfType<Player>();
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             damageCoroutine = StartCoroutine(DamageOverTime());
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             StopCoroutine(damageCoroutine);
//         }
//     }

//     private IEnumerator DamageOverTime()
//     {
//         while (player.currentHealth > 0)
//         {
//             player.TakeDamage(1);
//             yield return new WaitForSeconds(2f);
//         }
//     }
// }





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicArea : MonoBehaviour
{
    
    public int damagePerTick = 1;
    public float tickTime = 2f;

    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = 0f;
            InvokeRepeating("DealDamage", 0f, tickTime);
        }
        Debug.Log("A intrat in toxic");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CancelInvoke("DealDamage");
        }
        Debug.Log("A iesit din toxic");
    }

    private void DealDamage()
    {
        timer += tickTime;

        if (timer >= tickTime)
        {
            Player.Instance.TakeDamage(damagePerTick);
            timer = 0f;
        }
    }
}




