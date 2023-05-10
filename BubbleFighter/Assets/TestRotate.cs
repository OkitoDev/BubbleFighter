using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;
using Utilities;

public class TestRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Services.GetServiceFromComponent<Player>().transform.position, Vector3.forward, 20 * Time.deltaTime);
    }
}
