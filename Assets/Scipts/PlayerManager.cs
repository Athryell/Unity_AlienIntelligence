using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject collectiblesPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            LevelManager.Instance.ResetLevel();
        }
        else if (other.gameObject.CompareTag("Collectibles"))
        {
            GameObject prefab = Instantiate(collectiblesPrefab, transform.position, Quaternion.identity);

            GameManager.instance.collectiblesGathered++;

            Destroy(prefab, 2f);
        }
    }
}
