using UnityEngine;

namespace MyStuff
{
    public class RotateMe : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed;

        private void Update()
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}