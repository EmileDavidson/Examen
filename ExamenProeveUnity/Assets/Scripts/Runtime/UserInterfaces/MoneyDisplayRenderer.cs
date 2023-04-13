using TMPro;
using UnityEngine;

namespace Runtime.UserInterfaces
{
    public class MoneyDisplayRenderer : MonoBehaviour
    {
        public TMP_Text moneyText;
        
        /// <summary>
        /// Is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            GameManager.Instance.onMoneyChange.AddListener(UpdateMoneyText);
            UpdateMoneyText();
        }
        
        /// <summary>
        /// Updates the money text with the current money
        /// </summary>
        private void UpdateMoneyText()
        {
            moneyText.text = GetMoneyString();
        }
        
        /// <summary>
        /// Get money string from GameManager
        /// is used to get the 'string' for instance 1000 could also be 1k
        /// this converts it to the correct string
        /// </summary>
        /// <returns></returns>
        private string GetMoneyString()
        {
            return GameManager.Instance.Money.ToString();
        }
    }
}