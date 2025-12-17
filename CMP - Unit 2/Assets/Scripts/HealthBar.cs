using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health Script")]
    public Health playerHealth;

    [Header("Health Bar UI")]
    public Image FullHealthBar;
    public Image CurrentHealthBar;

    private void Start()
    {
        FullHealthBar.fillAmount = playerHealth.currentHealth / 5;
    }

    private void Update()
    {
        CurrentHealthBar.fillAmount = playerHealth.currentHealth / 5;
    }
}
