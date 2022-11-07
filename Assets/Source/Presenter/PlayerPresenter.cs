
using UnityEngine;

public class PlayerPresenter : CharacterPresenter<Player>
{
    [SerializeField] private PlayerInfoUI _playerInfoUI;

    public override void Initialize(Player model)
    {
        base.Initialize(model);

        _playerInfoUI.Initialize(model.Stats);
    }
}
