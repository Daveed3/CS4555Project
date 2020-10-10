using UnityEngine;
using System.Collections;

public class CrossHairPosition : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var pos = Input.mousePosition;
        pos.z = 45;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Input.mousePosition;
        pos.z = 45;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = pos;
    }
}
