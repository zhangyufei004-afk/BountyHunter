using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject menu;
    public GameObject credits;
    public Animator creditAnimator;

    [Header("Scriptable Objects")]
    public Money money;
    public FloatVariable enemyDamage; 
    public FloatVariable abilityRecharge;
    public FloatVariable bulletSpeed;
    public FloatVariable maxJumps;
    public FloatVariable meleeDamage;
    public FloatVariable movementSpeed;
    public FloatVariable playerDamage;
    public FloatVariable reloadSpeed;
    public ShopTimesBought tbAbilityRecharge;
    public ShopTimesBought tbBulletDamage;
    public ShopTimesBought tbBulletSpeed;
    public ShopTimesBought tbMaxJumps;
    public ShopTimesBought tbMeleeDamage;
    public ShopTimesBought tbMovementSpeed;
    public ShopTimesBought tbRealoadSpeed;

    public SceneChange levelOne;
    public SceneChange levelTwo;
    public SceneChange levelThree;
    public SceneChange levelFour;
    public SceneChange levelFive;
    public SceneChange levelSix;

    void Start()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void StartButton()
    {
        money.MoneyValue = 0;
        enemyDamage.Value = 1;
        abilityRecharge.Value = 10;
        bulletSpeed.Value = 15;
        maxJumps.Value = 1;
        meleeDamage.Value = 0;
        movementSpeed.Value = 10;
        playerDamage.Value = 1;
        reloadSpeed.Value = 0.5f;

        tbAbilityRecharge.TimesShopped = 0;
        tbBulletDamage.TimesShopped = 0;
        tbBulletSpeed.TimesShopped = 0;
        tbMaxJumps.TimesShopped = 1;
        tbMeleeDamage.TimesShopped = 0;
        tbMovementSpeed.TimesShopped = 0;
        tbRealoadSpeed.TimesShopped = 0;

        levelOne.LevelActive = true;
        levelTwo.LevelActive = false;
        levelThree.LevelActive = false;
        levelFour.LevelActive = false;
        levelFive.LevelActive = false;
        levelSix.LevelActive = false;

        levelOne.LevelCompleted = false;
        levelTwo.LevelCompleted = false;
        levelThree.LevelCompleted = false;
        levelFour.LevelCompleted = false;
        levelFive.LevelCompleted = false;
        levelSix.LevelCompleted = false;

        SceneManager.LoadScene("Lobby");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void OnCreditButton()
    {
        credits.SetActive(true);
        creditAnimator.SetTrigger("CreditStart");
        menu.SetActive(false);
    }

    public void OnBackButton()
    {
        creditAnimator.ResetTrigger("CreditStart");
        credits.SetActive(false);
        menu.SetActive(true);
    }
}
