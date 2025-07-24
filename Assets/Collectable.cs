using UnityEngine;

public class Collectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectItem();
            Destroy(gameObject);
        }
    }
}