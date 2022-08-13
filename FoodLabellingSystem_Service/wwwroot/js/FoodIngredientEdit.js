
import GroupDialog from "./GroupDialog.js";

var groupElement = document.querySelector(".group");
var groupTextBox = document.getElementById("groupTextBox");
var group = new GroupDialog(groupElement, groupTextBox);

function initSelectGroupDialog() {
    let groupItems = document.getElementsByClassName("group-body")[0];
    if (groupItems.childElementCount === 0) {
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
        group.initSelectedItem();
        groupTextBox.setAttribute("data-toggle", "modal");
    }, false);

    let selectIngredients = document.getElementById("IngredientId");

    // gets the sub-items

    group.saveButton.addEventListener("click", event => {

        let query = "/api/IngredientType/Ingredients/" + groupTextBox.value;

        fetch(query).then(
            async response => {

                let ingredients = await response.json();

                selectIngredients.innerHTML = "";
                ingredients.forEach(item => {
                    let option = document.createElement("option");
                    option.innerText = item.ingredientId;
                    selectIngredients.appendChild(option);
                });

            }
        );

    }, false);
    // prevents end-user from typing in
    groupTextBox.addEventListener("keydown", event => {
        event.preventDefault();

    }, false);

}

initSelectGroupDialog();
initCreatePage();
