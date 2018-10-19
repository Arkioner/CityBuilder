using System;
using UnityEngine;

namespace CityBuilder.Scripts.Infrastructure
{
    public class UnityBuilding : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private int _price;
        [SerializeField] private String _name;

        public int Id
        {
            get { return _id; }
        }

        public int Price
        {
            get { return _price; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}