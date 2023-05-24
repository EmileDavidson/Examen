using System.Collections.Generic;
using System.Linq;
using Runtime;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGrabRenderer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private PlayerGrab leftHand;
    [SerializeField] private PlayerGrab rightHand;
    [SerializeField] private Image leftHandImage;
    [SerializeField] private Image rightHandImage;
    private List<PlayerInput> _players;
    private List<PlayerGrab> _playerGrabs;

    private void Awake()
    {
        _players = PlayerManager.Instance.PlayerInputs;
        foreach (var player in _players)
        {
            if (player.playerIndex != playerIndex) continue;
            _playerGrabs = player.GetComponentsInChildren<PlayerGrab>().ToList();
        }
        
        if (_playerGrabs == null) return;
        
        AssignHands();
        
        leftHand.onGrabChanged.AddListener(UpdateVisual);
        rightHand.onGrabChanged.AddListener(UpdateVisual);
        
        UpdateVisual();
    }

    private void AssignHands()
    {
        foreach (var playerGrab in _playerGrabs)
        {
            if (playerGrab.HandType == HandType.Left)
            {
                leftHand = playerGrab;
                continue;
            }

            rightHand = playerGrab;
        }
    }

    private void UpdateVisual()
    {
        Grabbable leftHandGrabbable = leftHand.IsGrabbingObject ? leftHand.GrabbedGrabbable : null;
        Grabbable rightHandGrabbable = rightHand.IsGrabbingObject ? rightHand.GrabbedGrabbable : null;

        leftHandImage.enabled = false;
        rightHandImage.enabled = false;
        
        if (leftHandGrabbable != null)
        {
            leftHandImage.sprite = leftHandGrabbable.Icon != null ? leftHandGrabbable.Icon : null;
            leftHandImage.enabled = leftHandImage.sprite != null;
        }

        if (rightHandGrabbable != null)
        {
            rightHandImage.sprite = rightHandGrabbable.Icon != null ? rightHandGrabbable.Icon : null;
            rightHandImage.enabled = rightHandImage.sprite != null;
        }
    }
}
