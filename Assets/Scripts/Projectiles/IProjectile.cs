using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile : IPoolable
{

    void SetPool(ObjectPool<IProjectile> pool);

    void Setup(Vector3 position, Quaternion rotation);
        
    Rigidbody GetRigidbody();

}
