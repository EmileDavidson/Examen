using System;
using System.Collections.Generic;
using Runtime;
using Runtime.Enums;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Utilities.MethodExtensions;

namespace Runtime
{
    /// <summary>
    /// Grabbable is a script you can put on objects that can be grabbed by the player.
    /// note that the gameobject needs a collider that is a trigger for this to work.
    /// </summary>
    public class Grabbable : MonoBehaviour, IGrabbable
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private bool snapToPivot;

        private bool _isGrabbed;
        private bool _isInteractable;
        private readonly List<Guid> _grabbedBy = new();

        public readonly UnityEvent OnGrabbed = new UnityEvent();
        public readonly UnityEvent OnReleased = new UnityEvent();

        private void Awake()
        {
            _isInteractable = gameObject.HasComponent<Interactable>();
            if (icon == null)
            {
                icon = gameObject.HasComponent<Product>() ? gameObject.GetComponent<Product>().productScriptableObject.Icon : null;
            }
            _isGrabbed = false;

            OnGrabbed?.AddListener(() => { _isGrabbed = true; });
            OnReleased?.AddListener(() => { _isGrabbed = false; });
        }

        public void AddGrabbedBy(Guid entity)
        {
            if (_grabbedBy.Contains(entity)) return;
            _grabbedBy.Add(entity);
        }

        public void RemoveGrabbedBy(Guid entity)
        {
            if (_isGrabbed) return;
            _grabbedBy.Remove(entity);
        }


        public List<Guid> GrabbedBy => _grabbedBy;
        public bool SnapToPivot => snapToPivot;
        public bool IsGrabbed => _isGrabbed;
        public bool IsInteractable => _isInteractable;
        public Sprite Icon => icon;
    }
}