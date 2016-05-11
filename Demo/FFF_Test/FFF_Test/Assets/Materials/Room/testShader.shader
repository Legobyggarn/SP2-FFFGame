Shader "Custom/testShader" {
	Properties {
		_Color ("Color", Color) = (0,1,0,1)
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
      	CGPROGRAM

      	#pragma surface surf Lambert

      	struct Input 
      	{
      	    float4 color : COLOR;
      	    float3 viewDir;

      	};

      	float4 _RimColor;
      	float _RimPower;

      	void surf (Input IN, inout SurfaceOutput o) 
      	{
       	  	o.Albedo = 1;
       	  	half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          	o.Emission = _RimColor.rgb * pow (rim, _RimPower);
      	}
      	ENDCG
    }
    Fallback "Diffuse"
}
