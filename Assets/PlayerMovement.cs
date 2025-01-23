using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 worldPoint2D = new Vector2(mousePosition3D.x, mousePosition3D.y);


        transform.up = worldPoint2D;
        float verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * 100;
        transform.Translate(verticalInput * Vector2.up);
    }
}
