using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable
{
    void Heal(int healValue);
    IEnumerator HealOverTime(int healValue, double time);
}
