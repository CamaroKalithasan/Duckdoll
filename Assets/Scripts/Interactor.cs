using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 3f;

    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Check for F key press
        {
            Collider[] colliders = Physics.OverlapSphere(interactionPoint.position, interactionRadius);

            foreach (Collider collider in colliders)
            {
                GameObject tree = collider.gameObject;
                TreeHealth treeHealth = tree.GetComponent<TreeHealth>();

                if (tree.CompareTag("Tree"))
                {
                    // Perform tree interaction
                    if (treeHealth != null)
                    {
                        //Item treeItem = new Item(tree.name, tree.GetComponent<SpriteRenderer>().sprite);
                        //inventorySystem.AddItem(new Item();
                        treeHealth.TakeDamage(20); // Damage amount can be adjusted as needed
                    }

                    Debug.Log("Interacting with tree: " + tree.name + " " + treeHealth);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
