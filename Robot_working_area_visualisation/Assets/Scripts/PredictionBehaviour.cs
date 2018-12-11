/// <summary>
/// Filename: PredictionBehaviour.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///     This script handles the forecast of the robots mouvement.
/// </summary>
using UnityEngine;
using System.Collections.Generic;

public class PredictionBehaviour : MonoBehaviour {

    [Space(10)]
    [Header("Position Helper")]
    public List<GameObject> listPositionHelper = new List<GameObject>(7);

    [Space(10)]
    [Header("Parameters")]
    public int vectorModifier = 10;
    public int predictionDuration = 100;
    public Material rayCastMaterial;
    public Material baseMaterial;

    private int numberOfHelper;
    private Vector3[] positionHelperOldArray;
    private List<RaycastHitBox> hitList;
    private int frameCounter;

    private void Awake(){

        numberOfHelper = listPositionHelper.Count;
        positionHelperOldArray = new Vector3[numberOfHelper];
        AssignPositionHelperOldValue();
        hitList = new List<RaycastHitBox>();
        frameCounter = 0;
    }
	
	// Update is called once per frame
	void LateUpdate () {

        CleanUnusedBoxes();
        frameCounter++;
    }
    
    // Clear boxes which are not expected to be visited anymore by the robot.
    private void CleanUnusedBoxes()
    {
        // Loop through a list where all collided game objects are
        foreach(RaycastHitBox item in hitList.ToArray())
        {
            // Based on a duration check
            if (item.frame < (frameCounter - predictionDuration))
            {
                item.hit.collider.GetComponent<Renderer>().material = baseMaterial;
                hitList.Remove(item);
            }
        }
    }

    public void DrawDebugRay()
    {

        for (int i = 0; i < numberOfHelper; i++)
        {
            Debug.DrawLine(listPositionHelper[i].transform.position,
                listPositionHelper[i].transform.position +
                (listPositionHelper[i].transform.position - positionHelperOldArray[i]) * vectorModifier,
                Color.red);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(listPositionHelper[i].transform.position,
                                       listPositionHelper[i].transform.position +
                                       (listPositionHelper[i].transform.position - positionHelperOldArray[i]) * vectorModifier,
                                      100.0f);

            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j].collider.gameObject.CompareTag("Box"))
                {
                    hitList.Add(new RaycastHitBox(hits[j], frameCounter));
                    hits[j].collider.GetComponent<Renderer>().material = rayCastMaterial;
                }
                   
            }
        }

        //if (frameCounter % 3 == 0)
        AssignPositionHelperOldValue();
    }
    
    // Assign old position to an array to be used to calculate the "speed"
    public void AssignPositionHelperOldValue()
    {
        for (int i = 0; i < numberOfHelper; i++)
        {
            positionHelperOldArray[i] = listPositionHelper[i].transform.position;
        }
    }


}

public struct RaycastHitBox
{
    public RaycastHit hit { get; set; }
    public int frame { get; set; }

    public RaycastHitBox(RaycastHit h, int f){

        hit = h;
        frame = f;
    }
}

