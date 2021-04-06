using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationInstance : MonoBehaviour
{
    public Image displayImage;
    public Text stationName;

    public Stations station;

    public Text travelText;
    public Image artwork;
    public SceneTransitionScript scene;

    private void Start()
    {
        if (station != null)
        {
            displayImage.sprite = station.artwork;
            stationName.text = station.name;
        }
    }

    public void OnSelect()
    {
        travelText = GameObject.Find("TravelText").GetComponent<Text>();
        artwork = GameObject.Find("Artwork").GetComponent<Image>();
        scene = GameObject.FindObjectOfType<SceneTransitionScript>();

        travelText.text = station.name;
        artwork.sprite = station.artwork;
        scene.sceneIndex = station.sceneIndex;
        
        
    }
}
