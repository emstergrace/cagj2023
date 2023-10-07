using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CoffeeManager : MonoBehaviour
{
    public static CoffeeManager Inst { get; private set; }

	public List<PersonCoffeeChoice> CoffeeArtChoices = new List<PersonCoffeeChoice>();
	private Dictionary<string, PersonCoffeeChoice> coffeeDict = new Dictionary<string, PersonCoffeeChoice>(); public Dictionary<string, PersonCoffeeChoice> CoffeeDict { get { return coffeeDict; } }

	[Header("Final Dialogues")]
	public NPCConversation finishedOrdersConvo = null;
	public NPCConversation happyDay = null;
	public NPCConversation mehDay = null;
	public NPCConversation sadDay = null;

	[Header("Ending Panels")]
	public EndingPanelUI endingPanel = null;

	public Coffee CurrentCoffee { get; private set; } = null;

	public static BrewType CurrentBrew { get { return Inst.CurrentCoffee.Brew; } }
	public static CoffeeArt CurrentArt { get { return Inst.CurrentCoffee.Art; } }

	private int ordersDone = 0;

	public bool FinishedAllOrders { get { return ordersDone >= coffeeDict.Count; } }

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
		PlayerController.Inst.HoldCoffee(true);
	}

	public void FinishedOrder() {
		ordersDone++;
		CurrentCoffee = null;
		PlayerController.Inst.HoldCoffee(false);

		if (ordersDone == coffeeDict.Count) {
			StartCoroutine(QueueFinishedOrderConvo());
		}
	}

	IEnumerator QueueFinishedOrderConvo() {
		yield return null;
		while (ConversationManager.Instance.IsConversationActive) {
			yield return null;
		}

		ConversationManager.Instance.StartConversation(finishedOrdersConvo);
	}

	public void FinishDay() {
		if (FinishedAllOrders) {
			if (HappinessMeter.Inst.State == HappyState.Happy) {
				ConversationManager.Instance.StartConversation(happyDay);
			}
			else if (HappinessMeter.Inst.State == HappyState.Neutral) {
				ConversationManager.Instance.StartConversation(mehDay);
			}
			else {
				ConversationManager.Instance.StartConversation(sadDay);
			}
			StartCoroutine(ShowGameOverPanel());
		}
	}

	IEnumerator ShowGameOverPanel() {
		yield return null;
		while (ConversationManager.Instance.IsConversationActive) {
			yield return null;
		}

		if (HappinessMeter.Inst.State == HappyState.Happy) {
			endingPanel.happyPanel.SetActive(true);
		}
		else if (HappinessMeter.Inst.State == HappyState.Neutral) {
			endingPanel.neutralPanel.SetActive(true);
		}
		else {
			endingPanel.sadPanel.SetActive(true);
		}
	}

	public void QuitGame() {
		Application.Quit();
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