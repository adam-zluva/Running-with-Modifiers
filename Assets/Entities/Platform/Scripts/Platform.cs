using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    [SerializeField] private Gate gateA;
    [SerializeField] private Gate gateB;
    [SerializeField] private UnitGroup unitGroup;
    [SerializeField] private Reference<UnitGroup> playerUnitGroup;

    public UnityEvent onGatePassed;
    public UnityEvent onEnconterStarted;
    public UnityEvent onEnconterEnded;

    public void StartEncounter()
    {
        unitGroup.units.ForEach(unit => unit.StartEncounter(playerUnitGroup.value));
        playerUnitGroup.value.units.ForEach(unit => unit.StartEncounter(unitGroup));

        onEnconterStarted.Invoke();
    }

    public void EndEncounter()
    {
        onEnconterEnded.Invoke();
    }

    public void GatePassed(Vector3 position, MathExpression expression)
    {
        playerUnitGroup.value.HandleExpression(position, expression);
        onGatePassed.Invoke();
    }

    public void SetSection(LevelSection section)
    {
        SetUnits(section.enemies);
        gateA.SetGate(section.expressionA);
        gateB.SetGate(section.expressionB);
    }

    void SetUnits(int count)
    {
        unitGroup.Clear();

        for (int i = 0; i < count; i++)
        {
            Vector3 unitCircle = new Vector3(Random.value, 0f, Random.value).Clamp(1f);
            unitGroup.SpawnUnit(unitCircle);
        }
    }
}
