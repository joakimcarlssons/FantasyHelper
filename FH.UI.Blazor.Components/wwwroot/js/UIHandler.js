// Clears selection and put the focus on after the last letter in the element
// Used for example when we want to edit on double click
function clearSelection(element, index) {

    var sel = document.getSelection();
    sel.removeAllRanges();

    let inputField = document.querySelectorAll(element)[index];
    inputField.selectionStart = inputField.value.length;
    inputField.focus();
}