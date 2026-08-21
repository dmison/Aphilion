using UnityEngine;

public static class VectorDrawing
{
    /// <summary>
    /// Generates line points for arc.
    /// </summary>
    /// <param name="position"> centre point of the arc</param>
    /// <param name="dir"> direction of the arc</param>
    /// <param name="anglesRange"> arc angle range</param>
    /// <param name="radius"> radius of the arc</param>
    /// <param name="numSegments"> number of segments in the circle</param>
    public static Vector3[] GenerateArcLinePoints(
        Vector3 position, 
        Vector3 dir, 
        float anglesRange, 
        float radius, 
        int numSegments)
    {
        Vector3[] linePointArray = new Vector3[numSegments + 1];
        float srcAngles = Vector3.Angle(position, dir);
        Vector3 initialPos = position;
        Vector3 posA = initialPos;

        float stepAngles = anglesRange / numSegments;
        float angle = srcAngles - anglesRange / 2;

        for (int i = 0; i <= numSegments; i++)
        {
            float rad = Mathf.Deg2Rad * angle;
            Vector3 posB = initialPos;
            posB += new Vector3(radius * Mathf.Cos(rad), 0, radius * Mathf.Sin(rad));
            angle += stepAngles;
            linePointArray[i] = posA;
            posA = posB;
        }

        return linePointArray;
    }
}