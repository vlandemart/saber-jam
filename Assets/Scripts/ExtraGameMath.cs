using UnityEngine;

public class ExtraGameMath
{
    /// <summary>
    /// Returns velocity needed to be applied at rigidbody, in order for it to reach targetPos in parabolic manner
    /// </summary>
    /// <param name="startPos">Starting rigidbody position</param>
    /// <param name="targetPos">Desired destination position</param>
    /// <param name="angle">Angle at which object will fly</param>
    /// <returns></returns>
    public static Vector3 CalculateParabolicTrajectory(Vector3 startPos, Vector3 targetPos, float angle)
    {
        var distance = Vector3.Distance(startPos, targetPos);
        var gravity = Physics.gravity.y;
        var tanAlpha = Mathf.Tan(angle * Mathf.Deg2Rad);
        var height = startPos.y - targetPos.y;
        
        var Vz = Mathf.Sqrt(gravity * distance * distance / (2.0f * (height - distance * tanAlpha)));
        var Vy = tanAlpha * Vz;
        
        var velocity = new Vector3(0, Vy, Vz);
        return velocity;
    }
}