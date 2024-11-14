using UnityEngine;
using UnityEngine.Serialization;

namespace MyStuff
{
    public class LightMeUp : MonoBehaviour
    {
        [SerializeField] private Color lowestIntensityColor;
        [SerializeField] private Color highestIntensityColor;

        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private MaterialPropertyBlock _propertyBlock;
        private MeshRenderer _meshRenderer;

        private void OnEnable()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _propertyBlock = new MaterialPropertyBlock();
        }

        public void SetIntensity(float intensity)
        {
            _propertyBlock.SetColor(BaseColor, Color.Lerp(lowestIntensityColor, highestIntensityColor, intensity));
            _meshRenderer.SetPropertyBlock(_propertyBlock);
        }
    }
}