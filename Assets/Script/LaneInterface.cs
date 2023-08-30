using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaneInterface : MonoBehaviour
{
    [SerializeField]
    private TMP_Text cardCount;

    public Lane Lane { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Lane = new Lane();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
