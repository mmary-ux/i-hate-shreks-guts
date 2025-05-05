using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMana : MonoBehaviour
{
    public int maxMana = 100;
    public float currentMana;
    public Slider manaSlider;

    private void Start()
    {
        currentMana = 0;
        manaSlider.value = maxMana;
    }

    private void Update()
    {
        UpdateManaUI();
    }

    public void UseMana(int amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0);
    }

    public void UpdateManaUI()
    {
        if (manaSlider != null)
        {
            manaSlider.value = currentMana;
        }
    }
}