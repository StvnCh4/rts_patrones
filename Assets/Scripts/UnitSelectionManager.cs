using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitSelectionManager : MonoBehaviour
{
  //Dado que solo se necesita un manager para selección de unidades
  //Se utiliza el patrón singleton
  
  public static UnitSelectionManager Instance { get; set; }

  public List<GameObject> allUnitsList = new List<GameObject>(); //Lista de todas las unidades
  public List<GameObject> unitsSelected = new List<GameObject>(); //Lista de las unidades seleccionadas

  public LayerMask clickable;
  public LayerMask ground;
  public GameObject groundMarker;
  
  private Camera cam;

  public void Start()
  {
      cam = Camera.main;
  }
  
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

  private void Update()
  {
      if (Input.GetMouseButtonDown(0))
      {
          RaycastHit hit;
          Ray ray = cam.ScreenPointToRay(Input.mousePosition);

          //Si se da click en un objeto clickeable
          if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
          {
              SelectByClicking(hit.collider.gameObject);
          }
          else //Si no damos click en un objeto clickeable
          {
              //se deselecciona todo
              DeselectAll();
          }
      }
  }

  private void DeselectAll()
  {
      throw new NotImplementedException();
  }

  private void SelectByClicking(GameObject unit)
  {
      DeselectAll();
      
      unitsSelected.Add(unit);
      
      EnableUnitMovement(unit,true);
  }

  private void EnableUnitMovement(GameObject unit, bool shouldMove)
  {
      throw new NotImplementedException();

  }
}
