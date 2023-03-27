using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingInAndOutOfCar : MonoBehaviour
{
    [SerializeField] GameObject character = null;
    [SerializeField] KeyCode enterExitKey = KeyCode.E;
    [SerializeField] GameObject car = null;

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(enterExitKey))
        {
            GetOutOfCar();
        }
    }

    void GetOutOfCar()
    {
        character.SetActive(!character.activeSelf);
        character.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left);
    }
}
