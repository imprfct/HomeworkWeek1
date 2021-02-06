using UnityEngine;

public class HitManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var animator = other.gameObject.GetComponent<Animator>();
            animator.SetBool("isDead", true);
            other.gameObject.GetComponent<enemyController>().isDead = true;
            // destroy enemy
            Destroy(other.gameObject, 4.6f);
        }
        // destroy arrow
        Destroy(gameObject);
    }
}
