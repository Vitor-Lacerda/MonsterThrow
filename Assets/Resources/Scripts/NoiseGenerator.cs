using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour {

	public static NoiseGenerator instance;

	public int pixWidth;
	public int pixHeight;
	public float xOrg;
	public float yOrg;
	public float scale = 1.0F;
	public float changeRate = 1f;
	private float timer = 0f;
	private Texture2D noiseTex;
	private Color[] pix;
	private float redFactor, greenFactor;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		noiseTex = new Texture2D(pixWidth, pixHeight);
		noiseTex.wrapMode = TextureWrapMode.Repeat;
		pix = new Color[noiseTex.width * noiseTex.height];
		CalcNoise ();
	}

	void CalcNoise() {
		float y = 0.0F;
		while (y < noiseTex.height) {
			float x = 0.0F;
			while (x < noiseTex.width) {
				float xCoord = xOrg + x / noiseTex.width * scale;
				float yCoord = yOrg + y / noiseTex.height * scale;
				float sample = Mathf.PerlinNoise(xCoord, yCoord);
				redFactor = Random.Range (0f, 1f);
				greenFactor = Random.Range (0f, 1f);
				pix[(int)(y * noiseTex.width + x)] = new Color(sample*redFactor, sample*greenFactor, 0);
				x++;
			}
			y++;
		}
		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}
		
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= changeRate) {
			CalcNoise ();
			timer = 0;
		}
	}

	public Texture2D GetTex(){
		return noiseTex;
	}
}
