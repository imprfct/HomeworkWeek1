using UnityEngine;

public class shootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;
    
    [SerializeField]
    private GameObject arrowSpawner;
    
    [SerializeField]
    private int speed = 20;
    
    public void SpawnArrow(GameObject player, Vector3 target)
    {
        var spawnerTransform = arrowSpawner.transform;
        player.transform.LookAt(target);
        var arrow = Instantiate(arrowPrefab, spawnerTransform.position,
            Quaternion.LookRotation(target.normalized));

        arrow.GetComponent<Rigidbody>().velocity = new Vector3(target.x,
            1, target.z).normalized * speed;
    }
}
