using UnityEngine;

public abstract class AbstractLaunchAction : MonoBehaviour, ILaunchAction
{
    [SerializeField]
    protected float launchForce = 10f;

    public virtual bool CanLaunch()
    {
        return true;
    }

    public abstract void Launch(Rigidbody projectilePrefab);

}
