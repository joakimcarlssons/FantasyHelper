﻿namespace FH.UI.Blazor
{
    /// <summary>
    /// Container for state data within the application such as type of fantasy game etc
    /// </summary>
    public class StateContainer
    {
        /// <summary>
        /// Indicator if data is loading and if the page loader should be displayed
        /// </summary>
        private bool dataIsLoading;
        public bool DataIsLoading
        {
            get => dataIsLoading;
            set
            {
                dataIsLoading = value;
                NotifyStateChanged();
            }
        }


        /// <summary>
        /// Indicator of the selected fantasy game
        /// </summary>
        private string? selectedFantasyGame;
        public string SelectedFantasyGame
        {
            get => selectedFantasyGame ?? "FPL";
            set
            {
                if (selectedFantasyGame == value) return;
                selectedFantasyGame = value;
                NotifyStateChanged();
            }
        }



        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}