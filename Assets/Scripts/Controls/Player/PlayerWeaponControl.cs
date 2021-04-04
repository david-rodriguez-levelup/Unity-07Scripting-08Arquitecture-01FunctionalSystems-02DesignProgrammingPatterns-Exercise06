using UnityEngine;

[RequireComponent(typeof(ILaunchAction))]
public class PlayerWeaponControl : MonoBehaviour
{

    [SerializeField] private string fireButton = "Fire1";
    [SerializeField] protected Rigidbody projectilePrefab;
    [SerializeField] private ILaunchAction launchAction;
    
    private void Awake()
    {
        launchAction = GetComponent<ILaunchAction>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(fireButton))
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (launchAction.CanLaunch())
        {
            launchAction.Launch(projectilePrefab);
        } 
    }

}
