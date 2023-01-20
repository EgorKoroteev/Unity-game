using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public Room[] RoomPrefabs;
    public Room StartingRoom;
    public int countRooms;
    private Room[,] spawnedRooms;
    private void Start()
    {
        spawnedRooms = new Room[12, 12];
        spawnedRooms[2, 2] = StartingRoom;
        for (int i = 0; i < countRooms; i++)
        {
            Generate();
        }
    }
    private void Generate()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;
                int maxX = spawnedRooms.GetLength(0);
                int maxY = spawnedRooms.GetLength(1);
                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }
        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
        int limit = 500;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (ConnectDoors(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 2, position.y - 2, 0) * 3.6f;
                spawnedRooms[position.x, position.y] = newRoom;
                break;
            }
        }
    }
    private bool ConnectDoors(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0);
        int maxY = spawnedRooms.GetLength(1);
        List<Vector2Int> neighbors = new List<Vector2Int>();
        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbors.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbors.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorD != null) neighbors.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorU != null) neighbors.Add(Vector2Int.left);
        if (neighbors.Count == 0) return false;
        Vector2Int selectedDirection = neighbors[Random.Range(0, neighbors.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];
        if (selectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(false);
            selectedRoom.DoorD.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(false);
            selectedRoom.DoorU.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);
        }
        return true;
    }
}