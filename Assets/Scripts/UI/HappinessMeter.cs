using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessMeter : MonoBehaviour
{
    public static HappinessMeter Inst { get; private set; }

    public int Value { get; private set; } = 0;
    [SerializeField] private int MaxHappiness = 10;
    [SerializeField] private Slider meter = null;
    [SerializeField] private Image sliderImage = null;

    [SerializeField] private Sprite sadCoffee = null;
    [SerializeField] private Sprite neutralCoffee = null;
    [SerializeField] private Sprite happyCoffee = null;

    public HappyState State { get {
            if (Value < MaxHappiness / 3) return HappyState.Sad;
            else if (Value >= MaxHappiness / 3 && Value <= MaxHappiness * 2 / 3) return HappyState.Neutral;
            else return HappyState.Happy;
		} }

	private void Awake() {
        Inst = this;
        meter.maxValue = MaxHappiness;
	}

    public void Increment(int val) {
        Value += val;
        if (Value > MaxHappiness) Value = MaxHappiness;
        else if (Value < 0) Value = 0;

        if (Value < MaxHappiness / 3) {
            sliderImage.sprite = sadCoffee;
		}
        else if (Value >= MaxHappiness / 3 && Value <= MaxHappiness * 2 / 3) {
            sliderImage.sprite = neutralCoffee;
		}
        else {
            sliderImage.sprite = happyCoffee;
		}

        meter.value = Value;
	}

}

public enum HappyState
{
    Happy,
    Neutral,
    Sad
}