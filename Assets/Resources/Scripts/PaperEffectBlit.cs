using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PaperEffectBlit : MonoBehaviour
{

	public Texture[] textures; 
	public float changeRate = 1f;
	public Material BlitMaterial;

	int currentText = 0;
	float timer = 0;

	void Start(){
		BlitMaterial.SetTexture ("Overlay Texture", textures [0]);
	}

	void Update(){
		timer += Time.deltaTime;
		if (changeRate > 0 && timer >= changeRate) {
			currentText++;
			currentText = currentText % textures.Length;
			BlitMaterial.SetTexture ("_OverTex", textures [currentText]);
			timer = 0;
		}
	}

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
       	if (BlitMaterial != null)
            Graphics.Blit(src, dst, BlitMaterial);
    }






}
