using Interactables.Enums;
using Interactables.Interfaces;
using UnityEngine;

public class TestCharacter : MonoBehaviour, IInteractor
{
   [field: SerializeField] public InteractorType InteractorType { get; private set; }
}
