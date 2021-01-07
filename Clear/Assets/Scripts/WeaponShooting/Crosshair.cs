using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera cam;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = Input.mousePosition.y - 115;
        mousePos.x = Input.mousePosition.x + 10;
        transform.position = mousePos;
    }
}
