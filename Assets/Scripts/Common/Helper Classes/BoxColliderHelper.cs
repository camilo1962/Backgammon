using UnityEngine;

public class BoxColliderHelper : MonoBehaviour
{
    public static Vector3[] GetVertices(BoxCollider boxCollider)
    {
        Vector3[] vertices = new Vector3[8];

        vertices[0] = boxCollider.bounds.min;
        vertices[1] = boxCollider.bounds.max;
        vertices[2] = new Vector3(vertices[0].x, vertices[0].y, vertices[1].z);
        vertices[3] = new Vector3(vertices[0].x, vertices[1].y, vertices[0].z);
        vertices[4] = new Vector3(vertices[1].x, vertices[0].y, vertices[0].z);
        vertices[5] = new Vector3(vertices[0].x, vertices[1].y, vertices[1].z);
        vertices[6] = new Vector3(vertices[1].x, vertices[0].y, vertices[1].z);
        vertices[7] = new Vector3(vertices[1].x, vertices[1].y, vertices[0].z);

        return vertices;
    }
}
