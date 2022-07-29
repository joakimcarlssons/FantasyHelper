// #region Local Storage

    // Save data to local storage
    function SaveToLocalStorage(key, value) {
        localStorage.setItem(key, value);
    }

    // Load data from local storage
function LoadFromLocalStorage(key) {
    try {
        return localStorage.getItem(key);
    }
    catch {
    }
}

    // Remove data from local storage
    function RemoveFromLocalStorage(key) {
        localStorage.removeItem(key);
    }

// #endregion