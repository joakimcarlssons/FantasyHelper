// #region Local Storage

    // Save data to local storage
    function SaveToLocalStorage(key, value) {
        localStorage.setItem(key, value);
    }

    // Load data from local storage
    function LoadFromLocalStorage(key) {
        return localStorage.getItem(key);
    }

    // Remove data from local storage
    function RemoveFromLocalStorage(key) {
        localStorage.removeItem(key);
    }

// #endregion