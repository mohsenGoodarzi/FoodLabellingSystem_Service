
import GroupDialog from "./GroupDialog.js";

var groupElement = document.querySelector(".group");
var groupTextBox = document.getElementById("IngredientTypeId");
var group = new GroupDialog(groupElement, groupTextBox);

function initSelectGroupDialog() {

    //fetchs the daata if the data has not been fetched

    if (group.items.length === 0) {
        let query = "/api/IngredientType/SubItems/Ingredients";
        fetch(query).then(
            async response => {

                let ingredients = await response.json();
                group.addPath("Ingredients");
                group.initItems(ingredients);
            });
    }
}

function initCreatePage() {

    // pops up the modal window

    groupTextBox.addEventListener("click", event => {
       // user may select an item and does not press save change button
        // Group class need to find the saved change
            group.initSelectedItem();
        groupTextBox.setAttribute("data-toggle", "modal");
    }, false);
    
    // prevents end-user from typing in
    groupTextBox.addEventListener("keydown", event => {
        event.preventDefault();

    }, false);

}

initSelectGroupDialog();
initCreatePage();
