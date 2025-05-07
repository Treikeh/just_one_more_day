using System.Linq;
using UnityEngine;

public class Utils
{
    // CREDITS: Morgoth - Stac Exchange: https://gamedev.stackexchange.com/questions/148229/get-the-length-of-an-animation-from-the-animator
    public static float GetAnimationLength(Animator animator, string clip)
    {
        return animator.runtimeAnimatorController.animationClips.First(a => a.name == clip).length;
    }
}
