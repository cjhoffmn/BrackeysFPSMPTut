using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFacingBillboard : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
    Camera m_Camera = Camera.main;
        if (m_Camera !=null )
        {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
        }

    }
}
