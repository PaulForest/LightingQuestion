using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;

namespace MyStuff
{
    public class SunlightIntensity : MonoBehaviour
    {
        [SerializeField] private GameObject sunlight;
        [SerializeField] private LightMeUp lightMeUp;

        [SerializeField] private GameObject childDot001;
        [SerializeField] private GameObject childDot002;
        [SerializeField] private GameObject childDot003;

        private Collider _collider;

        private void OnEnable()
        {
            _collider = GetComponentInChildren<Collider>();

            if (!sunlight)
            {
                Debug.LogWarning($"Please set up {nameof(sunlight)} on {name} in the inspector.", this);
            }

            if (!lightMeUp)
            {
                Debug.LogWarning($"Please set up {nameof(lightMeUp)} on {name} in the inspector.", this);
            }
        }

        /*private void OnDrawGizmos()
        {
            // draw a normal to face
            Debug.DrawLine(transform.position, transform.position + transform.up * 10f, Color.red);

            if (sunlight)
            {
                // draw a normal to the directional light
                Debug.DrawLine(sunlight.transform.position,
                    sunlight.transform.position + sunlight.transform.forward * 10f,
                    Color.green);
            }
        }/**/

        private void Update()
        {
            // var center = _collider.bounds.center;
            // var min = _collider.bounds.min;
            // var max = _collider.bounds.max;
            //
            //
            // // get 3 points from the surface of this object.
            // var p1 = _collider.ClosestPointOnBounds(sunlight.transform.position);
            // var p2 = _collider.ClosestPointOnBounds(min);
            // var p3 = _collider.ClosestPointOnBounds(max);
            //
            // childDot001.transform.position = p1;
            // childDot002.transform.position = p2;
            // childDot003.transform.position = p3;


            // var p1 = _collider.ClosestPointOnBounds(new Vector3(-100, 100, 100));
            // var p2 = _collider.ClosestPointOnBounds(new Vector3(100, -100, 100));
            // var p3 = _collider.ClosestPointOnBounds(new Vector3(00, 00, 00));

            // childDot001.transform.position = p1;
            // childDot002.transform.position = p2;
            // childDot003.transform.position = p3;

            // Debug.DrawLine(p1 - Vector3.one, p1 + Vector3.one, Color.gray);
            // Debug.DrawLine(p1 + Vector3.one, p1 - Vector3.one, Color.gray);
            // Debug.DrawLine(p2 - Vector3.one, p2 + Vector3.one, Color.gray);
            // Debug.DrawLine(p2 + Vector3.one, p2 - Vector3.one, Color.gray);

            var v1 = childDot003.transform.position - childDot002.transform.position;
            var v2 = childDot001.transform.position - childDot002.transform.position;

            // var v1 = p3 - p1;
            // var v2 = p2 - p1;

            var cross = (-Vector3.Cross(v1, v2)).normalized;

            // var vToSunlight = sunlight.transform.position - transform.position;

            var dot = Vector3.Dot(cross, sunlight.transform.forward);
            var angle = Vector3.Angle(sunlight.transform.forward, cross);

            // var angle = Vector3.Angle(v2, v1);


            // var dotProduct = Vector3.Dot(sunlight.transform.forward, cross);
            var lightIntensity = -dot;
            // var lightIntensity = 1 - Mathf.Abs(dot);
            lightMeUp.SetIntensity(Mathf.Clamp01(lightIntensity));

            Debug.DrawLine(transform.position, transform.position + cross * 10f, Color.magenta);
            Debug.Log(
                $"{name} {nameof(lightIntensity)}: {lightIntensity} {nameof(dot)}: {dot} {nameof(angle)}: {angle} {nameof(cross)}: {cross}");


            return;
            /*

            // get 3 points from the surface of this object.
            var center = _collider.bounds.center;
            var min = _collider.bounds.min;
            var max = _collider.bounds.max;

            // get two vectors from the three points
            var centerToMin = min - center;
            var centerToMax = max - center;

            // find the normal vector to this object using the cross product
            var crossProduct= Vector3.Cross(centerToMax, centerToMin);
            Debug.DrawLine(center, center + crossProduct, Color.blue);


            // Compute the intensity of the sunlight's angle against the angle of this object.
            // Directly aiming the sunlight at this object should be maximum intensity, aiming to the side should be
            // mid-intensity, and aiming along the edge of the object should be zero.
            // Note that we assume only one side of this object can receive sunlight, so the intensity should be zero if
            // the sunlight is facing away from the object.
            var dotProduct = Vector3.Dot(sunlight.transform.forward, transform.forward);
            var lightIntensity = 1 - Mathf.Abs(dotProduct);
            lightMeUp.SetIntensity(lightIntensity);

            // Debug.Log($"{nameof(dotProduct)}: {dotProduct} {nameof(lightIntensity)}: {lightIntensity})");
            /**/
        }
    }
}