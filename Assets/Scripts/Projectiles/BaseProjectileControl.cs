using System.Collections;
using UnityEngine;

public class BaseProjectileControl : MonoBehaviour, IProjectile
{

    [SerializeField] float timeout = 5f;

    private Rigidbody rb;
    private LayerBasedCollisionSensor layerBasedCollisionSensor;

    private ObjectPool<IProjectile> pool;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        layerBasedCollisionSensor = GetComponent<LayerBasedCollisionSensor>();
    }

    private void OnEnable()
    {
        layerBasedCollisionSensor.OnEnter += ReleaseFromCollision; // (collision) => ReleaseFromCollision(); ???
    }

    private void OnDisable()
    {
        layerBasedCollisionSensor.OnEnter -= ReleaseFromCollision; // (collision) => ReleaseFromCollision(); ???
    }

    /*
    // Esto parecía buena idea PERO NO!
    // Al meter el object en el pool llama a su SetActive(false) y eso desactiva este OnDisable, con lo que devuelve el gameObject al pool y ya la tenemos liada!!!
    private void OnDisable()
    {
        Debug.Log("OnDisable de " + gameObject);
        //Pool.Release(gameObject);
    }
    // Esto nos obliga a:
    // - Quitar las actions "OnCollisionSelfDestroyAction" y "OnTimeoutSelfDestroyAction"
    // - Y substituirlos por sensores "LayerBasedCollisionSensor" y (NUEVO) "TimeoutSensor" y SUBSCRIBIRNOS AQUÍ para tener más control!!!
    */

    private IEnumerator ReleaseTimeout()
    {
        yield return new WaitForSeconds(timeout);
        ReleaseFromCoroutine();
    }

    private void ReleaseFromCoroutine()
    {
Debug.Log($"---------------- {name}: ReleaseFromCoroutine.");
        Release();
    }

    private void ReleaseFromCollision(Collision collision)
    {
Debug.Log($"---------------- {name}: ReleaseFromCollision.");
        Release();
    }

    private void Release()
    {
        pool.Put(this);
    }

    #region IPoolable

    public void SetPool(ObjectPool<IProjectile> pool)
    {
        this.pool = pool;
    }

    public void OnPoolGet()
    {
Debug.Log($"Instance: {name} SALE del pool (count: {pool.Count}).");

        gameObject.SetActive(true);

        StartCoroutine(ReleaseTimeout());
    }

    public void OnPoolPut()
    {
Debug.Log($"Instance: {name} VUELVE al pool (count: {pool.Count}).");

        StopAllCoroutines();

        gameObject.SetActive(false);
    }

    #endregion

    #region IProjectile

    public void Setup(Vector3 position, Quaternion rotation)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = position;
        rb.rotation = rotation;
        transform.SetPositionAndRotation(position, rotation);
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    #endregion


}
