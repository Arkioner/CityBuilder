using CityBuilder.Scripts.Application;
using CityBuilder.Scripts.Domain;
using UnityEngine;

namespace CityBuilder.Scripts.Infrastructure
{
    public class UnityController
    {
        private BuildUseCase _buildUseCase;

        public UnityController(BuildUseCase buildUseCase)
        {
            _buildUseCase = buildUseCase;
        }

        public void BuildHouse(UnityBuilding building, Vector3 position)
        {
            _buildUseCase.BuildAt(
                GetBuildingFromUnityBuilding(building),
                GetCoordinatesOfVector3(position)
            );
        }

        private static Building GetBuildingFromUnityBuilding(UnityBuilding building)
        {
            return new Building(building.Id, building.Price, building.Name);
        }

        private Coordinates GetCoordinatesOfVector3(Vector3 position)
        {
            return new Coordinates(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        }
        
        
    }
}