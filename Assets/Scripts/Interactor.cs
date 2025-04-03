using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 3f;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Check for F key press
        {
            Debug.Log("Interacting with something that is around you... i guess");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }

}
