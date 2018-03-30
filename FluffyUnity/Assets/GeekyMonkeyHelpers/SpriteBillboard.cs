using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour {

    public bool LockY = true;

    void Update()
    {
        if (LockY)
        {
            Vector3 v = Camera.main.transform.position - transform.position;
            v.y = v.z = 0.0f;
            transform.LookAt(Camera.main.transform.position - v);
        }
        else
        {
            transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
        transform.Rotate(0, 180, 0);

    }
}