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
    [SerializeField] private Image leftHandImage;
    [SerializeField] private Image rightHandImage;
    private PlayerGrab _leftHand;
    private PlayerGrab _rightHand;
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
        
        if (_playerGrabs == null)
        {
            Debug.LogWarning("Player grabs have not been found");
            return;
        };
        
        AssignHands();
        
        _leftHand.onGrabChanged.AddListener(UpdateVisual);
        _rightHand.onGrabChanged.AddListener(UpdateVisual);
        
        UpdateVisual();
    }

    private void AssignHands()
    {
        foreach (var playerGrab in _playerGrabs)
        {
            if (playerGrab.HandType == HandType.Left)
            {
                _leftHand = playerGrab;
                continue;
            }

            _rightHand = playerGrab;
        }
    }

    private void UpdateVisual()
    {
        Grabbable leftHandGrabbable = _leftHand.IsGrabbingObject ? _leftHand.GrabbedGrabbable : null;
        Grabbable rightHandGrabbable = _rightHand.IsGrabbingObject ? _rightHand.GrabbedGrabbable : null;

        leftHandImage.enabled = false;
        rightHandImage.enabled = false;
        
        if (leftHandGrabbable != null)
        {
            leftHandImage.sprite = leftHandGrabbable.Icon;
            leftHandImage.enabled = leftHandImage.sprite != null;
        }

        if (rightHandGrabbable != null)
        {
            rightHandImage.sprite = rightHandGrabbable.Icon;
            rightHandImage.enabled = rightHandImage.sprite != null;
        }
    }
}
