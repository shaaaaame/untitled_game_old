using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startpos;
    [SerializeField] private GameObject cam;
    [SerializeField][Range(0, 1)] private float parallax;

    // Start is called before the first frame update
    void Awake()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = cam.transform.position.x * parallax;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
