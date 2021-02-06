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
        var ray = new Ray(arrowSpawner.transform.position, target);
        if (Physics.Raycast(ray, out var hit))
        {
            // Позже реализовать плавное движение...
            
            /*Vector3 direction = target - player.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation,
                rotation, 5 * Time.fixedDeltaTime);*/
            
            player.transform.LookAt(target);
            var arrow = Instantiate(arrowPrefab, arrowSpawner.transform.position,
                player.transform.rotation);
            gameObject.transform.LookAt(target);
            arrow.transform.LookAt(target);
        
            arrow.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }
}
