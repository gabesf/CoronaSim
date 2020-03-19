using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoundaries : MonoBehaviour
{
    public GameObject wallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CreateHorizontalWalls();
        CreateVerticalWalls();
    }

    private void CreateVerticalWalls()
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.name = "RightWall";
        wall.transform.position = new Vector2(Constants.WorldSize.x, 0);
        wall.transform.parent = transform;
        wall.transform.localScale = new Vector3(10, 450, 0);
        wall = Instantiate(wallPrefab);
        wall.name = "LeftWall";
        wall.transform.position = new Vector2(-Constants.WorldSize.x, 0);
        wall.transform.parent = transform;
        wall.transform.localScale = new Vector3(10, 450, 0);

    }

    private void CreateHorizontalWalls()
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.name = "TopWall";
        wall.transform.position = new Vector2(0, Constants.WorldSize.y);
        wall.transform.parent = transform;
        wall.transform.localScale = new Vector3(800, 10, 0);

        wall = Instantiate(wallPrefab);
        wall.name = "BottomWall";
        wall.transform.position = new Vector2(0, -Constants.WorldSize.y);
        wall.transform.parent = transform;
        wall.transform.localScale = new Vector3(800, 10, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
