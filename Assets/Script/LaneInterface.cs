using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaneInterface : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardCount;

    [SerializeField]
    private Button plantButton;
    [SerializeField]
    private Button harvestButton;
    [SerializeField]
    private GameObject buttonPanel;

    private PointerHandler pointerHandler;

    public Lane Lane { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        pointerHandler = GetComponent<PointerHandler>();
        Lane = new Lane();
        pointerHandler.Initialize(onLeftDown: OnClick);
        plantButton.onClick.AddListener(OnPlantClicked);
        harvestButton.onClick.AddListener(OnHarvestClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(PointerEventData eventData)
    {
        buttonPanel.SetActive(true);
    }

    private void OnHarvestClicked()
    {
        buttonPanel.SetActive(false);
        EventSystem.Instance.FireEvent(new LaneHarvestEvent { Lane = Lane });
    }
    private void OnPlantClicked()
    {
        buttonPanel.SetActive(false);
        EventSystem.Instance.FireEvent(new LanePlantEvent { Lane = Lane });
    }

}
