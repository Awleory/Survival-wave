using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;

    private Player<CharacterHealth> _player;

    private void Awake()
    {
        _player = new Player<CharacterHealth>(new CharacterHealth());
        _playerPresenter.Initialize(_player);

        var character = new Character<CharacterHealth>(new CharacterHealth());
        var entity = new Entity<Health>(new Health());

        Entity<Health> arch = character;
        
    }
}
