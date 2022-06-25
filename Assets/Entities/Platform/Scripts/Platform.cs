using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    [SerializeField] private Gate gateA;
    [SerializeField] private Gate gateB;
    [SerializeField] private UnitGroup unitGroup;
    [SerializeField] private Reference<UnitGroup> playerUnitGroup;

    public UnityEvent onInit;
    public UnityEvent onGatePassed;
    public UnityEvent onEncounterStarted;
    public UnityEvent onEncounterEnded;
    public UnityEvent onDispose;

    public void Init()
    {
        onInit.RemoveAllListeners();
        onGatePassed.RemoveAllListeners();
        onEncounterStarted.RemoveAllListeners();
        onEncounterEnded.RemoveAllListeners();
        onDispose.RemoveAllListeners();

        onInit.Invoke();
    }

    public void StartEncounter()
    {
        onEncounterStarted.Invoke();
        unitGroup.StartEncounter(playerUnitGroup.value);
        playerUnitGroup.value.StartEncounter(unitGroup);
    }

    public void EndEncounter()
    {
        onEncounterEnded.Invoke();
        unitGroup.EndEncounter();
        playerUnitGroup.value.EndEncounter();
    }

    public void GatePassed(Vector3 position, MathExpression expression)
    {
        playerUnitGroup.value.HandleExpression(position, expression);
        onGatePassed.Invoke();
    }

    public void Dispose()
    {
        onDispose.Invoke();
    }

    public void SetSection(LevelSection section)
    {
        SetUnits(section.enemies);
        gateA.SetGate(section.expressionA);
        gateB.SetGate(section.expressionB);
    }

    void SetUnits(int count)
    {
        unitGroup.HandleExpression(Vector3.zero, new MathExpression(MathExpression.Operation.Addition, count));
    }
}
