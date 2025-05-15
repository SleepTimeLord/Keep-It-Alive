using UnityEngine;

public interface ITriggerInAttackRangeCheckable
{
    bool isInAttackRange { get; set; }

    void SetAttackRangeBool(bool inAttackRange);
}
