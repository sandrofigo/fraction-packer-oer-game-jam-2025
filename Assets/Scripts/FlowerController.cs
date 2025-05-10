using UnityEngine;

public class FlowerController : MonoBehaviour
{
    [SerializeField] private GameObject[] _flowers;

    [SerializeField] private float _enableProbability = 0.5f;

    private void Awake()
    {
        foreach (GameObject flower in _flowers)
        {
            flower.SetActive(Random.value < _enableProbability);
        }
    }
}