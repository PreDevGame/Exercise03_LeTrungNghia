using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrackWithMouse : MonoBehaviour
{
    public Camera _Camera;
    public Shader _DrawShader;
    [Range(1, 500)]
    public float _BrushSize;
    [Range(0, 1)]
    public float _BrushStrength;

    private RenderTexture _SplatMap;
    private Material _SnowMaterial, _DrawMaterial;
    private RaycastHit _hit;

    void Start()
    {
        _DrawMaterial = new Material(_DrawShader);
        _DrawMaterial.SetVector("_Color", Color.red);

        _SnowMaterial = GetComponent<MeshRenderer>().material;
        _SplatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _SnowMaterial.SetTexture("_Splat", _SplatMap);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_Camera.ScreenPointToRay(Input.mousePosition), out _hit))
            {
                _DrawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                _DrawMaterial.SetFloat("_Strength", _BrushStrength);
                _DrawMaterial.SetFloat("_Size", _BrushSize);
                RenderTexture temp = RenderTexture.GetTemporary(_SplatMap.width, _SplatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_SplatMap, temp);
                Graphics.Blit(temp, _SplatMap, _DrawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }

    //private void OnGUI()
    //{
    //    GUI.DrawTexture(new Rect(0, 0, 256, 256), _SplatMap, ScaleMode.ScaleToFit, false, 1);
    //}
}
