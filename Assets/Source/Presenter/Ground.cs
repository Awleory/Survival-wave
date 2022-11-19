using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Ground : MonoBehaviour
{
    private Movement _playerMovement;
    private RawImage _rawImage;

    public void Initialize(Movement playerMovement)
    {
        _rawImage = GetComponent<RawImage>();
        _playerMovement = playerMovement;

        enabled = true;
    }

    private void OnEnable()
    {
        _playerMovement.Moved += OnCharacterMoved;
    }

    private void OnDisable()
    {
        _playerMovement.Moved -= OnCharacterMoved;
    }

    private void OnCharacterMoved()
    {
        _rawImage.uvRect = new Rect(_playerMovement.Position.x, _playerMovement.Position.y, _rawImage.uvRect.width, _rawImage.uvRect.height);
    }
}