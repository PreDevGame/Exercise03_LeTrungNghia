using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelMarks : MonoBehaviour
{
    public Shader _DrawShader;
    public GameObject _Terrain;
    public Transform[] _Wheels;

    private RenderTexture _SplatMap;
    private Material _SnowMaterial, _DrawMaterial;

    [Range(0, 2)]
    public float _BrushSize;
    [Range(0, 1)]
    public float _BrushStrength;

    RaycastHit _GroundHit;
    int _PlayerMask;


    void Start()
    {
        _PlayerMask = LayerMask.GetMask("Ground");
        _DrawMaterial = new Material(_DrawShader);
        _SnowMaterial = _Terrain.GetComponent<MeshRenderer>().material;
        _SnowMaterial.SetTexture("_Splat", _SplatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
    }

    
    void Update()
    {
        for (int i = 0; i < _Wheels.Length; i++)
        {
            if (Physics.Raycast(_Wheels[i].position, -Vector3.up, out _GroundHit, 1f, _PlayerMask))
            {
                _DrawMaterial.SetVector("_Coordinate", new Vector4(_GroundHit.textureCoord.x, _GroundHit.textureCoord.y, 0, 0));
                _DrawMaterial.SetFloat("_Strength", _BrushStrength);
                _DrawMaterial.SetFloat("_Size", _BrushSize);
                RenderTexture _temp = RenderTexture.GetTemporary(_SplatMap.width, _SplatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_SplatMap, _temp);
                Graphics.Blit(_temp, _SplatMap, _DrawMaterial);
                RenderTexture.ReleaseTemporary(_temp);
            }
        }
    }
}
