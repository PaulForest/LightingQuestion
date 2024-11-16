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
            // var v1 = childDot003.transform.position - childDot002.transform.position;
            // var v2 = childDot001.transform.position - childDot002.transform.position;
            //
            // var cross = Vector3.Cross(v2, v1).normalized;
            //
            // var dot = Vector3.Dot(cross, sunlight.transform.forward);
            // var angle = Vector3.Angle(cross, sunlight.transform.forward);
            //
            // var lightIntensity = dot;
            // lightMeUp.SetIntensity(Mathf.Clamp01(lightIntensity));

            lightMeUp.SetIntensity(GetIntensityOfLightOnSurface(childDot001.transform.position,
                childDot002.transform.position, childDot003.transform.position, sunlight.transform.forward));
        }

        /// <summary>
        /// Finds the intensity of a light hitting a surface, like sunlight hitting the surface of a solar panel.
        /// Here the surface of the solar panel is defined by the points (p1, p2, p3), and the direction of the light
        /// is defined as lightDirection.  We assume only one side of the solar panel can receive light; light hitting
        /// the opposite side will have zero intensity.
        /// The returned intensity is in the range [0, 1] and represents the relative angle of the light to the surface
        /// of the "top" of the solar panel.  
        /// </summary>
        /// <param name="p1">a point on the surface</param>
        /// <param name="p2">a point on the surface</param>
        /// <param name="p3">a point on the surface</param>
        /// <param name="lightDirection">a vector representing the angle the light is aiming</param>
        /// <returns></returns>
        public float GetIntensityOfLightOnSurface(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 lightDirection)
        {
            var v1 = p3 - p2;
            var v2 = p1 - p2;

            var normal = Vector3.Cross(v2, v1).normalized;

            var dot = Vector3.Dot(normal, lightDirection);
            var angle = Vector3.Angle(normal, lightDirection);

            var lightIntensity = 0f;

            if (angle > 90f)
            {
                lightIntensity = (angle - 90f) / 90f;
            }

            Debug.DrawLine(p3, p2, Color.green);
            Debug.DrawLine(p1, p2, Color.blue);
            Debug.DrawLine(transform.position, transform.position + 10f * normal, Color.red);
            Debug.DrawLine(transform.position, transform.position + 10f * lightDirection, Color.yellow);

            Debug.Log(
                $"{transform.parent.name} {nameof(lightIntensity)}: {lightIntensity} {nameof(dot)}: {dot} {nameof(angle)}: {angle} {nameof(normal)}: {normal}");


            return lightIntensity;
        }
    }
}