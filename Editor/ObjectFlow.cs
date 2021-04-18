using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ObjectFlow generates desired amount of objects in a location in the canvas and moves them to target location
/// with predefined settings. 
/// </summary>
public class ObjectFlow : MonoBehaviour
{
    [Header("Projectile Setup")]
    /// <summary>
    /// Base instance of the objects in the flow. 
    /// </summary>
    public GameObject Projectile;
    public int Amount;
    /// <summary>
    /// Minimum possible speed of an object that flows to the target. 
    /// </summary>
    public float MinSpeed;
    /// <summary>
    /// Maximum possible speed of an object that flows to the target. 
    /// </summary>
    public float MaxSpeed;

    [Header("Explosion Stage")]
    /// <summary>
    /// Scattering velocity of the objects. 
    /// </summary>
    public float ExplosionSpeed;

    /// <summary>
    /// The maximum possible distance between two objects.
    /// </summary>
    public float SpreadRadius;
    
    /// <summary>
    /// Changes how the objects spread along the X axis.
    /// </summary>
    public AnimationCurve ExplosionCurveX;
    /// <summary>
    /// Changes how the objects spread along the Y axis.
    /// </summary>
    public AnimationCurve ExplosionCurveY;

    [Header("Flowing Stage")]
    /// <summary>
    /// Target location that the objects are going to flow.
    /// </summary>
    public Transform Target;
    
    /// <summary>
    /// Changes how the objects flow along the X axis.
    /// </summary>
    public AnimationCurve FlowCurveX;

    /// <summary>
    /// Changes how the objects flow along the Y axis.
    /// </summary>
    public AnimationCurve FlowCurveY;
    
    /// <summary>
    /// Container of the currently flowing objects. 
    /// </summary>
    public List<FlowObject> flowObjects;

    /// <summary>
    /// Objects starts flowing to the target position when set to true
    /// </summary>
    bool flowing;

    /// <summary>
    /// Uses the existing projectiles to reduce the UI overhead
    /// </summary>
    ObjectPool<FlowObject> pool;

    public void Start()
    {
        pool = new ObjectPool<FlowObject>(CreateFlowObject);
    }

    /// <summary>
    ///  Helps projectile pool to create new projectiles
    /// </summary>
    /// <returns>A new projectile that has the same transform with the original one.</returns>
    public FlowObject CreateFlowObject()
    {
        var flowObject = Instantiate(Projectile, transform);
        return new FlowObject() { transform = flowObject.transform };
    }

    /// <summary>
    /// Moves the spawned projectiles towards the target
    /// </summary>
   public void MoveObjects()
    {   

        for (int i = 0; i < flowObjects.Count; i++)
        {
            // Move each object to the target with velocity calculated based on the animation curve values at the time.
            var flowObject = flowObjects[i];
            flowObject.startTime += Time.deltaTime / flowObject.speed;
            var passTime = Mathf.Clamp(flowObject.startTime, 0, 1);
            var posY = flowObject.startPosition.y + (flowObject.curveY.Evaluate(passTime) * flowObject.direction.y);
            var posX = flowObject.startPosition.x + (flowObject.curveX.Evaluate(passTime) * flowObject.direction.x);
            flowObject.transform.position = new Vector3(posX, posY, flowObject.transform.position.z);

            // Start flowing to the target if the particle is close enough to the expected explosion radius
            if (Vector3.Distance(flowObject.transform.position, flowObject.target) < 0.01f)
            {
                // Put the flow object to the pool if it reaches to the target.
                if (flowObject.target == Target.position)
                {
                    flowObjects.RemoveAt(i);
                    i--;
                    flowObject.SetActive(false);
                    pool.Return(flowObject);
                    continue;
                }

                var projectileSpeed = Random.Range(1/MinSpeed, 1/MaxSpeed);
                flowObject.Init(flowObject.transform.position, Target.position, projectileSpeed, FlowCurveX, FlowCurveY);
            }
        }
    }

    /// <summary>
    /// Removes all objects from flow and puts back to the object pool for later use.
    /// </summary>
    void Clear() {
        // Skip the first time use
        if(flowObjects == null)
            return;

        // Put all flowing objects back to object pool
        for (int i = 0; i < flowObjects.Count; i++)
        {
            var flowObject = flowObjects[i];
            flowObjects.RemoveAt(i);
            i--;
            flowObject.SetActive(false);
            pool.Return(flowObject);
        }
      
    }

    /// <summary>
    /// Spawns and spreads the speficied number of objects on the canvas and starts the flow.
    /// </summary>
    public void Flow()
    {
        Clear();
        flowObjects = new List<FlowObject>();
        for (int i = 0; i < Amount; i++)
        {
            var marginX = Random.Range(-SpreadRadius, SpreadRadius);
            var marginY = Random.Range(-SpreadRadius, SpreadRadius);
            var flowObject = pool.Get();
            flowObject.SetActive(true);

            flowObject.transform.position = transform.position;
            var target = new Vector3(transform.position.x + marginX, transform.position.y + marginY, transform.position.z);

            flowObject.Init(transform.position, target, 1 / ExplosionSpeed, ExplosionCurveX, ExplosionCurveY);
            flowObjects.Add(flowObject);
        }

        flowing = true;
    }

    void Update()
    {
        if (flowing)
        {
            MoveObjects();
        }
    }
}
