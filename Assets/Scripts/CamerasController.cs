using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameras[0].SetActive(Input.GetKey(KeyCode.Alpha1));
        cameras[1].SetActive(Input.GetKey(KeyCode.Alpha2));
    }
}
