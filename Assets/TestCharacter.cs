using System;
using Interactables.Enums;
using Interactables.Interfaces;
using UnityEngine;

public class TestCharacter : MonoBehaviour, IInteractor
{
   public Transform SelfTransform => transform;
   [field: SerializeField] public InteractorType InteractorType { get; private set; }
   [SerializeField] private float _speed;

   private void Update()
   {
      var axis = Input.GetAxisRaw("Horizontal");
      if (axis!=0)
      {
         transform.Translate(new Vector3(axis,0f,0f)*_speed);
      }
   }
}
