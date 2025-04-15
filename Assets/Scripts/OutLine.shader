Shader "Custom/OutLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (0,0,0,1)
        _SurfaceColor ("Surface Color", Color) = (1,1,1,1)
        _LineWidth ("Line Width", Range(0, 0.1)) = 0.01
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        fixed4 _LineColor;
        fixed4 _SurfaceColor;
        float _LineWidth;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // ���� ���� uv���� �����´�. ( ���� ���� )
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            // left right�� ������ �ڵ��
            // 0 ~ 1 -> -0.5 ~ 0.5 -> abs�� ���� 0 ~ 0.5
            float2 uvDist = abs(IN.uv_MainTex - 0.5) * 2;

            // ��� ������ �� ������� üũ�Ѵ�.
            float maxDist = max(uvDist.x, uvDist.y);

            // 1 - �β��� �ϸ� �̰� 0.8 -> maxDist 0.7 -> 0��ȯ
            // 0.8 -> maxDist 0.9 -> 1��ȯ�ϴ� �Լ�
            float isBorder = step(1 - _LineWidth, maxDist);

            // 0 ~ 1���� ���� ��ȯ�Ѵ�.
            o.Albedo = lerp(_SurfaceColor.rgb, _LineColor.rgb, isBorder);
            o.Albedo = lerp(_SurfaceColor.rgb, _LineColor.rgb, isBorder);

            // �׳� ���� ������ �־��ִ°Ŵ�.
            o.Alpha = lerp(c.a, _LineColor.a, isBorder);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
