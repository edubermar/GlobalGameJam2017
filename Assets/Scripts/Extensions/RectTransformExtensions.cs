#if (UNITY_5 || UNITY_4)
using System;
using UnityEngine;

namespace Extensions.UnityEngine
{
    public static class RectTransformExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public static void SetAnchorX(this RectTransform rt, float anchorX)
        {
            rt.anchorMin = new Vector2(anchorX, rt.anchorMin.y);
            rt.anchorMax = new Vector2(anchorX, rt.anchorMax.y);
        }
        
        public static void SetAnchorX(this RectTransform rt, float anchorMinX, float anchorMaxX)
        {
            rt.anchorMin = new Vector2(anchorMinX, rt.anchorMin.y);
            rt.anchorMax = new Vector2(anchorMaxX, rt.anchorMax.y);
        }
        
        public static void SetAnchorY(this RectTransform rt, float anchorY)
        {
            rt.anchorMin = new Vector2(rt.anchorMin.x, anchorY);
            rt.anchorMax = new Vector2(rt.anchorMax.x, anchorY);
        }
        
        public static void SetAnchorY(this RectTransform rt, float anchorMinY, float anchorMaxY)
        {
            rt.anchorMin = new Vector2(rt.anchorMin.x, anchorMinY);
            rt.anchorMax = new Vector2(rt.anchorMax.x, anchorMaxY);
        }
        
        public static void SetBottomMargin(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
        
        public static void SetLeftMargin(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }
        
        public static void SetRightMargin(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(right, rt.offsetMax.y);
        }
        
        public static void SetTopMargin(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, top);
        }
        
        public static void SetPosX(this RectTransform rt, float posX)
        {
            rt.anchoredPosition = new Vector2(posX, rt.anchoredPosition.y);
        }
        
        public static void SetPosY(this RectTransform rt, float posY)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, posY);
        }
    }
    
}
#endif