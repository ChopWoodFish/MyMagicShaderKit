using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wind
{
    public class Grass : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        [SerializeField] private float spriteScreenWidth;
        
        private Material mat;
        private int externalForceID = Shader.PropertyToID("_ExternalForce");
        private float originalForce;
        [SerializeField] private float MaxForce = 5f;
        [SerializeField] private float MinForce = -5f;
        [SerializeField] private float DeltaMultiple = 0.2f;
        [SerializeField] private float RecoverForceDelta = 0.1f;

        private void Start()
        {
            mat = GetComponent<SpriteRenderer>().material;
            originalForce = mat.GetFloat(externalForceID);
            
            spriteScreenWidth = GetComponent<SpriteRenderer>().sprite.rect.width;
        }

        public void OnPointerEnter(PointerEventData e)
        {
            Debug.Log("Enter");
        }
        
        public void OnPointerExit(PointerEventData e)
        {
            Debug.Log("Exit");
        }

        public void OnPointerMove(PointerEventData e)
        {
            var delta = e.delta;
            float moveDeltaX = delta.x;
            float forceDeltaX = moveDeltaX * DeltaMultiple;
            float crtForce = mat.GetFloat(externalForceID);
            float newForce = Mathf.Clamp(crtForce + forceDeltaX, MinForce, MaxForce);
            mat.SetFloat(externalForceID, newForce);
            Debug.Log($"moveDeltaX: {moveDeltaX}, forceDeltaX: {forceDeltaX}, newForce: {newForce}");
        }

        private void Update()
        {
            float crtForce = mat.GetFloat(externalForceID);
            if (Mathf.Abs(crtForce - originalForce) > 0.01f)
            {
                float newForce = Mathf.Abs(crtForce - originalForce) < RecoverForceDelta ? originalForce
                    : crtForce > originalForce ? crtForce - RecoverForceDelta : crtForce + RecoverForceDelta;
                mat.SetFloat(externalForceID, newForce);
            }
        }
    }
}