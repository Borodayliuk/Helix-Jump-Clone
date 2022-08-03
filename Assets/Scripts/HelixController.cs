using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    [SerializeField] private float _sensibility;
    private Vector2 _lastTapPosition;
    private Vector3 _startRotation;

    public Transform topTransform;
    public Transform GoalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    private float _helixDistance;
    private List<GameObject> _spawnedLevels = new List<GameObject>();

    

    private void Awake()
    {
       
        Instantiate(helixLevelPrefab);
        _startRotation = transform.localEulerAngles;
        _helixDistance = topTransform.localPosition.y - (GoalTransform.localPosition.y + 0.1f);
        LoadStage(0);
       
    }

    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition; 
            if (_lastTapPosition == Vector2.zero)
            {
                _lastTapPosition = curTapPos;
            }
            float delta = _lastTapPosition.x - curTapPos.x;
            _lastTapPosition = curTapPos;
            RotationHelix(delta);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _lastTapPosition = Vector2.zero;
        }
    }
    public void RotationHelix(float delta)
    {
        transform.Rotate(Vector3.up * delta * _sensibility);
    }
    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];
        if (stage == null)
        {
            Debug.LogError("No stage " + stageNumber);
            return;
        }
        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
        FindObjectOfType<PlayerController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallPartColor;

        transform.localEulerAngles = _startRotation;

        foreach (GameObject gameObject in _spawnedLevels)
            Destroy(gameObject);

        float levelDistance = _helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            _spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.levels[i].partCount;

            List<GameObject> disableParts = new List<GameObject>();

            while (disableParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disableParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disableParts.Add(randomPart);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }
            List<GameObject> deathParts = new List<GameObject>();

            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];
                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }

        }
    }
}
