using System;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wind
{
    public class MouseController : MonoBehaviour
    {
        private Camera mainCamera;
        private Collider2D[] arrClickCollider;
        private Vector2 mouseScreenPos;
        private Vector3 mouseWorldPos;

        [SerializeField] private MouseInfo mouseInfo;
        private Vector2 lastMouseScreenPos;

        private void Start()
        {
            mainCamera = Camera.main;
            arrClickCollider = new Collider2D[3];

            UpdateMousePos();
            lastMouseScreenPos = mouseScreenPos;
        }

        private void Update()
        {
            UpdateMousePos();
            GetColliderUnderMouse();
            UpdateInfo();
        }
        
        private void UpdateMousePos()
        {
            lastMouseScreenPos = mouseScreenPos;
            mouseScreenPos = Mouse.current.position.ReadValue();
            mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        }
        
        private void GetColliderUnderMouse()
        {
            StringBuilder sb = new StringBuilder("collider under mouse: ");
            int collideCount = 0;
            for (int i = 0; i < arrClickCollider.Length; i++)
            {
                arrClickCollider[i] = null;
            }
            Physics2D.OverlapPointNonAlloc(mouseWorldPos, arrClickCollider);
            foreach (var collider in arrClickCollider)
            {
                if (collider != null)
                {
                    collideCount++;
                    sb.Append($"{collider.name}, ");
                }
            }

            sb.Append(collideCount);
            // Debug.Log(sb.ToString());
        }

        private void UpdateInfo()
        {
            
            mouseInfo.tmpMouseMoveDelta.text = (mouseScreenPos - lastMouseScreenPos).ToString();
            // Debug.Log((mouseScreenPos - lastMouseScreenPos).ToString());
        }
    }
}