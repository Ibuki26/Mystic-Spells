using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ʏ펞�̉񕜗ʂ̐ݒ�
public class NormalHeal : IHealStrategy
{
    public int CalculateHeal(int healPower)
    {
        return Mathf.Max(0, healPower);
    }
}
