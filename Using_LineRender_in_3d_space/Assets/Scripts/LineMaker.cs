using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    public Transform startPos;
    public Vector3 prevStartPos;
    public Transform endPos;
    public Vector3 prevEndPos;
    
    public LineRenderer lineRenderer;

    public int segmentCount;
    public float segmentLength;

    public bool isReDraw;
    
    [SerializeField]
    public List<Segment> segments = new List<Segment>();
    
    // Start is called before the first frame update
    void Start()
    {
        DivideIntoSegment();
        DrawSegmentLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (prevEndPos != endPos.position||prevStartPos != startPos.position)
        {
            isReDraw = true;
            DivideIntoSegment();
            DrawSegmentLine();
        }
        
    }

    public void DivideIntoSegment()
    {
        Vector3 subtraction = endPos.position - startPos.position;
        prevEndPos = endPos.position;
        prevStartPos = startPos.position;
        
        //Get distance and direction from endpoint to startPoint.
        float distance = subtraction.magnitude;
        Vector3 dir = subtraction.normalized;
        
        //Get each of segments Length.
        segmentLength = distance / segmentCount;
        Vector3 segmentPos = startPos.position;
        
        
        if (isReDraw)
        {
            segments.Clear();
            isReDraw = false;
        }
        
        //each of segments has vertex for drawing line.
        for (int i = 0; i < segmentCount+1; i++)
        {
            segments.Add(new Segment(segmentPos));
            segmentPos += new Vector3(segmentLength*dir.x,segmentLength*dir.y,segmentLength*dir.z);
        }
    }

    public void DrawSegmentLine()
    {
        Vector3[] segmentPositions = new Vector3[segments.Count];
      
        for (int i = 0; i < segments.Count; i++)
        {
            segmentPositions[i] = segments[i].pos;
        }
        
        //Draw Line.
        lineRenderer.positionCount = segmentPositions.Length;
        lineRenderer.SetPositions(segmentPositions);
    }
}
