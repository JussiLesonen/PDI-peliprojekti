using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7f && other.TryGetComponent<Player>(out var player))
        {
            player.Teleport(destination.position, destination.rotation);
            Debug.Log("tp");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, .4f);
        var direction = destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(destination.position, direction);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
