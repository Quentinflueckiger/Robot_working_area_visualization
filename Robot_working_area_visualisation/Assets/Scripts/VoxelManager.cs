/// <summary>
/// Filename: VoxelManager.cs
/// Author: Flückiger Quentin
/// 
/// Description:
///     This script handle setup and the changes of the voxel world.
/// </summary>

using System;
using UnityEngine;
using UnityEngine.UI;

public class VoxelManager : MonoBehaviour {

    public GameObject box;
    public int nbrOfBoxesOneSide;
    public GameObject boxParent;

    private float widthLength = 10;
    private int height = 3;
    private float sizeOfBox;
    private GameObject boxToInstantiate;
    private float posX;
    private float posY;
    private float posZ;
    private Quaternion rotation = new Quaternion();
    private Vector3 position;


	void Awake () {

        CreateLayout();
    }

    // Prepare the boxes, their size, number of them vertically and first position.
    private void SetUpBoxes() {

        sizeOfBox = getSizeOfBoxes(); 

        CalculateHeight(sizeOfBox);
        
        CalculateStartPosition();
        position = new Vector3(posX, posY, posZ);
    }

    /// <summary>
    /// This return the computed value of the size of the boxes.
    /// Which is calculated based on the width of the bse square and the number of boxes per side wanted.
    /// </summary>
    ///<returns>the computed size of the boxes</returns>
    private float getSizeOfBoxes(){

        return widthLength / nbrOfBoxesOneSide;
    }
    
    /// <summary>
    /// Clear the world and instantiate new box to fill it.
    /// </summary>
    /// <param name="nbrOfBoxesOneSide">the number of box per side</param>
    /// <param name="boxToInstatiate">the game object to instantiate</param>
    /// <param name="position">the position where to instantiate it</param>
    private void InstantiateBoxes(int nbrOfBoxesOneSide, GameObject boxToInstatiate, Vector3 position) {

        DestroyPreviousBoxes();

        // Instantiate the boxes with a triple for, one for each dimension.
        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < nbrOfBoxesOneSide; z++)
            {
                // Instantiate one box after the other
                for (int x = 0; x < nbrOfBoxesOneSide; x++)
                {
                
                    boxToInstantiate = Instantiate(box, position, rotation, boxParent.transform);
                    boxToInstantiate.transform.localScale = GetSizeOfBox();

                    // The next position to instantiate a box
                    position += new Vector3(sizeOfBox, 0, 0);
                }
                // One row after the other
                position += new Vector3(-widthLength, 0, sizeOfBox);
            }
            // One floar after the other
            position += new Vector3(0, sizeOfBox, -widthLength);
        }   
    }

    /// <summary>
    /// Return the current size of the box
    /// </summary>
    /// <returns>a Vector3 with the information on the scaling of the box</returns>
    private Vector3 GetSizeOfBox() {

       return new Vector3(sizeOfBox, sizeOfBox, sizeOfBox);
    }

    // Calculates the position where the first box should be placed.
    private void CalculateStartPosition() {

        float offset = sizeOfBox / 2;
        float startPos = -(widthLength / 2);

        posX = startPos + offset;
        posY = offset;
        posZ = startPos + offset;
    }

    // Clean the whole voxel world from any boxes
    private void DestroyPreviousBoxes() {

        foreach (Transform child in boxParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    ///<summary>
    /// Calculates the vertical number of boxes to be created.
    /// </summary>
    /// <param name="size">the size of the box</param>
    private void CalculateHeight(float size)
    {

        if (size < 1f && size > 0.6f)
            height = 4;
        else if (size <= 0.6f && size > 0.4f)
            height = 5;
        else if (size <= 0.4f)
            height = 6;
        else if (sizeOfBox > 2f)
            height = 2;
        else
            height = 3;
    }

    // Calls multiple method in order to create the voxel world layout.
    private void CreateLayout() {

        SetUpBoxes();
        InstantiateBoxes(nbrOfBoxesOneSide, boxToInstantiate, position);
    }
    
    // Recreates the voxel world with another number of boxes per side
    public void ChangeVoxelWorld(Slider nbrOfBoxesSlider) {

        nbrOfBoxesOneSide = (int)nbrOfBoxesSlider.value;
        CreateLayout();
    }
}
