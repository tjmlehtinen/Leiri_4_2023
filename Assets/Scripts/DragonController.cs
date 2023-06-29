using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    private Transform throwPoint;
    public GameObject fireBall;
    private float timeBetweenThrows = 3f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        throwPoint = transform;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenThrows)
        {
            Instantiate(fireBall, throwPoint.position, throwPoint.rotation);
            timer = 0f;
        }
    }
}
