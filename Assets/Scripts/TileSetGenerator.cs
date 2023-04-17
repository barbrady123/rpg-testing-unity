using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSetGenerator : MonoBehaviour
{
    public GameObject GridPrefab;
    public GameObject HolePrefab;

    private GameObject _grid;

    private int gridSize = 100;

    private string _tilePath = "Palettes\\Dungeon";

    // Start is called before the first frame update
    void Start()
    {
        var floorTile = Resources.Load<TileBase>($"{_tilePath}\\cave_Padded_0");
        var wallTile = Resources.Load<TileBase>($"{_tilePath}\\cave_Padded_98");

        _grid = Instantiate(GridPrefab, transform);
        
        var groundTileMap = _grid.GetComponentsInChildren<Tilemap>().FirstOrDefault(x => x.name == "Ground");
        var objectsTileMap = _grid.GetComponentsInChildren<Tilemap>().FirstOrDefault(x => x.name == "Objects");
        var interactTilemap = _grid.GetComponentsInChildren<Tilemap>().FirstOrDefault(x => x.name == "Interact");

        for (int y = -gridSize; y <= gridSize;  y++)
        {
            for (int x = -gridSize; x <= gridSize; x++)
            {
                groundTileMap.SetTile(new Vector3Int(x, y), floorTile);
            }
        }

        for (int y = -gridSize; y <= gridSize;  y++)
        {
            for (int x = -gridSize; x <= gridSize; x++)
            {
                var wallPos = new Vector3Int(x, y);

                if ((Random.Range(0, 100) < 10) && (wallPos != Vector3Int.zero))
                {
                    objectsTileMap.SetTile(wallPos, wallTile);
                }
            }
        }

        bool setHole = false;

        while (!setHole)
        {
            var holePos = new Vector3Int(Random.Range(-gridSize, gridSize + 1), Random.Range(-gridSize, gridSize + 1));
            if (holePos == Vector3Int.zero)
                continue;

            if (objectsTileMap.GetTile(holePos) != null)
                continue;

            Instantiate(HolePrefab, holePos, Quaternion.identity);

            setHole = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
