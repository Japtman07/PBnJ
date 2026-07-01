using System.Collections;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(WaitAnimation());
    }
    
    IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("PlayAnim");
        yield return new WaitForSeconds(2f);
        animator.enabled = false;
    }
}
