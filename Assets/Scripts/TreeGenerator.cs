using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject[] treePrefabs; // Array of tree prefabs
    
    [SerializeField] private int numberOfTrees = 50;
    [SerializeField] private int treeMaxHealth = 100; // Set the maximum health value for all trees
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;

    private void Start()
    {
        GenerateTrees();
    }

    private void GenerateTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            GameObject randomTreePrefab = GetRandomTreePrefab();
            Vector3 randomPosition = GetRandomPositionWithinSpawnArea();
            GameObject tree = Instantiate(randomTreePrefab, randomPosition, Quaternion.identity);
            TreeHealth treeHealth = tree.GetComponent<TreeHealth>();
            if (treeHealth != null)
            {
                treeHealth.maxHealth = treeMaxHealth; // Set the maxHealth to a 100
            }
        }
    }

    private GameObject GetRandomTreePrefab()
    {
        int randomIndex = Random.Range(0, treePrefabs.Length);
        return treePrefabs[randomIndex];
    }

    private Vector3 GetRandomPositionWithinSpawnArea()
    {
        Vector3 randomPosition = new Vector3
            (
            Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
            spawnAreaCenter.y,
            Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
            );

        return randomPosition;
    }
}
