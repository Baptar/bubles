using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParentManager : MonoBehaviour
{
    public static ParentManager instance;
    
    [SerializeField] private GameObject _dad;
    [SerializeField] private GameObject _mum;

    [SerializeField] private List<GameObject> _roomPoints;
    [SerializeField] private List<String> _missionSpeeches;

    [SerializeField] private GameObject _speechBubble;
    [SerializeField] private TextMeshProUGUI _speechText;

    private Animator _dadAnimator;
    private Animator _mumAnimator;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        _dadAnimator = _dad.GetComponent<Animator>();
        _mumAnimator = _mum.GetComponent<Animator>();
    }

    public void MoveParents(int roomIndex)
    {
        GameObject roomPoint = _roomPoints[roomIndex];
        _dad.transform.position = new Vector3(roomPoint.transform.position.x - 1f, roomPoint.transform.position.y, 0f);
        _mum.transform.position = new Vector3(roomPoint.transform.position.x + 1f, roomPoint.transform.position.y, 0f);
    }

    public void ShowMissionSpeech(int missionIndex)
    {
        _speechText.text = _missionSpeeches[missionIndex];
        _speechBubble.SetActive(true);
    }

    public void HideMissionSpeech()
    {
        _speechBubble.SetActive(false);
    }
}
