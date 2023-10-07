using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class CoffeeBrewUI : MonoBehaviour
{

    public static CoffeeBrewUI Inst { get; private set; }

    [SerializeField] private GameObject OrdersGO = null;
    [SerializeField] private GameObject CoffeeChoicesBG = null;
    [SerializeField] private GameObject CoffeeChoicesGO = null;
    [SerializeField] private GameObject BorderGO = null;
    [SerializeField] private Transform CustomersContainer = null;
    [SerializeField] private GameObject brewButtonGO = null;
    [SerializeField] private GameObject backButtonGO = null;
    [SerializeField] private GameObject fallSprites = null;

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

    [Header("")]
    [SerializeField] private NPCConversation alreadyHaveCoffee;


    private string customerName;
    private CoffeeManager.BrewType brewChoice = CoffeeManager.BrewType.None;
    private CoffeeArt artChoice = CoffeeArt.None;

    private Dictionary<string, GameObject> CustomerDict = new Dictionary<string, GameObject>();

    public bool IsBrewing { get; private set; } = false;

	private void Awake() {
        Inst = this;
	}

	private void Start() {

        // test
        //AddCustomer("Reginald");
        //AddCustomer("Reginald");
    }

	public void ActivateCoffeemachine() {
        if (CoffeeManager.Inst.CurrentCoffee == null) {
            BorderGO.SetActive(true);
            OrdersGO.SetActive(true);
            backButtonGO.SetActive(true);

            AudioManager.Inst.PlaySound("steam");

            IsBrewing = true;
        }
        else {
            ConversationManager.Instance.StartConversation(alreadyHaveCoffee);
		}
	}

    public void CancelCoffee() {
        artChoice = CoffeeArt.None;
        brewChoice = CoffeeManager.BrewType.None;
        ClickArtButton(-1);
        ClickCoffeeButton(-1);
        AudioManager.Inst.StopSound("steam");

        BorderGO.SetActive(false);
        OrdersGO.SetActive(false);
        CoffeeChoicesGO.SetActive(false);
        CoffeeChoicesBG.SetActive(false);
        brewButtonGO.SetActive(false);
        fallSprites.SetActive(false);
        backButtonGO.SetActive(false);


        IsBrewing = false;
	}

    public void AddCustomer(string name) {
        GameObject newCust = Instantiate(customerFacePrefab, CustomersContainer);
        newCust.GetComponent<UnityEngine.UI.Image>().sprite = ResourcesLibrary.Inst.CustomerDictionary[name];
        newCust.GetComponent<Button>().onClick.AddListener(() => { SetCustomer(name); newCust.transform.localScale = Vector3.one * 1.35f; });
        CustomerDict.Add(name, newCust);
	}

    public void RemoveCustomer(string name) {
        Destroy(CustomerDict[name]);
        CustomerDict.Remove(name);
	}

    public void SetCustomer(string name) {
        customerName = name;
        CoffeeChoicesGO.SetActive(true);
        CoffeeChoicesBG.SetActive(true);
        fallSprites.SetActive(true);
        brewButtonGO.SetActive(false);

        ClickCoffeeButton(-1);
        ClickArtButton(-1);

		ArtOne.onClick.AddListener(() => { SetCoffeeArt(CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[0]); ClickArtButton(0);  });
		ArtTwo.onClick.AddListener(() => { SetCoffeeArt(CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[1]); ClickArtButton(1);  });
		ArtThree.onClick.AddListener(() => { SetCoffeeArt(CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[2]); ClickArtButton(2);  });

		ArtOneImage.sprite = ResourcesLibrary.Inst.CoffeeArtDictionary[CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[0]];
		ArtTwoImage.sprite = ResourcesLibrary.Inst.CoffeeArtDictionary[CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[1]];
        ArtThreeImage.sprite = ResourcesLibrary.Inst.CoffeeArtDictionary[CoffeeManager.Inst.CoffeeDict[name].coffeeArtChoices.Choices[2]];

        foreach(KeyValuePair<string, GameObject> kvp in CustomerDict) {
            kvp.Value.transform.localScale = Vector3.one;
		}

    }

    public void SetCoffeeType(CoffeeManager.BrewType brewType) {
        brewChoice = brewType;
	}

    public void SetCoffeeArt(CoffeeArt artType) {
        artChoice = artType;
	}

    public void ClickCoffeeButton(int num) {
        AudioManager.Inst.PlaySound("interact");
        switch (num) {
            case 0:
                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                brewChoice = CoffeeManager.BrewType.Coffee;
                break;

            case 1:
                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                brewChoice = CoffeeManager.BrewType.Matcha;
                break;
            case 2:
                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                brewChoice = CoffeeManager.BrewType.Cocoa;
                break;

            case 3:
                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorderPressed;
                brewChoice = CoffeeManager.BrewType.Pumpkin;
                break;

            case -1:
                CoffeeBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                MatchaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                CocoaBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                PumpkinBorder.sprite = ResourcesLibrary.Inst.CircleBorder;
                brewChoice = CoffeeManager.BrewType.None;
                break;
		}
        VerifyCoffee();
	}

    public void ClickArtButton(int num) {
        AudioManager.Inst.PlaySound("interact");
        switch (num) {
            case 0:
                ((Image)ArtOne.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorderPressed;
                ((Image)ArtTwo.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtThree.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                break;
            case 1:
                ((Image)ArtOne.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtTwo.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorderPressed;
                ((Image)ArtThree.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                break;
            case 2:
                ((Image)ArtOne.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtTwo.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtThree.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorderPressed;
                break;
            case -1:
                ((Image)ArtOne.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtTwo.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                ((Image)ArtThree.targetGraphic).sprite = ResourcesLibrary.Inst.SquareBorder;
                break;

        }
        VerifyCoffee();
	}

    private void VerifyCoffee() {
        if (brewChoice != CoffeeManager.BrewType.None && artChoice != CoffeeArt.None) {
            brewButtonGO.SetActive(true);
        }
	}

    public void BrewButton() {
        CoffeeManager.Inst.CreateCoffee(customerName, brewChoice, artChoice);
        ArtOne.onClick.RemoveAllListeners();
        ArtTwo.onClick.RemoveAllListeners();
        ArtThree.onClick.RemoveAllListeners();

        AudioManager.Inst.PlaySound("coffeepour");
        brewButtonGO.GetComponent<Animator>().SetBool("IsBrewing", true);
        brewButtonGO.GetComponent<Button>().interactable = false;
        backButtonGO.GetComponent<Button>().interactable = false;
        StartCoroutine(DisableGOs());
	}

	private IEnumerator DisableGOs() {

		yield return new WaitForSeconds(3.2f);

		brewButtonGO.GetComponent<Animator>().SetBool("IsBrewing", false);

		yield return new WaitForSeconds(1f);

        FloatingArrowManager.Inst.ActivateFloatingArrow(customerName, true);

        BorderGO.SetActive(false);
		OrdersGO.SetActive(false);
		CoffeeChoicesGO.SetActive(false);
        CoffeeChoicesBG.SetActive(false);
		brewButtonGO.SetActive(false);
        fallSprites.SetActive(false);
        backButtonGO.SetActive(false);
        brewButtonGO.GetComponent<Button>().interactable = true;
        backButtonGO.GetComponent<Button>().interactable = true;
        IsBrewing = false;

        AudioManager.Inst.PlaySound("playbutton");

        customerName = "";
	}
}
