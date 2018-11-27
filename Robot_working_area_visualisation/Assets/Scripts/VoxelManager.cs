/**
*   Filename: VoxelManager.cs
*   Author: Flückiger Quentin
*   
*   Description:
*       This script handle the text change of a slider.
*   
**/
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

    private void SetUpBoxes() {
        
        sizeOfBox = widthLength / nbrOfBoxesOneSide;

        CalculateHeight(sizeOfBox);

        boxToInstantiate = ScaleBox(sizeOfBox, box);
        CalculateStartPosition();
        position = new Vector3(posX, posY, posZ);
    }

    private void InstantiateBoxes(int nbrOfBoxesOneSide, GameObject boxToInstatiate, Vector3 position) {

        DestroyPreviousBoxes();

        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < nbrOfBoxesOneSide; z++)
            {
                for (int x = 0; x < nbrOfBoxesOneSide; x++)
                {
                    Instantiate(boxToInstatiate, position, rotation, boxParent.transform);
                    position += new Vector3(sizeOfBox, 0, 0);
                }
                position += new Vector3(-10f, 0, sizeOfBox);
            }
            position += new Vector3(0, sizeOfBox, -10f);
        }
       
        
    }

    private GameObject ScaleBox(float sizeOfBox, GameObject box) {

        box.transform.localScale = new Vector3(sizeOfBox, sizeOfBox, sizeOfBox);
        return box;
    }

    private void CalculateStartPosition() {

        float offset = sizeOfBox / 2;
        float startPos = -(widthLength / 2);
        posX = startPos + offset;
        posY = offset;
        posZ = startPos + offset;
    }

    private void DestroyPreviousBoxes() {

        foreach (Transform child in boxParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

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

    private void CreateLayout() {

        SetUpBoxes();
        InstantiateBoxes(nbrOfBoxesOneSide, boxToInstantiate, position);
    }
    
    public void ChangeVoxelWorld(Slider nbrOfBoxesSlider) {

        nbrOfBoxesOneSide = (int)nbrOfBoxesSlider.value;
        CreateLayout();
    }
}
