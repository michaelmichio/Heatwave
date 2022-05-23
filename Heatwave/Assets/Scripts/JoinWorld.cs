using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinWorld : MonoBehaviour
{
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target.transform.position = new Vector3(50, 0, 50);
    }
}
