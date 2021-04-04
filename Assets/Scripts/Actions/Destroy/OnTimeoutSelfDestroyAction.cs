using UnityEngine;

public class OnTimeoutSelfDestroyAction : MonoBehaviour
{

    [SerializeField] float timeout = 5f;

    private void Start()
    {
        Destroy(gameObject, timeout);
    }

}
