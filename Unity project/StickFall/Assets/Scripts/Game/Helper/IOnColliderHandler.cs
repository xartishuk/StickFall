using UnityEngine;

public interface IOnColliderHandler
{
    void OnChildCollisionEnter(Collision collision);
    void OnChildCollisionExit(Collision collision);


    void OnChildTriggerEnter(Collider other);
    void OnChildTriggerExit(Collider other);
}
