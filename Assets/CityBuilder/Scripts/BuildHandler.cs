using UnityEngine;

namespace CityBuilder.Scripts
{
    public class BuildHandler : MonoBehaviour
    {
        public int BoardSize;
        private int _offset = 1;
        private Building _buildingModeEnabled;

        public void AddBuilding(Building building, Vector3Int position)
        {
            Vector3Int buildingToAddPosition = CalculateGridPosition(position);
            Building instance = Instantiate(building, buildingToAddPosition, Quaternion.identity);
            instance.transform.parent = transform;
        }

        private Vector3Int CalculateGridPosition(Vector3Int position)
        {
            return new Vector3Int(position.x + _offset, 0, position.z + _offset);
        }

        public void EnableBuildingMode(Building building)
        {
            _buildingModeEnabled = building;
        }
    }
}