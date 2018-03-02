using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseBlit : MonoBehaviour {

	public Material BlitMaterial;


	void Update(){
		BlitMaterial.SetTexture ("_NoiseTex", NoiseGenerator.instance.GetTex ());
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (BlitMaterial != null)
			Graphics.Blit(src, dst, BlitMaterial);
	}
}
