using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeManager : MonoBehaviour
{
    public static CoffeeManager Inst { get; private set; }

	public List<PersonCoffeeChoice> CoffeeArtChoices = new List<PersonCoffeeChoice>();
	private Dictionary<string, PersonCoffeeChoice> coffeeDict = new Dictionary<string, PersonCoffeeChoice>(); public Dictionary<string, PersonCoffeeChoice> CoffeeDict { get { return coffeeDict; } }

	public Coffee CurrentCoffee { get; private set; } = null;

	public enum BrewType
	{
		Matcha,
		Cocoa,
		Coffee,
		Pumpkin,
		None
	}

	private void Awake() {
        Inst = this;

		for (int i = 0; i < CoffeeArtChoices.Count; i++) {
			coffeeDict.Add(CoffeeArtChoices[i].name, CoffeeArtChoices[i]);
		}
	}

	public void CreateCoffee(string customer, BrewType brew, CoffeeArt art) {
		CurrentCoffee = new Coffee(customer, brew, art);
	}

}

[System.Serializable]
public class Coffee
{
	public string CustomerName;
	public CoffeeManager.BrewType Brew;
	public CoffeeArt Art; 

	public Coffee(string customer, CoffeeManager.BrewType brew, CoffeeArt art) {
		CustomerName = customer;
		Brew = brew;
		Art = art;
	}
}

[System.Serializable]
public class PersonCoffeeChoice
{
	public string name;
	public CoffeeArtCombosSO coffeeArtChoices;
}