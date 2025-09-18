using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float timer = 0;
    private int lastTime = 0;
    
    [SerializeField] private Transform transform;
    [SerializeField] private Tweener tweener;

    private const float moveSpeed = 0.5f; // In units per second
    private int cycleStage = 0;
    [SerializeField] private Animator animator;
    
    private Vector3 firstPos = new Vector3(7.3f, -1.73f, 0);
    private Vector3 secondPos = new Vector3(7.3f, 2.29f, 0);
    private Vector3 thirdPos = new Vector3(12.34f, 2.29f, 0);
    private Vector3 fourthPos = new Vector3(12.34f, -1.73f, 0);

    private void Start()
    {
        transform.position = firstPos;
        animator.SetInteger("Direction", cycleStage);
        CycleTween();
    }

    private void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        if (timer - lastTime >= 1 && !tweener.IsTweenActive())
        {
            UpdateCycleStage();
            CycleTween();
            lastTime++;
        }
    }

    private void CycleTween()
    {
        Vector3 currentPosition = transform.position;
        float duration = 2.0f;
        
        switch (cycleStage)
        {
            case 0:
                tweener.SetActiveTween(new Tween(transform, currentPosition, firstPos, Time.time, duration));
                break;
            case 1:
                tweener.SetActiveTween(new Tween(transform, currentPosition, secondPos, Time.time, duration));
                break;
            case 2:
                tweener.SetActiveTween(new Tween(transform, currentPosition, thirdPos, Time.time, duration));
                break;
            case 3:
                tweener.SetActiveTween(new Tween(transform, currentPosition, fourthPos, Time.time, duration));
                break;
        }
    }

    private void UpdateCycleStage()
    {
        animator.SetInteger("Direction", cycleStage);
        transform.localScale = new Vector3(1, 1, 1);

        if (cycleStage == 3)
        {
            // Flip sprite
            transform.localScale = new Vector3(-1, 1, 1);
            cycleStage = 0;
        } else
        {
            // Set sprite to face normally
            cycleStage += 1;
        }

    }
    
}
