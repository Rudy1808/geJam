using UnityEngine;

public class TowerPlacerManager : MonoBehaviour
{
    public static TowerPlacerManager Instance { get; private set; }

    private TowerPlacer activePlacer;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void RequestActivation(TowerPlacer requester)
    {
        if (activePlacer == requester)
        {
            activePlacer.StopPlacing();
            activePlacer = null;
            return;
        }

        activePlacer?.StopPlacing();
        activePlacer = requester;
        requester.StartPlacing();
    }

    public void NotifyStopped(TowerPlacer placer)
    {
        if (activePlacer == placer)
            activePlacer = null;
    }
}