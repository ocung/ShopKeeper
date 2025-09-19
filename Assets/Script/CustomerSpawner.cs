using System;
using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public event Action OnCustomerSpawn;
    public event Action OnCustomerDespawn;
    // [SerializeField] private HagglingSystem hagglingSystem;

    [SerializeField] private float spawnCooldown = 5f;


    private void Start()
    {
        // hagglingSystem.OfferingEnded += OnHagglingEnd;
        OnHagglingEnd();
    }
    public void OnHagglingEnd()
    {
        DespawnCustomer();
        StartCoroutine(SpawnCustomerCoroutine());

    }

    IEnumerator SpawnCustomerCoroutine()
    {
        yield return new WaitForSeconds(spawnCooldown);
        SpawnCustomer();
    }

    private void SpawnCustomer()
    {
        Debug.Log("Spawning customer...");
        OnCustomerSpawn?.Invoke();
    }

    private void DespawnCustomer()
    {
        OnCustomerDespawn?.Invoke();
    }

    // void OnDestroy()
    // { 
    //     
    // }
}
