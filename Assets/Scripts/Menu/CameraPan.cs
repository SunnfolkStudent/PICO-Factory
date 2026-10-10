using UnityEngine;

public class CameraBobbing : MonoBehaviour 
{ 
    // idk what these mean, tutorial said to do this. Change variables to change camera pan amount and speed :) VVV 
    [Header("Movement Settings")] 
    [SerializeField] private float MoveSpeed = 0.2f;    
    [SerializeField] private float MoveAmount = 0.2f; 

    private Vector3 startPosition; 
    private bool bobbingStarted = false;
    private Animator animator;
    private float bobbingStartTime;
    
    void Start() 
    { 
        // For LVL 1. When the starting animation finishes, the camera pan starts VVV 
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            StartBobbingImmediately();
        }
    } 

    void Update() 
    { 
        if (bobbingStarted)
        {
            float timeElapsed = Time.time - bobbingStartTime;
            float newY = startPosition.y + Mathf.Sin(timeElapsed * MoveSpeed) * MoveAmount; 
            
            transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z); 
            return;
        }

        if (animator != null)
        {
            if (!animator.isActiveAndEnabled || !animator.hasBoundPlayables)
            {
                StartBobbingImmediately();
                return;
            }

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.normalizedTime >= 1.0f)
            {
                startPosition = transform.localPosition;
                animator.enabled = false; 
                bobbingStartTime = Time.time;
                bobbingStarted = true;
            }
        }
    } 
    
    private void StartBobbingImmediately()
    {
        startPosition = transform.localPosition;
        bobbingStartTime = Time.time;
        bobbingStarted = true;
    }
}
