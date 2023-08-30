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
    public DefenceApplication defenceApp;

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
    public IEnumerator CardUsesCorrectEnergyTest()
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

        // clean up the test data
        playerLogic.currentEnergy = 0; // reset player defence

        // wait for any actions to complete
        yield return null;
    }



    // -------------------------------------------------------------------------------------------------------------------------------
    [UnityTest] // CHECK CARD APPLIES CORRECT DAMAGE -- CH
    public IEnumerator CardAttackDamageTest()
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
        playerLogic.currentEnergy = 5; // enough energy to play any card once

        // enemy
        GameObject enemy = GameObject.Find("Enemy");
        EntityStats entityStats = enemy.GetComponent<EntityStats>();
        entityStats.maxHealth = 10; // high enough to take damage from a card
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
        thisCard.energyCost = 1; // low enough to be used

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
        thisCard.damage = 0; // reset card data

        // wait for any actions to complete
        yield return null;
    }


    // -------------------------------------------------------------------------------------------------------------------------------
    [UnityTest] // CHECK CARD APPLIES CORRECT DEFENCE -- CH
    public IEnumerator CardDefenceTest()
    {

        Debug.Log("Card Defence Test:"); // print test for console
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
        EntityStats entityStats = player.GetComponent<EntityStats>(); // get player stats
        playerLogic.currentEnergy = 5; // enough energy to play any card once
        entityStats.defence = 0; // no defence on start
        

        Debug.Log("Player active with " + entityStats.getCurrentDefence() + " defence");

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
        thisCard.defence = 2; // set to set amount for testing

        Debug.Log("Defence = " + thisCard.defence);
        defenceApp = player.GetComponent<DefenceApplication>();
        defenceApp.playerHand = playerHand;
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
                defenceApp.testDefence(); // defence method designed for testing that skips uneccearies like animations and sounds
            }
            else { Debug.Log("selectCard is null"); } // error handling
        }
        else
        {
            Debug.Log("cardgameobject is null"); // error handling
        }

        // ensure that player energy has been correctly updated after using the card
        Assert.AreEqual(2, entityStats.getCurrentDefence()); // initial defence 0 + card defence 2 = 2
        Debug.Log("Updated Player Defence is " + entityStats.getCurrentDefence());

        // clean up the test data
        thisCard.defence = 0; // reset card data
        entityStats.defence = 0; // reset player defence

        // wait for any actions to complete
        yield return null;
    }



    // -------------------------------------------------------------------------------------------------------------------------------
    [UnityTest] // CHECK CARD APPLIES CORRECT MOVE FOR SPECIAL O1 (+5 HEALTH) -- CH
    public IEnumerator CardSpecial01Test()
    {

        Debug.Log("Card Get Enough Sleep Special Test:"); // print test for console
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
        EntityStats entityStats = player.GetComponent<EntityStats>(); // get player stats
        playerLogic.currentEnergy = 5; // enough energy to play any card once
        entityStats.maxHealth = 10; // high enough to take damage from a card
        entityStats.health = 5; // less than MAX health as card adds health
        Assert.AreEqual(5, entityStats.getCurrentHealth());

        Debug.Log("Player active with " + entityStats.getCurrentHealth() + " health");

        // clear cards to be dealt
        levelLoader.availableCards.Clear();

        // get player hand
        Hand playerHand = player.GetComponent<Hand>();

        // load card01 into scene
        cardUserPref = GameObject.Find("Progress").GetComponent<CardUserPref>();
        cardUserPref.PopulateCardDictionary();
        GameObject playerCard = cardUserPref.GetCardByName("Card10"); // card10 = special01
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

        defenceApp = player.GetComponent<DefenceApplication>();
        defenceApp.playerHand = playerHand;
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
                defenceApp.applySpecialOneTest(); // method designed for testing that skips uneccearies like animations and sounds
            }
            else { Debug.Log("selectCard is null"); } // error handling
        }
        else
        {
            Debug.Log("cardgameobject is null"); // error handling
        }

        // ensure that player energy has been correctly updated after using the card
        Assert.AreEqual(10, entityStats.getCurrentHealth()); // initial health 5 + card special heal 5 = 10
        Debug.Log("Updated Player Health is " + entityStats.getCurrentHealth());

        // clean up the test data
        entityStats.maxHealth = 10; // reset player maxhealth
        entityStats.health = entityStats.maxHealth; // reset player health

        // wait for any actions to complete
        yield return null;
    }



    // -------------------------------------------------------------------------------------------------------------------------------
    [UnityTest] // CHECK CARD APPLIES CORRECT MOVE FOR SPECIAL O2 (+3 ENERGY) -- CH
    public IEnumerator CardSpecial02Test()
    {

        Debug.Log("Card Eenrgy Drink Test:"); // print test for console

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
        playerLogic.currentEnergy = 2; // set as low amount for adding energy from specal card
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
        GameObject playerCard = cardUserPref.GetCardByName("Card11");
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
        defenceApp = player.GetComponent<DefenceApplication>();
        defenceApp.playerHand = playerHand;

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
                defenceApp.applySpecialTwoTest(); // method designed for testing that skips uneccearies like animations and sounds
            }
            else { Debug.Log("selectCard is null"); } // error handling
        }
        else
        {
            Debug.Log("cardgameobject is null"); // error handling
        }

        // ensure that player energy has been correctly updated after using the card
        Assert.AreEqual(5, playerLogic.currentEnergy); // initial energy 3 + card enrgy addition 3 = 5
        Debug.Log("Remaining energy is " + playerLogic.currentEnergy);

        // clean up the test data
        playerLogic.currentEnergy = 0; // reset player defence

        // wait for any actions to complete
        yield return null;
    }


    // -------------------------------------------------------------------------------------------------------------------------------
    [UnityTest] // CHECK CARD APPLIES CORRECT MOVE FOR SPECIAL O3 (DOUBLE DAMAGE ON NEXT ATTACK) -- CH
    public IEnumerator CardSpecial03Test()
    {

        Debug.Log("Card Workout Test:"); // print test for console
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
        playerLogic.currentEnergy = 10; // enough energy to play special card (4) and attack card (3)

        // enemy
        GameObject enemy = GameObject.Find("Enemy");
        EntityStats entityStats = enemy.GetComponent<EntityStats>();

        entityStats.maxHealth = 20; // high enough to take damage from a card with double damage
        entityStats.health = entityStats.maxHealth;
        Assert.AreEqual(20, entityStats.getCurrentHealth());

        Debug.Log("Enemy active with " + entityStats.getCurrentHealth() + " health");

        // clear cards to be dealt
        levelLoader.availableCards.Clear();

        // get player hand
        Hand playerHand = player.GetComponent<Hand>();

        // load card01 and card12 into scene
        cardUserPref = GameObject.Find("Progress").GetComponent<CardUserPref>();
        cardUserPref.PopulateCardDictionary();
        GameObject playerCardSpecial = cardUserPref.GetCardByName("Card12");
        GameObject playerCardAttack = cardUserPref.GetCardByName("Card01");
        Debug.Log("Special Card: " + playerCardSpecial);
        Debug.Log("Attack Card: " + playerCardAttack);

        // load special card into hand // card12
        Transform playerAreaTransform = levelLoader.PlayerArea.transform;
        GameObject instantiatedCard01 = Instantiate(playerCardSpecial, Vector3.zero, Quaternion.identity);
        instantiatedCard01.transform.SetParent(playerAreaTransform, false);

        // load attack card into hand // card01
        GameObject instantiatedCard02 = Instantiate(playerCardAttack, Vector3.zero, Quaternion.identity);
        instantiatedCard02.transform.SetParent(playerAreaTransform, false);

        // check there are cards in the player's hand
        if (playerAreaTransform.childCount == 0)
        {
            Debug.LogError("No cards in the player's hand.");
            yield break;
        }

        // get components for using special card
        GameObject specialCardGameObject = levelLoader.PlayerArea.transform.GetChild(0).gameObject;
        SelectCard specialSelectCard = specialCardGameObject.GetComponent<SelectCard>();
        ThisCard thisSpecialCard = playerCardSpecial.GetComponent<ThisCard>();

        // get components for using attack card
        GameObject attackCardGameObject = levelLoader.PlayerArea.transform.GetChild(1).gameObject;
        SelectCard attackSelectCard = attackCardGameObject.GetComponent<SelectCard>();
        ThisCard thisAttackCard = playerCardAttack.GetComponent<ThisCard>();

        // get energy special
        GameObject cardEnergyParentSpecial = playerCardSpecial.transform.Find("EnergyCircle").gameObject;
        TMP_Text cardSpecialEnergy = cardEnergyParentSpecial.transform.Find("EnergyCostText").gameObject.GetComponent<TextMeshProUGUI>();
        string specialEnergyString = cardSpecialEnergy.text; // save as string
        int.TryParse(specialEnergyString, out int specialEnergy); // parse as int
        thisSpecialCard.energyCost = specialEnergy;

        // get energy attack
        GameObject cardEnergyParentAttack = playerCardAttack.transform.Find("EnergyCircle").gameObject;
        TMP_Text cardAttackEnergy = cardEnergyParentAttack.transform.Find("EnergyCostText").gameObject.GetComponent<TextMeshProUGUI>();
        string attackEnergyString = cardAttackEnergy.text; // save as string
        int.TryParse(attackEnergyString, out int attackEnergy); // parse as int
        thisAttackCard.energyCost = attackEnergy;

        damagecalc = enemy.GetComponent<DamageCalculation>();

        // check set correctly
        thisAttackCard.damage = 3; // set to set amount for testing

        Debug.Log("Attack Card Damage = " + thisAttackCard.damage);
        damagecalc = enemy.GetComponent<DamageCalculation>();
        damagecalc.playerHand = playerHand;

        Debug.Log("ThisSpecialCard: " + thisSpecialCard);
        Debug.Log("ThisAttackCard: " + thisAttackCard);

        // check special card not null before continuing
        if (specialCardGameObject != null) 
        {
            if (specialSelectCard != null)
            {
                // "click" the card to select
                playerLogic.myTurn = true;
                specialSelectCard.player = player;

                // apply the card
                playerHand.currentlySelectedCard = thisSpecialCard;
                Debug.Log("CurrentlySelectedCard: " + playerHand.currentlySelectedCard);
                defenceApp.applySpecialThreeTest(); // method designed for testing that skips uneccearies like animations and sounds
                
            }
            else { Debug.Log("selectSpecialCard is null"); } // error handling
        }
        else
        {
            Debug.Log("specialCardgameobject is null"); // error handling
        }

        
        // ensure that double damage is active
        Assert.AreEqual(true, entityStats.specialx2); // specialx2 is set to true after playing special card
        Debug.Log("Double Damage Active = " + entityStats.specialx2);

        // check attack card not null before continuing
        if (attackCardGameObject != null)
        {
            if (attackSelectCard != null)
            {
                // "click" the card to select
                playerLogic.myTurn = true;
                attackSelectCard.player = player;

                // apply the card
                playerHand.currentlySelectedCard = thisAttackCard;
                Debug.Log("CurrentlySelectedCard: " + playerHand.currentlySelectedCard);
                damagecalc.testAttack(); // attack method designed for testing that skips uneccearies like animations and sounds
            }
            else { Debug.Log("selectAttackCard is null"); } // error handling
        }
        else
        {
            Debug.Log("attackCardgameobject is null"); // error handling
        }

        // ensure that enemy health has been correctly decreased after using the card
        Assert.AreEqual(14, entityStats.getCurrentHealth()); // initial health 20 - (card damage 3x2 = 6) = 14
        Debug.Log("Remaining enemy health is " + entityStats.getCurrentHealth());

        // ensure that double damage is now inactive
        Assert.AreEqual(false, entityStats.specialx2); // specialx2 is set to true after playing special card
        Debug.Log("Double Damage Active = " + entityStats.specialx2);

        // clean up the test data
        thisAttackCard.damage = 0; // reset card data

        // wait for any actions to complete
        yield return null;
    }


}