using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAudioHandler : MonoBehaviour
{

    public static MultiAudioHandler sceneInstance;
    private List<ListeningPoint> ListeningPoints;

    // Start is called before the first frame update
    void Start()
    {
        if(sceneInstance != null)
        {
            Debug.LogWarning("There is already a MultiAudioHandler in the Scene! Deleting this one");
            Destroy(this);
        }
        else
        {
            sceneInstance = this;
            ListeningPoints = new List<ListeningPoint>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ListeningPoint getClosestListeningPoint(Vector3 pos)
    {
        if(ListeningPoints.Count > 0)
        {
            int bigIndex = 0;
            float distance = Vector3.Distance(ListeningPoints[0].transform.position, pos);
            for(int i = 1; i < ListeningPoints.Count; i++)
            {
                float newDist = Vector3.Distance(ListeningPoints[1].transform.position, pos);
                if(newDist < distance)
                {
                    bigIndex = i;
                    distance = newDist;
                }
            }
            return ListeningPoints[bigIndex];
        }
        return null;
    }


    public void addPoint(ListeningPoint point)
    {
        if(!ListeningPoints.Contains(point)) ListeningPoints.Add(point);
    }

    public void removePoint(ListeningPoint point)
    {
        if (ListeningPoints.Contains(point)) ListeningPoints.Remove(point);
    }


}
