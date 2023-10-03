using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeBrewUI : MonoBehaviour
{

    public static CoffeeBrewUI Inst { get; private set; }

    [SerializeField] private GameObject OrdersGO = null;
    [SerializeField] private GameObject CoffeeChoicesGO = null;
    [SerializeField] private GameObject BorderGO = null;
    [SerializeField] private Transform CustomersContainer = null;

    [SerializeField] private GameObject customerFacePrefab = null;

    [Header("Coffee Type")]
    [SerializeField] private Image CoffeeBorder = null;
    [SerializeField] private Image MatchaBorder = null;
    [SerializeField] private Image CocoaBorder = null;
    [SerializeField] private Image PumpkinBorder = null;

    [Header("Coffee Art")]
    [SerializeField] private Button ArtOne = null;
    [SerializeField] private Image ArtOneImage = null;
    [SerializeField] private Button ArtTwo = null;
    [SerializeField] private Image ArtTwoImage = null;
    [SerializeField] private Button ArtThree = null;
    [SerializeField] private Image ArtThreeImage = null;


    private string customerName;
    private CoffeeManager.BrewType brewChoice;
    private CoffeeArt artChoice;

    private List<GameObject> CustomerGOs = new List<GameObject>();

	private void Awake() {
        Inst = this;
        // test
        AddCustomer("Reginald");
        AddCustomer("Reginald");
	}

	public void ActivateCoffeemachine() {
        BorderGO.SetActive(true);
        OrdersGO.SetActive(true);
	}

    public void AddCustomer(string name) {
        GameObject newCust = Instantiate(customerFacePrefab, CustomersContainer);
        newCust.GetComponent<UnityEngine.UI.Image>().sprite = ResourcesLibrary.Inst.CustomerDictionary[name];
        newCust.GetComponent<Button>().onClick.AddListener(() => { SetCustomer(name); newCust.transform.localScale = Vector3.one * 1.35f; });
        CustomerGOs.Add(newCust);
	}

    public void SetCustomer(string name) {
        customerName = name;
        CoffeeChoicesGO.SetActive(true);
		ArtOne.onClick.AddListener(() => { ClickArtButton(1); SetCoffeeArt(CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[0]); });
		ArtTwo.onClick.AddListener(() => { ClickArtButton(2); SetCoffeeArt(CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[1]); });
		ArtThree.onClick.AddListener(() => { ClickArtButton(3); SetCoffeeArt(CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[2]); });

		ArtOneImage.sprite = ResourcesLibrary.Inst.CoffeeArtDictionary[CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[0]];
		ArtTwoImage.sprite = ResourcesLibrary.Inst.CoffeeArtDictionary[CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[1]];
        ArtThreeImage.sprite = ResourcesLibrary.Inst.CoffeeArtDictionary[CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[2]];

        for (int i = 0; i < CustomerGOs.Count; i++) {
            CustomerGOs[i].transform.localScale = Vector3.one;
		}

    }

    public void SetCoffeeType(CoffeeManager.BrewType brewType) {
        brewChoice = brewType;
	}

    public void SetCoffeeArt(CoffeeArt artType) {
        artChoice = artType;
	}

    public void ClickCoffeeButton(int num) {
        switch (num) {
            case 0:
                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                break;

            case 1:

                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                break;
            case 2:

                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                break;

            case 3:

                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                break;
		}
	}

    public void ClickArtButton(int num) {
        switch (num) {
            case 1:
                ((Image)ArtOne.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorderPressed;
                ((Image)ArtTwo.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtThree.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                break;
            case 2:
                ((Image)ArtOne.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtTwo.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorderPressed;
                ((Image)ArtThree.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                break;
            case 3:
                ((Image)ArtOne.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtTwo.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtThree.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorderPressed;
                break;

		}
	}

    public void BrewButton() {
        CoffeeManager.Inst.CreateCoffee(customerName, brewChoice, artChoice);
        ArtOne.onClick.RemoveAllListeners();
        ArtTwo.onClick.RemoveAllListeners();
        ArtThree.onClick.RemoveAllListeners();

        AudioManager.Inst.PlaySound("coffeepour");
        StartCoroutine(DisableGOs());
    }

    private IEnumerator DisableGOs() {

        yield return new WaitForSeconds(3.2f);

        BorderGO.SetActive(false);
        OrdersGO.SetActive(false);
        CoffeeChoicesGO.SetActive(false);
    }
}
