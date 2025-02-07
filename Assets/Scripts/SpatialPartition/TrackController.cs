using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    private float trackSpeed;
    private Transform prevSeg;
    private GameObject trackParent;
    private Transform segParent;
    private List<GameObject> segments;
    private Stack<GameObject> segStack;
    private Vector3 currentPosition = Vector3.zero;

    [Tooltip("List of race tracks")]
    [SerializeField]
    private Track track;

    [Tooltip("Initial amount of segment to load at start")]
    [SerializeField]
    private int initSegAmount;

    [Tooltip("Amount of incremental segments to load at run")]
    [SerializeField]
    private int incrSegAmount;

    [Tooltip("Dampen the speed of the track")]
    [Range(0f, 100f)]
    [SerializeField]
    private float speedDampener;

    private void Awake()
    {
        segments = Enumerable.Reverse(track.segments).ToList();
    }

    private void InitTrack()
    {
        Destroy(trackParent);

        trackParent = Instantiate(Resources.Load("Track", typeof(GameObject))) as GameObject;

        if (trackParent) segParent = trackParent.transform.Find("Segments");

        prevSeg = null;

        segStack = new Stack<GameObject>(segments);

        LoadSegment(initSegAmount);
    }

    private void LoadSegment(int amount)
    {
        for(int i = 0; i < amount; ++i)
        {
            if(segStack.Count >0)
            {
                GameObject segment = Instantiate(segStack.Pop(), segParent.transform);

                if (!prevSeg) currentPosition.z = 0;

                if (prevSeg) currentPosition.z = prevSeg.position.z + track.segmentLength;

                segment.transform.position = currentPosition;

                Segment segmentComponent = segment.AddComponent<Segment>();

                segmentComponent.trackController = this;

                prevSeg = segment.transform;
            }
        }
    }

    public void LoadNextSegment()
    {
        LoadSegment(incrSegAmount);
    }
}
