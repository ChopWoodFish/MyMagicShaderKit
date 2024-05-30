using System;
using UnityEngine;
using UnityEngine.UI;

public class Hades_Transition : MonoBehaviour {

    [SerializeField] private Material material;

    [SerializeField] private Button btnFadeIn;
    [SerializeField] private Button btnFadeOut;

    private float maskAmount = 0f;
    private float targetValue = 1f;

    private void Start()
    {
        btnFadeIn.onClick.AddListener(() =>
        {
            targetValue = 1f;
        });
        
        btnFadeOut.onClick.AddListener(() =>
        {
            targetValue = -1f;
        });
    }
    
    private void Update() {
        // if (Input.GetKeyDown(KeyCode.T)) {
        //     targetValue = -.1f;
        // }
        // if (Input.GetKeyDown(KeyCode.Y)) {
        //     targetValue = 1f;
        // }
    
        float maskAmountChange = targetValue > maskAmount ? +.1f : -.1f;
        maskAmount += maskAmountChange * Time.deltaTime * 6f;
        maskAmount = Mathf.Clamp01(maskAmount);
    
        material.SetFloat("_MaskAmount", maskAmount);
    }


}