using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils
{
    // CREDITS: Morgoth - Stac Exchange: https://gamedev.stackexchange.com/questions/148229/get-the-length-of-an-animation-from-the-animator
    public static float GetAnimationLength(Animator animator, string clip)
    {
        return animator.runtimeAnimatorController.animationClips.First(a => a.name == clip).length;
    }

    // TODO: Add the GameObjects scene path to the string
    public static string GetSceneId(GameObject gameObject)
    {
        return SceneManager.GetActiveScene().name + gameObject.name + gameObject.transform.GetSiblingIndex();
    }

    public static void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public static void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
