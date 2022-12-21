using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPos;
    Touch touch;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(player.transform.position, tempFingerPos) < 1f)
            {
                CreateLine();
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(tempFingerPos, fingerPos[fingerPos.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
        else
        {
            Moving();
            Remove();
        }


        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(Vector2.Distance(player.transform.position, tempFingerPos) < 1f)
                {
                    CreateLine();
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {

                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (Vector2.Distance(tempFingerPos, fingerPos[fingerPos.Count -1]) > .1f)
                {

                    UpdateLine(tempFingerPos);

                }

            }
            else
            {
                Moving();
                Remove();
            }

        }

    }

    private void CreateLine()
    {
 
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLine.gameObject.tag = "cloneLine";
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPos.Clear();
        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(touch.position));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(touch.position));
        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);
        edgeCollider.points = fingerPos.ToArray();

    }

    private void UpdateLine(Vector2 newFingerPos)
    {

        fingerPos.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPos.ToArray();
    }

    private void Moving()
    {
        if (fingerPos.Count > 0)
        {
            player.transform.position = fingerPos[0]; //move the player
            fingerPos.RemoveAt(0);
        }
    }

    private void Remove()
    {
        if (fingerPos.Count == 0)
            Destroy(currentLine);
    }
}