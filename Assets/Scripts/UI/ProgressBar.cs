using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar;

    public void SetAmount(float amount)
    {
        bar.fillAmount = amount;
    }
    
}
