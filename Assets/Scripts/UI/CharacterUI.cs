using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private ProgressBar healthSlider;
    [SerializeField] private ProgressBar attackPrepareSlider;
    [SerializeField] private ProgressBar attackTimeSlider;

    [SerializeField] private CharacterSystem characterSystem;


    private void Start()
    {
        // Инициализируем UI
        UpdateHealthUI();
        ResetAttackSliders();
    }

    private void OnEnable()
    {
        if(!characterSystem) return;

        characterSystem.OnComonentsInitialized += SubscribeToComponentsEvents;

        SubscribeToComponentsEvents();
    }

    private void OnDisable()
    {
        if(!characterSystem) return;

        UnsubscribeToComponentsEvents();
    }

    private void UnsubscribeToComponentsEvents()
    {
        if(characterSystem.Health == null) return;
        
        characterSystem.Health.OnHealthChanged -= UpdateHealthUI;
        
        if(characterSystem.Combat == null) return;
        
        characterSystem.Combat.OnAttackPrepareProgress -= UpdateAttackPrepareUI;
        characterSystem.Combat.OnAttackTimeProgress -= UpdateAttackTimeUI;
    }
    private void SubscribeToComponentsEvents()
    {
        if(characterSystem.Health == null) return;
        
        characterSystem.Health.OnHealthChanged += UpdateHealthUI;
        
        if(characterSystem.Combat == null) return;
        
        characterSystem.Combat.OnAttackPrepareProgress += UpdateAttackPrepareUI;
        characterSystem.Combat.OnAttackTimeProgress += UpdateAttackTimeUI;
    }

    // Обновление UI здоровья
    private void UpdateHealthUI()
    {
        healthSlider.SetAmount(characterSystem.Health.CurrentHealth / characterSystem.Health.MaximumHealth);
    }

    // Обновление UI подготовки атаки
    private void UpdateAttackPrepareUI(float progress)
    {
        if (progress > 0)
        {
            if(!attackPrepareSlider.gameObject.activeInHierarchy) attackPrepareSlider.gameObject.SetActive(true);
            attackPrepareSlider.SetAmount(progress);
        }
        else
        {
            attackPrepareSlider.gameObject.SetActive(false);
        }
        
    }

    // Обновление UI времени атаки
    private void UpdateAttackTimeUI(float progress)
    {
        if (progress > 0)
        {
            if(!attackTimeSlider.gameObject.activeInHierarchy) attackTimeSlider.gameObject.SetActive(true);
            attackTimeSlider.SetAmount(progress);
        }
        else
        {
            attackTimeSlider.gameObject.SetActive(false);
        }
    }

    // Сброс слайдеров атаки
    private void ResetAttackSliders()
    {
        attackPrepareSlider.SetAmount(0);
        attackTimeSlider.SetAmount(0);
    }
}