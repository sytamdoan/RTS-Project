using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitsSelect : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = new LayerMask();
    private Camera mainCamera;

    private List<Unit> selectedUnits = new List<Unit>();
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            foreach (Unit selectedUnit in selectedUnits)
            {
                selectedUnit.Deselect();
            }

            selectedUnits.Clear();
        }
        else if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            SelectionArea();
        }

    }
    private void SelectionArea()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        if(!hit.collider.TryGetComponent<Unit>(out Unit unit)) { return; }

        if(!unit.hasAuthority) { return; }

        selectedUnits.Add(unit);

        foreach (Unit selectedUnit in selectedUnits)
        {
            selectedUnit.Select();
        }
    }
}
