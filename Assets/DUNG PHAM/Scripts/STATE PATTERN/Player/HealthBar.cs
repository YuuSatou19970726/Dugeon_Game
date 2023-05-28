using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [Header("Slider Control")]
    public Slider slider;
    public Image fill;
    public Gradient gradient;
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    // // Quay Health Bar theo Camera (3D)
    // [Header("Face to Camera")]
    // public Transform tCamera;
    // void LateUpdate()
    // {
    //     transform.LookAt(transform.position + tCamera.position);
    // }
}
