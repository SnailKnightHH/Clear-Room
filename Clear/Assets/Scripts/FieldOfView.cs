using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    Mesh mesh;
    [SerializeField] private LayerMask layerMask;
    private float startingAngle;
    Vector3 origin;
    float fov;
    float viewDistance;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Physics2D.queriesStartInColliders = false;
        origin = Vector3.zero;
        fov = 90f;
        viewDistance = 10f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mesh.RecalculateBounds();
        int rayCount = 50;
        float currentAngle = startingAngle;
        float angleIncrease = fov / rayCount;
        


        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; // rayCount does not count the initial ray, so 2 rays + 1 initial rays + origin
        Vector2[] UV = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1, triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {

            Vector3 currentVertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), viewDistance, layerMask);

            if (raycastHit2D.collider == null)
            {
                currentVertex = origin + GetVectorFromAngle(currentAngle) * viewDistance;
                Debug.Log("Arrived3");
            }
            else
            {
                currentVertex = raycastHit2D.point;
                Debug.Log("Arrived4");
                Debug.Log(raycastHit2D.collider.gameObject);
            }
            vertices[vertexIndex] = currentVertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            currentAngle -= angleIncrease;

        }

        mesh.vertices = vertices;
        mesh.uv = UV;
        mesh.triangles = triangles;
    }

    Vector3 GetVectorFromAngle(float angleInDegrees)
    {
        float angleRad = angleInDegrees * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void setOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void setAimDirection(float lookAngle)
    {
        startingAngle = lookAngle - fov / 2;
    }

    public void setFOV(float fov)
    {
        this.fov = fov;
    }

    public void setViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}
