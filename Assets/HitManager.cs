using UnityEngine;

public class HitManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // destroy enemy
            Destroy(other.gameObject);
        }
        // destroy arrow
        Destroy(gameObject);
    }
}
