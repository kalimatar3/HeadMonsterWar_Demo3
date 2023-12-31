  Shader "Character/CharShader-bumped" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _MaskTex ("MaskTex (RGBA)", 2D) = "black" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
      _EyeColor   ("Eye Color", Color) = (0.5,0.5,0.5,1)
      _SkinColor  ("Skin Color", Color) = (0.5,0.5,0.5,1)
      _HairColor  ("Hair Color", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf BlinnPhong nolightmap
         
      struct Input {
        float2 uv_MainTex;
        float2 uv_BumpMap;
      };
      
      sampler2D _MainTex;
      sampler2D _BumpMap;
      sampler2D _MaskTex;
      
      float4    _EyeColor;
      float4    _SkinColor;
      float4    _HairColor;
      
      void surf (Input IN, inout SurfaceOutput o) 
      {
      	float4 basecol = tex2D (_MainTex, IN.uv_MainTex);
      	float4 maskcol = tex2D (_MaskTex, IN.uv_MainTex);
      	float3 newcol;
      
      	if (maskcol.r > 0)
      	{
      		float3 graycol = dot(basecol.rgb,float3(0.3,0.59,0.11));
      		newcol = graycol * 2 * _EyeColor.rgb;
      		basecol.rgb = lerp(basecol,newcol,maskcol.r); 
      	}
      	
      	if (maskcol.g > 0)
      	{
      	    newcol = basecol * 2 * _SkinColor.rgb;
      		basecol.rgb = lerp(basecol,newcol,maskcol.g);
      	}
      	if (maskcol.b > 0)
      	{
      		newcol = basecol * 2 * _HairColor.rgb;
      		basecol.rgb = lerp(basecol,newcol,maskcol.b);
      	}
      	      	
        o.Albedo = basecol;
        o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
  		o.Gloss = basecol.a;
		o.Specular =  basecol.a;        
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }