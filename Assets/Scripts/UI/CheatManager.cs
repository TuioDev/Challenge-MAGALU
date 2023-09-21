using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatManager : MonoBehaviour
{
    [Header("Cheat References")]
    [Header("Plojectile")]
    [SerializeField] private FloatVariable ProjectileDamage;
    [SerializeField] private Slider SliderProjectileDamage;
    [SerializeField] private TextMeshProUGUI TextProjectileDamage;
    [Space]
    [SerializeField] private FloatVariable ProjectileSpeed;
    [SerializeField] private Slider SliderProjectileSpeed;
    [SerializeField] private TextMeshProUGUI TextProjectileSpeed;
    [Header("Ant")]
    [SerializeField] private FloatVariable AntSpeed;
    [SerializeField] private Slider SliderAntSpeed;
    [SerializeField] private TextMeshProUGUI TextAntSpeed;
    [Space]
    [SerializeField] private FloatVariable AntMaxHealth;
    [SerializeField] private Slider SliderAntMaxHealth;
    [SerializeField] private TextMeshProUGUI TextAntMaxHealth;
    [Header("Cloud")]
    [SerializeField] private FloatVariable CloudSpeed;
    [SerializeField] private Slider SliderCloudSpeed;
    [SerializeField] private TextMeshProUGUI TextCloudSpeed;
    [Space]
    [SerializeField] private FloatVariable CloudSpeedOnWind;
    [SerializeField] private Slider SliderCloudSpeedOnWind;
    [SerializeField] private TextMeshProUGUI TextCloudSpeedOnWind;
    [Space]
    [SerializeField] private FloatVariable CloudMaxHealth;
    [SerializeField] private Slider SliderCloudMaxHealth;
    [SerializeField] private TextMeshProUGUI TextCloudMaxHealth;
    [Header("Spider")]
    [SerializeField] private FloatVariable SpiderSpeed;
    [SerializeField] private Slider SliderSpiderSpeed;
    [SerializeField] private TextMeshProUGUI TextSpiderSpeed;
    [Space]
    [SerializeField] private FloatVariable SpiderSpeedOnWind;
    [SerializeField] private Slider SliderSpiderSpeedOnWind;
    [SerializeField] private TextMeshProUGUI TextSpiderSpeedOnWind;
    [Space]
    [SerializeField] private FloatVariable SpiderMaxHealth;
    [SerializeField] private Slider SliderSpiderMaxHealth;
    [SerializeField] private TextMeshProUGUI TextSpiderMaxHealth;

    void Start()
    {
        SetTexts();
    }

    public void SetTexts()
    {
        // Projectile
        SliderProjectileDamage.value = ProjectileDamage.Value * 10;
        TextProjectileDamage.text = ProjectileDamage.Value.ToString("0.0");

        SliderProjectileSpeed.value = ProjectileSpeed.Value * 10;
        TextProjectileSpeed.text = ProjectileSpeed.Value.ToString("0.0");

        // Ant
        SliderAntSpeed.value = AntSpeed.Value * 10;
        TextAntSpeed.text = AntSpeed.Value.ToString("0.0");

        SliderAntMaxHealth.value = AntMaxHealth.Value * 10;
        TextAntMaxHealth.text = AntMaxHealth.Value.ToString("0.0");

        // Cloud
        SliderCloudSpeed.value = CloudSpeed.Value * 10;
        TextCloudSpeed.text = CloudSpeed.Value.ToString("0.0");

        SliderCloudSpeedOnWind.value = CloudSpeedOnWind.Value * 10;
        TextCloudSpeedOnWind.text = CloudSpeedOnWind.Value.ToString("0.0");

        SliderCloudMaxHealth.value = CloudMaxHealth.Value * 10;
        TextCloudMaxHealth.text = CloudMaxHealth.Value.ToString("0.0");

        // Spider
        SliderSpiderSpeed.value = SpiderSpeed.Value * 10;
        TextSpiderSpeed.text = SpiderSpeed.Value.ToString("0.0");

        SliderSpiderSpeedOnWind.value = SpiderSpeedOnWind.Value * 10;
        TextSpiderSpeedOnWind.text = SpiderSpeedOnWind.Value.ToString("0.0");

        SliderSpiderMaxHealth.value = SpiderMaxHealth.Value * 10;
        TextSpiderMaxHealth.text = SpiderMaxHealth.Value.ToString("0.0");
    }

    public void ChangeProjectileDamage(float value)
    {
        value /= 10;
        ProjectileDamage.Value = value;
        TextProjectileDamage.text = value.ToString("0.0");
    }

    public void ChangeProjectileSpeed(float value)
    {
        value /= 10;
        ProjectileSpeed.Value = value;
        TextProjectileSpeed.text = value.ToString("0.0");
    }

    public void ChangeAntSpeed(float value)
    {
        value /= 10;
        AntSpeed.Value = value;
        TextAntSpeed.text = value.ToString("0.0");
    }

    public void ChangeAntMaxHealth(float value)
    {
        value /= 10;
        AntMaxHealth.Value = value;
        TextAntMaxHealth.text = value.ToString("0.0");
    }

    public void ChangeCloudSpeed(float value)
    {
        value /= 10;
        CloudSpeed.Value = value;
        TextCloudSpeed.text = value.ToString("0.0");
    }

    public void ChangeCloudSpeedOnWind(float value)
    {
        value /= 10;
        CloudSpeedOnWind.Value = value;
        TextCloudSpeedOnWind.text = value.ToString("0.0");
    }

    public void ChangeCloudMaxHealth(float value)
    {
        value /= 10;
        CloudMaxHealth.Value = value;
        TextCloudMaxHealth.text = value.ToString("0.0");
    }

    public void ChangeSpiderSpeed(float value)
    {
        value /= 10;
        SpiderSpeed.Value = value;
        TextSpiderSpeed.text = value.ToString("0.0");
    }

    public void ChangeSpiderSpeedOnWind(float value)
    {
        value /= 10;
        SpiderSpeedOnWind.Value = value;
        TextSpiderSpeedOnWind.text = value.ToString("0.0");
    }

    public void ChangeSpiderMaxHealth(float value)
    {
        value /= 10;
        SpiderMaxHealth.Value = value;
        TextSpiderMaxHealth.text = value.ToString("0.0");
    }
}
