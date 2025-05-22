using UnityEngine;

public class CharacterMovementAnimations : MonoBehaviour
{
    private enum CharacterFaceDir { FOLLOW_MOVEMENT, FRONT, BACK, LEFT, RIGHT, }


    [SerializeField] private Animator animator;
    [SerializeField] private CharacterFaceDir characterFaceDir = CharacterFaceDir.FOLLOW_MOVEMENT;

    private Vector3 prevPosition = Vector3.zero;


    private void FixedUpdate()
    {
        Vector2 moveDir = transform.position - prevPosition;
        if (moveDir.magnitude > 0.01f)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("x", moveDir.x);
            animator.SetFloat("y", moveDir.y);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        SetCharacterFaceDir();
        prevPosition = transform.position;
    }


    // Set the direction the character should be facing.
    // NOTE: Only works when characterFaceDir == FOLLOW_MOVEMENT
    private void SetCharacterFaceDir()
    {
        switch (characterFaceDir)
        {
            case CharacterFaceDir.FRONT:
                animator.SetFloat("x", 0f);
                animator.SetFloat("y", -1f);
                break;
            case CharacterFaceDir.BACK:
                animator.SetFloat("x", 0f);
                animator.SetFloat("y", 1f);
                break;
            case CharacterFaceDir.LEFT:
                animator.SetFloat("x", -1f);
                animator.SetFloat("y", 0f);
                break;
            case CharacterFaceDir.RIGHT:
                animator.SetFloat("x", 1f);
                animator.SetFloat("y", 0f);
                break;
        }
    }
}
