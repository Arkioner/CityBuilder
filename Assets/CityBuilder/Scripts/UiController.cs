using TMPro;
using UnityEngine;

namespace CityBuilder.Scripts
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameInfoText;

        [SerializeField] private TextMeshProUGUI _dayText;

        public void UpdateDayCount(int currentDay)
        {
            _dayText.SetText(string.Format("Day {0}", currentDay));
        }

        public void UpdateGameInfo(int cash, int income, int populationCurrent,
            int populationCeiling, int food, int jobsCurrent, int jobsCeiling)
        {
            _gameInfoText.SetText(
                string.Format(
                    "Cash: {0}€ (+{1})\nPopulation: {2}/{3}\nFood; {4}\nJobs: {5}/{6}",
                    cash,
                    income,
                    populationCurrent, 
                    populationCeiling, 
                    food,
                    jobsCurrent, 
                    jobsCeiling
                )
            );
        }
    }
}