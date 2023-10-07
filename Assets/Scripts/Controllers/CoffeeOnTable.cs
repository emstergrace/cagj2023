using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeOnTable : MonoBehaviour
{
    public static CoffeeOnTable Inst { get; private set; }

	[Header("Reginald Coffees")]
	public GameObject rCoffee;
	public GameObject rHotChocolate;
	public GameObject rMatcha;
	public GameObject rPumpkin;

	[Header("Lazlo Coffees")]
	public GameObject lCoffee;
	public GameObject lHotChocolate;
	public GameObject lMatcha;
	public GameObject lPumpkin;

	[Header("Saanvi Coffees")]
	public GameObject svCoffee;
	public GameObject svHotChocolate;
	public GameObject svMatcha;
	public GameObject svPumpkin;

	[Header("Brinley Coffees")]
	public GameObject bCoffee;
	public GameObject bHotChocolate;
	public GameObject bMatcha;
	public GameObject bPumpkin;

	[Header("Santiago Coffees")]
	public GameObject saCoffee;
	public GameObject saHotChocolate;
	public GameObject saMatcha;
	public GameObject saPumpkin;


	private void Awake() {
        Inst = this;
	}

	public void GiveCoffee(string name, CoffeeManager.BrewType brew) {
		switch (brew) {
			case CoffeeManager.BrewType.Coffee:
				switch (name) {
					case "Reginald":
						rCoffee.SetActive(true);
						break;
					case "Lazlo":
						lCoffee.SetActive(true);
						break;
					case "Saanvi":
						svCoffee.SetActive(true);
						break;
					case "Santiago":
						saCoffee.SetActive(true);
						break;
					case "Brinley":
						bCoffee.SetActive(true);
						break;
				}
				break;
			case CoffeeManager.BrewType.Cocoa:
				switch (name) {
					case "Reginald":
						rHotChocolate.SetActive(true);
						break;
					case "Lazlo":
						lHotChocolate.SetActive(true);
						break;
					case "Saanvi":
						svHotChocolate.SetActive(true);
						break;
					case "Santiago":
						saHotChocolate.SetActive(true);
						break;
					case "Brinley":
						bHotChocolate.SetActive(true);
						break;
				}
				break;
			case CoffeeManager.BrewType.Matcha:
				switch (name) {
					case "Reginald":
						rMatcha.SetActive(true);
						break;
					case "Lazlo":
						lMatcha.SetActive(true);
						break;
					case "Saanvi":
						svMatcha.SetActive(true);
						break;
					case "Santiago":
						saMatcha.SetActive(true);
						break;
					case "Brinley":
						bMatcha.SetActive(true);
						break;
				}
				break;
			case CoffeeManager.BrewType.Pumpkin:
				switch (name) {
					case "Reginald":
						rPumpkin.SetActive(true);
						break;
					case "Lazlo":
						lPumpkin.SetActive(true);
						break;
					case "Saanvi":
						svPumpkin.SetActive(true);
						break;
					case "Santiago":
						saPumpkin.SetActive(true);
						break;
					case "Brinley":
						bPumpkin.SetActive(true);
						break;
				}
				break;

		}

	}

}
