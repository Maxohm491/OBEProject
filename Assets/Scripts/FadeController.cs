using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FadeController : MonoBehaviour
{
    private Material[] materials;
    private bool isTransparent = false;

    private void Awake()
    {
        materials = GetComponent<Renderer>().materials;
    }

    public void SetOpacity(float opacity)
    {
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

    public void SetMaterialsToTransparent()
    {
        if (isTransparent) return;

        foreach (Material material in materials)
        {
            if (material != null)
            {
                SetMaterialToTransparent(material);
            }
        }

        isTransparent = true;
    }

    private void SetMaterialToTransparent(Material material)
    {
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
