using System;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance{get; set;}
    
    //creando dos listas, una con todas unidades, la otra con las seleccionadas
    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask clickable;

    public LayerMask ground;
    public GameObject groundMarker;
    
    public  LayerMask attackable;
    public bool attackCursorVisible;
    private Camera cam;
    
    //solo un manager para unit selection
    //patron Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //Inicia camara
    private void Start()
    {
        cam = Camera.main;
    }
    
    //para seleccionar unidades o bien deseleccionar si no se toca alguna
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                //para seleccionar multiples unidades

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                DeselectAll();
            }
        }

        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;   
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }

        }
        
        
        //Attack target
        
        if (unitsSelected.Count > 0 && AtleastOneOffensiveUnit(unitsSelected))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                Debug.Log("Enemy hovered with mouse");

                attackCursorVisible = true;

                if (Input.GetMouseButton(1))
                {
                    Transform target = hit.transform;

                    foreach (GameObject unit in unitsSelected)
                    {
                        if (unit.GetComponent<AttackController>())
                        {
                            unit.GetComponent<AttackController>().targetToAttack = target;
                        }
                        
                    }
                }
            }
            else
            {
                attackCursorVisible = false;
            }

        }

    }

    private bool AtleastOneOffensiveUnit(List<GameObject> unitsSelected)
    {
        foreach (GameObject unit in unitsSelected)
        {
            if (unit.GetComponent<AttackController>())
            {
                return true;
            }
        }
        return false;
    }

    private void MultiSelect(GameObject unit)
    {

        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);

        }
        else
        {
            SelectUnit(unit, false);

            unitsSelected.Remove(unit);
        }
    }

    public void DeselectAll()
    {
        //para desactivar el movimiento a las unidades seleccionadas
        foreach (var unit in unitsSelected)
        {
            SelectUnit(unit, false);
        }
        groundMarker.SetActive(false);
        unitsSelected.Clear();
        
    }

    private void SelectByClicking(GameObject unit)
    {
        //Quita la seleccion de otras unidades
        DeselectAll();
        //Agrega a la lista de unidades seleccionadas
        unitsSelected.Add(unit);
        SelectUnit(unit, true);


    }

    private void SelectUnit(GameObject unit, bool isSelected)
    {
        TriggerSelectionIndicator(unit,isSelected);
        EnableUnitMovement(unit,isSelected);
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
    }


    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

    public void DragSelect(GameObject unit)
    {
        if (unitsSelected.Contains(unit)==false)
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);
        }
    }
}
