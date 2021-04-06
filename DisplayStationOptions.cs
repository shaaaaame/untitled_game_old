using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class DisplayStationOptions : MonoBehaviour
{
    public Queue<Stations> stationQueue;
    public Stations[] availableStations;

    [SerializeField] RectTransform rect;

    public GameObject instance;
    private bool firstObject = true;

    private void Awake()
    {
        GameObject temp;

        foreach (Stations station in availableStations){

            temp = Instantiate(instance, this.transform, false) as GameObject;
            temp.GetComponent<StationInstance>().station = station;
            if (firstObject)
            {
                temp.GetComponent<StationInstance>().OnSelect();
                firstObject = false;
            }
            
        }

        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y * availableStations.Length);
    }

    //unlocks next station
    void UnlockStation()
    {
        availableStations.Append(stationQueue.Dequeue());
    }

    

}
