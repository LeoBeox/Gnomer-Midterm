using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI healthTxt;
   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHUD();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();
    }
    
    private void UpdateHUD()
    {
        if (scoreTxt != null)
        {
            scoreTxt.text = $"Score: {GameManager.Instance.Score}";
        }
        if (healthTxt != null)
        {
            healthTxt.text = $"Health: {GameManager.Instance.Health}";
        }
    }
}
