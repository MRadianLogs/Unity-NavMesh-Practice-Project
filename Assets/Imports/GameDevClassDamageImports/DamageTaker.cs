using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{

    [SerializeField] int hitPoints = 20;
    // Could add evasion and damage mitigation capabilities

    [SerializeField] ScrollingText damageText;
    [SerializeField] Color damageTextColor = Color.red;
    Vector3 textOffset = new Vector3(0f, 1.5f, 0f);


    public void TakeDamage(int hitAmount) //might also have damage type info for mitigation
    {
        hitPoints -= hitAmount;
        StartDamageText(hitAmount);
        // check for death
        Debug.Log("hit points: " + hitPoints.ToString());
    }

    void StartDamageText(int damage)
    {
        var text = damage.ToString();

        var scrollingText = Instantiate(damageText, transform.position+textOffset, Quaternion.identity);
        scrollingText.SetText(text);
        scrollingText.SetColor(damageTextColor);
    }
}
