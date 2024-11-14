using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OpacityController : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float opacity = 1f;

    private Material[] materials;

    private void Start()
    {
        // Get all materials from the Renderer component
        Renderer renderer = GetComponent<Renderer>();
        materials = renderer.materials;

        // Ensure the materials use a shader that supports transparency
        foreach (Material material in materials)
        {

            if (material.shader.name != "Standard" && material.shader.name != "Universal Render Pipeline/Lit")
            {
                Debug.LogWarning($"The material '{material.name}' does not support transparency. Use a shader that supports alpha blending.");
            }
            if (material != null)
            {
                SetMaterialToTransparent(material);
            }
        }
    }

    private void Update()
    {
        // Update the alpha value for each material's color
        foreach (Material material in materials)
        {
            if (material != null)
            {
                Color color = material.color;
                color.a = opacity;
                material.color = color;
            }
        }
    }

    private void SetMaterialToTransparent(Material material)
    {
        // Check if the material is already in Transparent mode
        if (material.GetFloat("_Mode") == 3f) return;

        // Switch the material to Transparent mode (Standard Shader)
        material.SetFloat("_Mode", 3f); // 3 = Transparent mode
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }
}
