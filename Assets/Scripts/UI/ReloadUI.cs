using UnityEngine;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    [SerializeField] private Slider reloadSlider;
    public static ReloadUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(instance.reloadSlider.value <= instance.reloadSlider.maxValue) instance.reloadSlider.value += Time.deltaTime;
    }

    // Cuando cambia de arma llamo a este
    public void SetMax(float max) {
        instance.reloadSlider.maxValue = max;
    }

    // Cada vez que dispara llamo a este
    public void ResetValue() {
        instance.reloadSlider.value = 0;
    }

}
