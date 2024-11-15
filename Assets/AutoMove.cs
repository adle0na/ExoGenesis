using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 20f; // 이동 속도

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Z값이 -150에 도달하면 0으로 리셋
        if (transform.position.z >= 20f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -100f);
        }
    }
}
