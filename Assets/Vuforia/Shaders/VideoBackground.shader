//Copyright (c) 2014 Qualcomm Connected Experiences, Inc.
//All Rights Reserved.
//Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
// https://developer.vuforia.com/forum/unity-extension-technical-discussion/vuforia-5010-broke-ugui-mask-component
Shader "Custom/VideoBackground" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags {"Queue"="geometry-11" "RenderType"="opaque" }
        Pass {
            ZWrite Off
            Cull Off
            Lighting Off
			
			Stencil {
                Ref 2
                Comp Always
                Pass Replace
            }
            
            SetTexture [_MainTex] {
                combine texture 
            }
        }
    } 
    FallBack "Diffuse"
}
