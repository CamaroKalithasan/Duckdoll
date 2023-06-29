using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRange = 3f;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Check for F key press
        {
            // Perform raycast
            Ray ray = new Ray(interactionPoint.position, interactionPoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                GameObject tree = hit.collider.gameObject;
                if (tree.CompareTag("Tree"))
                {
                    // Perform tree interaction
                    Debug.Log("Interacting with tree: " + tree.name);
                    // Add your interaction logic here
                }
            }
        }
    }
}
