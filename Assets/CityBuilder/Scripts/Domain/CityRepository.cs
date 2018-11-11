using System.Collections;
using System.Collections.Generic;
using CityBuilder.Scripts.Domain;
using UnityEngine;

public interface CityRepository
{
    City LoadCity();
    void StoreCity(City city);
}
