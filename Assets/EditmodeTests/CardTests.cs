using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class CardTests : MonoBehaviour
{
    public Card card;
    public GameObject player, enemy;
    public Hand playerHand;
    public CardUserPref cardUserPref;
    public DamageCalculation damagecalc;



    // -------------------------------------------------------------------------------------------------------------------------------
    [UnitySetUp] // basic test level setup with scripts, player, enemy, hand, and empty cards
    public IEnumerator SetUp()
    {
        // Load the level scene
        EditorSceneManager.OpenScene("Assets/Scenes/TestScene.unity");
        // wait for the scene to load to ensure access to objects
        yield return null;
    }



    // -------------------------------------------------------------------------------------------------------------------------------
    [UnityTest] // ENERGY USAGE CHECK ON CARD PLAY -- CH
    public IEnumerator CardUsesCorrectEnergy()
    {

        Debug.Log("Card Energy Test:"); // print test for console
        // set up as above
        SetUp();

        // set things needed for testing (player, card, other scripts referenced)
        // level load script for initialising cards
        GameObject background = GameObject.Find("Background");
        if (background == null)
        {
            Debug.LogError("Background object not found");
            yield break;
        }
        LevelLoad levelLoader = background.GetComponent<LevelLoad>();

        // player and energy
        GameObject player = GameObject.Find("Player");
        PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();
        playerLogic.currentEnergy = 5;
        Debug.Log("Player active with " + playerLogic.currentEnergy + " energy");

        // enemy
        GameObject enemy = GameObject.Find("Enemy");
        // clear cards to be dealt
        levelLoader.availableCards.Clear();

        // get player hand
        Hand playerHand = player.GetComponent<Hand>();

        // load card01 into scene
        cardUserPref = GameObject.Find("Progress").GetComponent<CardUserPref>();
        cardUserPref.PopulateCardDictionary();
        GameObject playerCard = cardUserPref.GetCardByName("Card01");
        Debug.Log("playerCard: " + playerCard);
        Transform playerAreaTransform = levelLoader.PlayerArea.transform;
        GameObject instantiatedCard = Instantiate(playerCard, Vector3.zero, Quaternion.identity);
        instantiatedCard.transform.SetParent(playerAreaTransform, false);

        // check there are cards in the player's hand
        if (playerAreaTransform.childCount == 0)
        {
            Debug.LogError("No cards in the player's hand.");
            yield break;
        }

        // get components for using card
        GameObject cardGameObject = levelLoader.PlayerArea.transform.GetChild(0).gameObject;
        SelectCard selectCard = cardGameObject.GetComponent<SelectCard>();
        ThisCard thisCard = playerCard.GetComponent<ThisCard>();

        // get energy
        GameObject cardEnergyParent = playerCard.transform.Find("EnergyCircle").gameObject;
        TMP_Text cardEnergy = cardEnergyParent.transform.Find("EnergyCostText").gameObject.GetComponent<TextMeshProUGUI>();
        string cardEnergyString = cardEnergy.text; // save as string
        int.TryParse(cardEnergyString, out int energy); // parse as int
        thisCard.energyCost = energy;
        damagecalc = enemy.GetComponent<DamageCalculation>();

        // check set correctly
        Debug.Log("Energy Cost = " + thisCard.energyCost);
        damagecalc.playerHand = playerHand;
        Debug.Log("ThisCard: " + thisCard);

        if (cardGameObject != null) // check not null before continuing
        {
            if (selectCard != null)
            {
                // "click" the card to select
                playerLogic.myTurn = true;
                selectCard.player = player;

                // apply the card
                playerHand.currentlySelectedCard = thisCard;
                Debug.Log("CurrentlySelectedCard: " + playerHand.currentlySelectedCard);
                damagecalc.testAttack();
            }
            else { Debug.Log("selectCard is null"); } // error handling
        }
        else
        {
            Debug.Log("cardgameobject is null"); // error handling
        }

        // ensure that player energy has been correctly updated after using the card
        Assert.AreEqual(3, playerLogic.currentEnergy); // initia energy 5 - card enrgy 2 = 3
        Debug.Log("Remaining energy is " + playerLogic.currentEnergy);

        // wait for any actions to complete
        yield return null;
    }



    // -------------------------------------------------------------------------------------------------------------------------------
    [UnityTest] // CHECK CARD APPLIES CORRECT DAMAGE -- CH
    public IEnumerator CardAttackDamage()
    {

        Debug.Log("Card Attack Test:"); // print test for console
        // set up as above
        SetUp();

        // set things needed for testing (player, card, other scripts referenced)
        // level load script for initialising cards
        GameObject background = GameObject.Find("Background");
        if (background == null)
        {
            Debug.LogError("Background object not found");
            yield break;
        }
        LevelLoad levelLoader = background.GetComponent<LevelLoad>();

        // player and energy
        GameObject player = GameObject.Find("Player");
        PlayerLogic playerLogic = player.GetComponent<PlayerLogic>();
        playerLogic.currentEnergy = 5;

        // enemy
        GameObject enemy = GameObject.Find("Enemy");
        EntityStats entityStats = enemy.GetComponent<EntityStats>();
        entityStats.maxHealth = 10;
        entityStats.health = entityStats.maxHealth;
        Assert.AreEqual(10, entityStats.getCurrentHealth());

        Debug.Log("Enemy active with " + entityStats.getCurrentHealth() + " health");

        // clear cards to be dealt
        levelLoader.availableCards.Clear();

        // get player hand
        Hand playerHand = player.GetComponent<Hand>();

        // load card01 into scene
        cardUserPref = GameObject.Find("Progress").GetComponent<CardUserPref>();
        cardUserPref.PopulateCardDictionary();
        GameObject playerCard = cardUserPref.GetCardByName("Card01");
        Debug.Log("playerCard: " + playerCard);

        Transform playerAreaTransform = levelLoader.PlayerArea.transform;
        GameObject instantiatedCard = Instantiate(playerCard, Vector3.zero, Quaternion.identity);
        instantiatedCard.transform.SetParent(playerAreaTransform, false);

        // check there are cards in the player's hand
        if (playerAreaTransform.childCount == 0)
        {
            Debug.LogError("No cards in the player's hand.");
            yield break;
        }

        // get components for using card
        GameObject cardGameObject = levelLoader.PlayerArea.transform.GetChild(0).gameObject;
        SelectCard selectCard = cardGameObject.GetComponent<SelectCard>();
        ThisCard thisCard = playerCard.GetComponent<ThisCard>();
        thisCard.energyCost = 1; // low enough to be used each test

        // check set correctly
        thisCard.damage = 3; // set to set amount for testing
        thisCard.tag = "Attack Card"; // set specifically for testing

        Debug.Log("Attack Cost = " + thisCard.damage);
        damagecalc = enemy.GetComponent<DamageCalculation>();
        damagecalc.playerHand = playerHand;
        Debug.Log("ThisCard: " + thisCard);

        if (cardGameObject != null) // check not null before continuing
        {
            if (selectCard != null)
            {
                // "click" the card to select
                playerLogic.myTurn = true;
                selectCard.player = player;

                // apply the card
                playerHand.currentlySelectedCard = thisCard;
                Debug.Log("CurrentlySelectedCard: " + playerHand.currentlySelectedCard);
                damagecalc.testAttack(); // attack method designed for testing that skips uneccearies like animations and sounds
            }
            else { Debug.Log("selectCard is null"); } // error handling
        }
        else
        {
            Debug.Log("cardgameobject is null"); // error handling
        }

        // ensure that player energy has been correctly updated after using the card
        Assert.AreEqual(7, entityStats.getCurrentHealth()); // initial health 10 - card damage 3 = 7
        Debug.Log("Remaining enemy health is " + entityStats.getCurrentHealth());

        // clean up the test data
        thisCard.damage = 0; // reset

        // wait for any actions to complete
        yield return null;
    }


    // -------------------------------------------------------------------------------------------------------------------------------
}