using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class Raycaster : MonoBehaviour {

    public SpriteMask mask;
    public List<SpriteMask> spriteMasks = new List<SpriteMask>();

    public bool DrawDots;
    [SerializeField] Material mat;
    [SerializeField] List<Vector3> verticesVisual = new List<Vector3>();
    List<GameObject> meshGameObjects = new List<GameObject>();
    public int rayAmount;
    public int incrementAmount;
    public int previousIncrementAmount;
    private void Start() {
        previousIncrementAmount = incrementAmount;
        rayAmount = 360 / incrementAmount;
        CreateMeshesGameObjects();
        CreateSpriteMasks();
    }
    void CreateSpriteMasks() {
        for (int i = 0; i < rayAmount; i++) { 
            SpriteMask s_Mask = Instantiate(mask);
            s_Mask.transform.localScale = Vector3.one;
            spriteMasks.Add(s_Mask);

        }
    }
    void CreateMeshesGameObjects() {
        for (int i = 0; i < rayAmount; i++) {
            GameObject meshObject = new GameObject();
            meshObject.name = i.ToString();
            meshObject.AddComponent<MeshFilter>();
            meshObject.AddComponent<MeshRenderer>();
            /*
            meshObject.AddComponent<CreatedMeshCollisionCheck>();
            meshObject.AddComponent<MeshCollider>().convex = true;
            meshObject.GetComponent<MeshCollider>().isTrigger = true;
            */

            meshGameObjects.Add(meshObject);
            AddMeshesToObjects(meshObject);

        }
    }
    void AddMeshesToObjects(GameObject obj) {
        Mesh mesh = new Mesh();
        obj.GetComponent<MeshFilter>().mesh = mesh;
    }
    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Return)) {
            StartCoroutine(CalculateAverageFPS());
        }
        //spriteMasks.Clear();
        verticesVisual.Clear();


        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 worldPoint2D = new Vector2(mousePosition3D.x, mousePosition3D.y);

        //idea for revealing position
        //get size of object on an axis
        //set spritemask to that size
        //set spritemask forward direction to raycast direction


        for (int i = 0; i < 360; i += incrementAmount) {

            Vector3[] vertices = new Vector3[3];

            Vector3 dir = Quaternion.Euler(0, 0, i) * worldPoint2D;
            Vector3 dir2 = Quaternion.Euler(0, 0, i + incrementAmount) * worldPoint2D;
            RaycastHit2D hit = Physics2D.Raycast(worldPoint2D, dir, Mathf.Infinity);
            RaycastHit2D hit2 = Physics2D.Raycast(worldPoint2D, dir2, Mathf.Infinity);


            vertices[0] = worldPoint2D;
            vertices[1] = hit.point;
            vertices[2] = hit2.point;

            /*
            float objectHitSize = hit.transform.localScale.y;
            spriteMasks[i].transform.localScale = new Vector3(spriteMasks[i].transform.localScale.x, objectHitSize, spriteMasks[i].transform.localScale.z);
            spriteMasks[i].transform.up = dir;
            spriteMasks[i].transform.position = hit.point;
            */


            Mesh mesh = meshGameObjects[i].GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = new int[] { 2, 1, 0 };
            Color[] colors = new Color[3];
            colors[0] = Color.yellow;
            colors[1] = Color.yellow;
            colors[2] = Color.yellow;
            mesh.colors = colors;

            GameObject meshObject = meshGameObjects[i];

            mesh.RecalculateNormals();

            meshObject.GetComponent<MeshRenderer>().material = mat;
            meshObject.GetComponent<MeshFilter>().mesh = mesh;

            spriteMasks[i].transform.position = (hit.point);

            verticesVisual.Add(vertices[1]);
            verticesVisual.Add(vertices[2]);




        }



    }
    float averageFPS;
    float maxTime = 10;
    float elapsedTime;
    float totalFPS;
    int framesGone;
    IEnumerator CalculateAverageFPS() {
        elapsedTime = maxTime;
        while (elapsedTime > 0) {
            elapsedTime -= Time.deltaTime;
            float averageFPSForFrame = 1.0f / Time.deltaTime;
            totalFPS += averageFPSForFrame;
            Debug.Log(averageFPSForFrame);
            framesGone++;
            yield return null;
        }
        averageFPS = totalFPS / framesGone;
        Debug.Log("Average FPS Over 10 Seconds = " + averageFPS);
    }

    private void LateUpdate() {
        if (incrementAmount != previousIncrementAmount) {
            previousIncrementAmount = incrementAmount;
            meshGameObjects.Clear();
            spriteMasks.Clear();
            foreach (GameObject g in meshGameObjects) {
                Destroy(g);
            }
            foreach (SpriteMask s in spriteMasks) {
                Destroy(s);
            }
            rayAmount = 360 / incrementAmount;
            CreateMeshesGameObjects();
            CreateSpriteMasks();
        }
    }


    private void OnDrawGizmos() {
        if (DrawDots) {
            foreach (Vector3 pos in verticesVisual) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(pos, 0.1f);
            }
        }

    }

}
