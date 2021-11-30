using UnityEngine;
using UnityEngine.UI;

public class ReloadController : MonoBehaviour
{
    [SerializeField] private Slider reloadSlider;

    // Cuando cambia de arma llamo a este
    public void SetMax(float max) {
        reloadSlider.maxValue = max;
    }

    // Cada vez que dispara llamo a este
    public void ResetValue() {
        reloadSlider.value = 0;
    }

    void Update()
    {
        if(reloadSlider.value <= reloadSlider.maxValue) reloadSlider.value += Time.deltaTime;
    }
}
