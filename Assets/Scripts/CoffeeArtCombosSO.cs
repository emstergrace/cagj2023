using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Coffee Art Choices")]
public class CoffeeArtCombosSO : ScriptableObject
{
	[SerializeField] private List<CoffeeArt> choices = new List<CoffeeArt>(); public List<CoffeeArt> Choices { get { return choices; } }
}

public enum CoffeeArt
{
	Bird,
	Sunflower,
	Dog,
	RainCloud,
	Panda,
	Bee,
	Flower,
	Anchor,
	Penguin,
	Pumpkin,
	Wolf,
	Heart,
	Butterfly,
	SadFace,
	Fish,
	None
}