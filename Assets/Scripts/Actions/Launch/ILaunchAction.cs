using UnityEngine;

public interface ILaunchAction
{

    bool CanLaunch();

    void Launch(Rigidbody projectilePrefab);

}
