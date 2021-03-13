using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BasePostEffect : MonoBehaviour
{
    public Material PostMaterial;
    public int BlurIter;
    public float BlurSpread;
    public int downScale = 2;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (PostMaterial != null)
        {
            int width = source.width, height = source.height;
            int nw = width / downScale, nh = height / downScale;
            RenderTexture buffer0 = RenderTexture.GetTemporary(nw, nh, 0);
            buffer0.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, buffer0);
            for (int i = 1; i <= BlurIter; i++)
            {
                PostMaterial.SetFloat("_BlurSize", 1 + i * BlurSpread);
                RenderTexture buffer1 = RenderTexture.GetTemporary(nw, nh, 0);
                Graphics.Blit(buffer0, buffer1, PostMaterial, 0);
                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;
                buffer1 = RenderTexture.GetTemporary(nw, nh, 0);
                Graphics.Blit(buffer0, buffer1, PostMaterial, 1);
                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;
            }
            Graphics.Blit(buffer0,destination);
            RenderTexture.ReleaseTemporary(buffer0);

        }
        else Graphics.Blit(source, destination);
    }
}
