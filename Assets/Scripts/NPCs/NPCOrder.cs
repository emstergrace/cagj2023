using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCOrder : MonoBehaviour, IInteractable
{
    public string CustomerName = "";
    public CoffeeManager.BrewType BrewOfChoice = CoffeeManager.BrewType.None;
    [Header("Conversations")]
    public NPCConversation preOrder = null;
    public NPCConversation withoutCoffee = null;

    public NPCConversation givingRightCoffeeYay = null;
    public NPCConversation givingRightCoffeeMeh = null;
    public NPCConversation givingRightCoffeeGross = null;
    public NPCConversation givingWrongCoffee = null;

    public NPCConversation givenCoffeeYay = null;
    public NPCConversation givenCoffeeMeh = null;
    public NPCConversation givenCoffeeGross = null;
    public NPCConversation givenWrongCoffee = null;

    [Header("Sounds")]
    public AudioSource soundSource = null;
    public AudioClip yaySound = null;
    public AudioClip mehSound = null;
    public AudioClip ewwSound = null;

    private bool takenOrder = false;
    private bool hasGivenCoffee = false;
    private int satisfied = 0; // -2, -1, 0, 1, for use in hasGivenCoffee

    public void SetHappy(int num) {
        HappinessMeter.Inst.Increment(num);
	}

	public void Interact() {

        // After given coffee
        if (hasGivenCoffee) {
            switch (satisfied) {
                case -2:
                    ConversationManager.Instance.StartConversation(givenWrongCoffee);
                    break;
                case -1:
                    ConversationManager.Instance.StartConversation(givenCoffeeGross);
                    break;
                case 0:
                    ConversationManager.Instance.StartConversation(givenCoffeeMeh);
                    break;
                case 1:
                    ConversationManager.Instance.StartConversation(givenCoffeeYay);
                    break;
			}
        }
        // Pre-ordering
        else {
            if (!takenOrder) {
                ConversationManager.Instance.StartConversation(preOrder);
                takenOrder = true;
            }
            else if (takenOrder) {
                // post-order, pre-giving coffee
                if (CoffeeManager.Inst.CurrentCoffee == null || CoffeeManager.Inst.CurrentCoffee.CustomerName != CustomerName) {
                    ConversationManager.Instance.StartConversation(withoutCoffee);
                }
                // giving coffee
                else if (CoffeeManager.Inst.CurrentCoffee.CustomerName == CustomerName) {
                    if (CoffeeManager.CurrentBrew != BrewOfChoice) {
                        ConversationManager.Instance.StartConversation(givingWrongCoffee);
                        HappinessMeter.Inst.Increment(-5);
                        soundSource.clip = ewwSound;
                        soundSource.Play();
                        satisfied = -2;
					}
                    else {
                        if (CoffeeManager.CurrentArt == CoffeeManager.Inst.CoffeeDict[CustomerName].coffeeArtChoices.Choices[0]) {
                            ConversationManager.Instance.StartConversation(givingRightCoffeeYay);
                            HappinessMeter.Inst.Increment(3);
                            soundSource.clip = yaySound;
                            soundSource.Play();
                            satisfied = 1;
                        }
                        if (CoffeeManager.CurrentArt == CoffeeManager.Inst.CoffeeDict[CustomerName].coffeeArtChoices.Choices[1]) {
                            ConversationManager.Instance.StartConversation(givingRightCoffeeMeh);
                            HappinessMeter.Inst.Increment(1);
                            soundSource.clip = mehSound;
                            soundSource.Play();
                            satisfied = 0;

                        }
                        if (CoffeeManager.CurrentArt == CoffeeManager.Inst.CoffeeDict[CustomerName].coffeeArtChoices.Choices[2]) {
                            ConversationManager.Instance.StartConversation(givingRightCoffeeGross);
                            HappinessMeter.Inst.Increment(-2);
                            soundSource.clip = ewwSound;
                            soundSource.Play();
                            satisfied = -1;
                        }
					}
                    hasGivenCoffee = true;

                }

            }
        }

	}


}
