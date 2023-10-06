using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesLibrary : MonoBehaviour
{
    public static ResourcesLibrary Inst { get; private set; } = null;

	[Header("Coffee Sprites")]
	[SerializeField] private Sprite Bird = null;
	[SerializeField] private Sprite Sunflower = null;
	[SerializeField] private Sprite Dog = null;
	[SerializeField] private Sprite RainCloud = null;
	[SerializeField] private Sprite Panda = null;
	[SerializeField] private Sprite Bee = null;
	[SerializeField] private Sprite Flower = null;
	[SerializeField] private Sprite Anchor = null;
	[SerializeField] private Sprite Penguin = null;
	[SerializeField] private Sprite Pumpkin = null;
	[SerializeField] private Sprite Wolf = null;
	[SerializeField] private Sprite Heart = null;
	[SerializeField] private Sprite Butterfly = null;
	[SerializeField] private Sprite SadFace = null;
	[SerializeField] private Sprite Fish = null;

	public Dictionary<CoffeeArt, Sprite> CoffeeArtDictionary { get; private set; } = new Dictionary<CoffeeArt, Sprite>();

	[Header("Customer Sprites")]
	[SerializeField] private Sprite ReginaldSprite = null;
	[SerializeField] private Sprite SaanviSprite = null;
	[SerializeField] private Sprite BrinleySprite = null;
	[SerializeField] private Sprite LazloSprite = null;
	[SerializeField] private Sprite SantiagoSprite = null;

	public Dictionary<string, Sprite> CustomerDictionary { get; private set; } = new Dictionary<string, Sprite>();

	[Header("UI")]
	[SerializeField] private Sprite squareBorder  = null; public Sprite SquareBorder { get { return squareBorder; } }
	[SerializeField] private Sprite squareBorderPressed = null; public Sprite SquareBorderPressed { get { return squareBorderPressed; } }
	[SerializeField] private Sprite circleBorder = null; public Sprite CircleBorder { get { return circleBorder; } }
	[SerializeField] private Sprite circleBorderPressed = null; public Sprite CircleBorderPressed { get { return circleBorderPressed; } }

	private void Awake() {
		if (Inst != null) Destroy(this.gameObject);
		Inst = this;
		DontDestroyOnLoad(this.gameObject);

		CoffeeArtDictionary.Add(CoffeeArt.Bird, Bird);
		CoffeeArtDictionary.Add(CoffeeArt.Sunflower, Sunflower);
		CoffeeArtDictionary.Add(CoffeeArt.Dog, Dog);
		CoffeeArtDictionary.Add(CoffeeArt.RainCloud, RainCloud);
		CoffeeArtDictionary.Add(CoffeeArt.Panda, Panda);
		CoffeeArtDictionary.Add(CoffeeArt.Bee, Bee);
		CoffeeArtDictionary.Add(CoffeeArt.Flower, Flower);
		CoffeeArtDictionary.Add(CoffeeArt.Anchor, Anchor);
		CoffeeArtDictionary.Add(CoffeeArt.Penguin, Penguin);
		CoffeeArtDictionary.Add(CoffeeArt.Pumpkin, Pumpkin);
		CoffeeArtDictionary.Add(CoffeeArt.Wolf, Wolf);
		CoffeeArtDictionary.Add(CoffeeArt.Heart, Heart);
		CoffeeArtDictionary.Add(CoffeeArt.Butterfly, Butterfly);
		CoffeeArtDictionary.Add(CoffeeArt.SadFace, SadFace);
		CoffeeArtDictionary.Add(CoffeeArt.Fish, Fish);

		CustomerDictionary.Add("Reginald", ReginaldSprite);
		CustomerDictionary.Add("Saanvi", SaanviSprite);
		CustomerDictionary.Add("Brinley", BrinleySprite);
		CustomerDictionary.Add("Lazlo", LazloSprite);
		CustomerDictionary.Add("Santiago", SantiagoSprite);
	}
}
