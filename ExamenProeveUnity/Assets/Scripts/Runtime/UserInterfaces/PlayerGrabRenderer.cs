using System.Collections.Generic;
using System.Linq;
using Runtime;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Player;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MethodExtensions;

public class PlayerGrabRenderer : MonoBehaviour
{
    [SerializeField] private PlayerGrab leftHand;
    [SerializeField] private PlayerGrab rightHand;
    [SerializeField] private Image leftHandImage;
    [SerializeField] private Image rightHandImage;
    private List<Entity> _players;
    private List<PlayerGrab> _playerGrabs;

    private void Awake()
    {
        _players = PlayerManager.Instance.Players;
        foreach (var player in _players)
        {
            _playerGrabs = player.GetComponentsInChildren<PlayerGrab>().ToList();
        }
        
        if (_playerGrabs == null) return;
        
        foreach (var playerGrab in _playerGrabs)
        {
            if (playerGrab.HandType == HandType.Left)
            {
                leftHand = playerGrab;
                continue;
            }
            rightHand = playerGrab;
        }
        
        leftHand.onGrab.AddListener(UpdateVisual);
        leftHand.onRelease.AddListener(UpdateVisual);
        rightHand.onGrab.AddListener(UpdateVisual);
        rightHand.onRelease.AddListener(UpdateVisual);
        
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Grabbable leftHandGrabbable = leftHand.GrabbedGrabbable;
        Grabbable rightHandGrabbable = rightHand.GrabbedGrabbable;

        leftHandImage.enabled = false;
        rightHandImage.enabled = false;
        
        if (leftHandGrabbable != null)
        {
            leftHandImage.sprite = leftHandGrabbable.HasComponent<Product>() ? leftHandGrabbable.GetComponent<Product>().productScriptableObject.Icon : null;
            leftHandImage.enabled = rightHandImage.sprite != null;
        }

        if (rightHandGrabbable != null)
        {
            rightHandImage.sprite = rightHandGrabbable.HasComponent<Product>() ? rightHandGrabbable.GetComponent<Product>().productScriptableObject.Icon : null;
            rightHandImage.enabled = rightHandImage.sprite != null;
        }
    }
}
