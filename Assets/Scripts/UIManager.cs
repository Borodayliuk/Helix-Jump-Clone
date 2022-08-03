using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _bestScore;

    private void Update()
    {
        _score.GetComponent<TextMeshProUGUI>().text = "Score: " + GameManager.singleton.score;
        _bestScore.GetComponent<TextMeshProUGUI>().text = "Best: " + GameManager.singleton.best;
    }
}
