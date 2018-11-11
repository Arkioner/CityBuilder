using System;
using System.Collections.Generic;

namespace CityBuilder.Scripts.Domain
{
    public class City
    {
        private int _cash;
        private int _income;
        private int _currentDay;
        private int _populationCurrent;
        private int _populationCeiling;
        private int _jobsCurrent;
        private int _jobsCeiling;
        private int _food;

        private int _basePopulation;
        private int _baseJobs;
        private int _baseFood;
        private int _baseCash;

        private const int ROAD = 0;
        private const int HOUSE = 1;
        private const int FARM = 2;
        private const int FACTORY = 3;

        private BuildingBoard _buildingBoard;

        public City(int basePopulation, int baseJobs, int baseFood, int baseCash, int boardSize)
        {
            _basePopulation = basePopulation;
            _baseJobs = baseJobs;
            _baseFood = baseFood;
            _baseCash = baseCash;
            _currentDay = 0;
            _income = 0;
            _cash = baseCash;
            _populationCurrent = basePopulation;
            _populationCeiling = basePopulation;
            _jobsCurrent = baseJobs;
            _jobsCeiling = baseJobs;
            _food = baseFood;
            _buildingBoard = new BuildingBoard(boardSize);
        }

        public void RunEndTurn()
        {
            CalculateFood();
            CalculateJobs();
            CalculatePopulation();
            CalculateCash();
            CalculateIncome();
        }

        public void UpdateInfo()
        {
            CalculateFood();
            CalculatePopulation();
            CalculateJobs();
            CalculateIncome();
        }


        private void CalculateCash()
        {
            _cash += _income;
        }
        
        private void CalculateIncome()
        {
            _income = _jobsCurrent * 3;
        }

        private void CalculateFood()
        {
            _food = _baseFood + _buildingBoard.GetBuildingsOfType(BuildingType.Farm) * 4;
        }

        private void CalculateJobs()
        {
            _jobsCeiling = _baseJobs + _buildingBoard.GetBuildingsOfType(BuildingType.Factory) * 10;
            _jobsCurrent = Math.Min(_populationCurrent, _jobsCeiling);
        }

        private void CalculatePopulation()
        {
            _populationCeiling = _basePopulation + _buildingBoard.GetBuildingsOfType(BuildingType.House) * 5;
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

        public void Build(Building building, Coordinates position)
        {
            if (_cash >= building.Price)
            {
                if (_buildingBoard.AddBuildingToPosition(building, position))
                {
                    _cash -= building.Price;
                }
            }
        }

        public void Demolish(Building building, Coordinates position)
        {
            if (_buildingBoard.RemoveBuildingFromPosition(building, position))
            {
                _cash += building.Price;
            }
        }


        private class BuildingBoard
        {
            private Dictionary<BuildingType, int> _buildingsByType = new Dictionary<BuildingType, int>();
            private Building[,] _buildingsBoard;

            public BuildingBoard(int boardSize)
            {
                _buildingsBoard = new Building[boardSize, boardSize];
            }

            public bool AddBuildingToPosition(Building building, Coordinates position)
            {
                if (IsPositionClearForBuilding(position))
                {
                    _buildingsBoard[position.X, position.Y] = building;
                    UpdateBuildingTypeCounter(building.Type, +1);
                    return true;
                }

                return false;
            }

            public bool RemoveBuildingFromPosition(Building building, Coordinates position)
            {
                Building buildingAtPosition = _buildingsBoard[position.X, position.Y];
                if (null != buildingAtPosition && buildingAtPosition.Id == building.Id)
                {
                    _buildingsBoard[position.X, position.Y] = null;
                    UpdateBuildingTypeCounter(building.Type, -1);
                    return true;
                }

                return false;
            }

            private bool IsPositionClearForBuilding(Coordinates position)
            {
                return _buildingsBoard[position.X, position.Y] == null;
            }

            private void UpdateBuildingTypeCounter(BuildingType buildingType, int value)
            {
                int counter = GetBuildingsOfType(buildingType);
                counter += value;
                _buildingsByType.Add(buildingType, counter);
            }

            public int GetBuildingsOfType(BuildingType buildingType)
            {
                int counter;
                _buildingsByType.TryGetValue(buildingType, out counter);
                return counter;
            }
        }
    }
}