using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public Money money;
    public FloatVariable enemyDamage; 
    public FloatVariable abilityRecharge;
    public FloatVariable bulletSpeed;
    public FloatVariable maxJumps;
    public FloatVariable meleeDamage;
    public FloatVariable movementSpeed;
    public FloatVariable playerDamage;
    public FloatVariable reloadSpeed;
    public FloatVariable tbAbilityRecharge;
    public FloatVariable tbBulletDamage;
    public FloatVariable tbBulletSpeed;
    public FloatVariable tbMaxJumps;
    public FloatVariable tbMeleeDamage;
    public FloatVariable tbMovementSpeed;
    public FloatVariable tbRealoadSpeed;

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

        tbAbilityRecharge.Value = 0;
        tbBulletDamage.Value = 0;
        tbBulletSpeed.Value = 0;
        tbMaxJumps.Value = 1;
        tbMeleeDamage.Value = 0;
        tbMovementSpeed.Value = 0;
        tbRealoadSpeed.Value = 0;
        SceneManager.LoadScene("Lobby");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
