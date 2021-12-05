using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] private GameObject ring1;
	[SerializeField] private GameObject ring2;
	[SerializeField] private GameObject ring3;

    void Update()
    {
        ring1.transform.Rotate (0,50*Time.deltaTime,0);
		//ring2.transform.Rotate (0,50*Time.deltaTime,0);
		//ring3.transform.Rotate (0,50*Time.deltaTime,0);
    }
}
