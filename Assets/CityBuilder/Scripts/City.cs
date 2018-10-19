using System;
using UnityEngine;

namespace CityBuilder.Scripts
{
    [RequireComponent(typeof(UiController))]
    public class City : MonoBehaviour
    {
        private int _cash;
        private int _income;
        private int _currentDay;
        private int _populationCurrent;
        private int _populationCeiling;
        private int _jobsCurrent;
        private int _jobsCeiling;
        private int _food;


        [SerializeField] private int _basePopulation;
        [SerializeField] private int _baseJobs;
        [SerializeField] private int _baseFood;
        [SerializeField] private int _baseCash;


        [SerializeField] private UiController _uiController;
        [SerializeField] private BuildHandler _buildHandler;

        private const int ROAD = 0;
        private const int HOUSE = 1;
        private const int FARM = 2;
        private const int FACTORY = 3;

        private int[] _buildingsByType = new int[4];
        private Building[,] _buildingsBuild;

        private void CheckRequirements()
        {
            if (_uiController == null || _buildHandler == null)
            {
                throw new MissingComponentException("Missing dependencies.");
            }
        }

        // Use this for initialization
        void Start()
        {
            CheckRequirements();
            _buildingsBuild = new Building[_buildHandler.BoardSize, _buildHandler.BoardSize];
            _currentDay = 0;
            _cash = _baseCash;
            _populationCurrent = _basePopulation;
            _populationCeiling = _basePopulation;
            _jobsCurrent = _baseJobs;
            _jobsCeiling = _baseJobs;
            _food = _baseFood;
        }

        // Update is called once per frame
        private void Recalculate()
        {
            CalculateCash();
            CalculateFood();
            CalculateJobs();
            CalculatePopulation();
        }


        private void CalculateCash()
        {
            _income = _jobsCurrent * 3;
            _cash += _income;
        }

        private void CalculateFood()
        {
            _food += _buildingsByType[FARM] * 4;
        }

        private void CalculateJobs()
        {
            _jobsCeiling = _baseJobs + _buildingsByType[FACTORY] * 10;
            _jobsCurrent = Math.Min(_populationCurrent, _jobsCeiling);
        }

        private void CalculatePopulation()
        {
            _populationCeiling = _basePopulation + _buildingsByType[HOUSE] * 5;
            if (_food >= _populationCurrent && _populationCurrent < _populationCeiling)
            {
                _food -= _populationCurrent;
                _populationCurrent = Math.Min(_populationCurrent + _food, _populationCeiling);
            }
            else if (_food < _populationCurrent)
            {
                _populationCurrent -= _populationCurrent - _food;
            }
        }

        private void ReloadUi()
        {
            _uiController.UpdateDayCount(_currentDay);
            _uiController.UpdateGameInfo(_cash, _income, _populationCurrent, _populationCeiling,
                _food, _jobsCurrent, _jobsCeiling);
        }

        private void Build(Building building, Vector3Int position)
        {
            if (_cash >= building.Cost)
            {
                if (IsPositionClearForBuilding(position))
                {
                    _cash -= building.Cost;
                    _buildingsByType[building.Id]++;
                    _buildingsBuild[position.x, position.z] = building;
                    _buildHandler.AddBuilding(building, position);
                }
            }
        }

        public void BuildRoad(Building building)
        {
            Build(building, new Vector3Int(98, 0, 98));
        }

        private bool IsPositionClearForBuilding(Vector3Int position)
        {
            return _buildingsBuild[position.x, position.z] == null;
        }

        public void EndTurn()
        {
            _currentDay++;
            Recalculate();
            ReloadUi();
        }
    }
}