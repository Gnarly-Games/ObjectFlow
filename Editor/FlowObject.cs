using UnityEngine;

/// <summary>
/// FlowObject is the signle moving object in a flow.
/// </summary>
public class FlowObject
{   
        public Vector3 target;
        public Transform transform;
        public Vector3 startPosition;
        public Vector3 direction;
        public float startTime;
        public AnimationCurve curveX;
        public AnimationCurve curveY;
        public float speed;

        /// <summary>
        /// Init resets the values of the object with given parameters.
        /// </summary>
        /// <param name="curveX">Defines how the object moves along the X axis over time.</param>
        /// <param name="curveY">Defines how the object moves along the Y axis over time.</param>
        /// <param name="startPosition">Initial position of the object.</param>
        /// <param name="target">The position in the space that the object will be after the flow.</param>
        /// <param name="speed">Flowing speed of the individual object.</param>
        public void Init(Vector3 startPosition,  Vector3 target, float speed, AnimationCurve curveX, AnimationCurve curveY)
        {
            this.curveX = curveX;
            this.curveY = curveY;
            this.startTime = 0f;
            this.startPosition = startPosition;
            this.target = target;
            this.direction = this.target - this.transform.position;
            this.speed = speed;
        }
    
    /// <summary>
    /// Changes the state of the flow object to Activate or Deactive. 
    /// </summary>
    /// <param name="state"></param>
    public void SetActive(bool state)
    {
        transform.gameObject.SetActive(state);
    }
}
