
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [SerializeField] private float _baseExperience;
    [SerializeField] private float _experienceRatePerLevel;

    public float BaseExperience => _baseExperience;
    public float ExperienceRatePerLevel => _experienceRatePerLevel;
}
