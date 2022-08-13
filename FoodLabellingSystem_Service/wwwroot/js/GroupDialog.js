
class GroupDialog {
    #body = null;
    #bodyItems = null;
    #path = null;
    #data = null;
    #savedChange = false;
    #saveButton;
    #closeButton;
    #tempItem = null;
    #seletedElement = null;
    #clickedElement = null;

    constructor(groupHTMLElement, clickedElement) {

        this.#path = groupHTMLElement.querySelector(".group-path");
        this.#body = groupHTMLElement.querySelector(".group-body");
        this.#closeButton = document.querySelector("#close");
        this.#saveButton = document.querySelector("#save");
        this.#clickedElement = clickedElement;

        this.init();

    }
/**
* Getter
* Returns the group body rows.
* 
* @return {[HTMLLIElement]} this.#body.children
* */
    get items() {
        return this.#body.children;
    }
/**
* Getter
* Returns the Save Change button from GroupDialog.
*
* @return {HTMLButtonElement} this.#body.children
* */
    get saveButton() {
        return this.#saveButton;
    }
/**
* init()
* set the save change button event.  
*
* @return void
* */
    init() {

        this.saveButton.addEventListener("click", event => {
            this.#savedChange = true;
            // a selected item gets stored in the tempItem
            // user presses save button we store it in selectedItem.
            // otherwise, selectedItem remains null. 
            this.#seletedElement = this.#tempItem;
            this.#clickedElement.value = this.selectedItemValue;

        }, false);
    }
/**
 * Removes all the paths from the tail to head until it reaches to the given path name.
* removePathsExcept(pathName)
* @param {string} pathName
* @return void
* */


    /**
     * 
     * @param {any} pathName
     */
    removePathsExcept(pathName) {

        for (let i = this.#path.childElementCount - 1; i > 0; i--) {

            if (this.#path.children[i].innerText !== pathName) {

                this.#path.children[i].remove();

            } else {
                break;
            }

        }

    }
/**
*  Creates a path for the given path name and adds it to the paths area.
* removePathsExcept(pathName)
* @param {string} pathName
* @return void
* */
    addPath(pathName) {

        let pathItem = document.createElement("span");
        pathItem.className = "path-item";
        pathItem.innerText = pathName;
        pathItem.addEventListener("click", event => {
            let query = event.target.innerText;

            this.removePathsExcept(query);

            fetch("/api/IngredientType/SubItems/" + query).then(async response => {
                var result = null;
                result = await response.json();
                this.clear();

                this.initItems(result);
            }
            );

        }, false);
        this.#path.appendChild(pathItem);

    }

   
    /**
     * Initilises the items of the body of the GroupDialog.
     * @param {[Object]} items
     * example: let items = [{name:"Red-Meat",hasChildren:false}];
     * @return void 
     */
    initItems(items) {
        for (let index = 0; index < items.length; index++) {
            let row = this.createItem(items[index]);
            this.#body.appendChild(row);
        }
    }
    // 
    /**
     * Creates an item for the body of the GroupDialog.
     * @param {Object} item
     * example: let item = {name:"Red-Meat",hasChildren:false};
     * @return {Object} item
     */
    createItem(item) {

        var itemElement = document.createElement("li");
        itemElement.className = "group-body-item unselected";
        let itemTitle = document.createElement("span");
        itemTitle.className = "item-title";
        itemTitle.innerText = item.name;
        itemElement.appendChild(itemTitle);

        // checks if the given item can be expanded(if it has children)

        if (item.hasChildren == true) {
            let itemButton = document.createElement("span");
            itemButton.innerText = ">";
            itemButton.className = "item-button";
            itemButton.addEventListener("click", event => {
                let query = event.target.previousElementSibling.innerHTML;
                fetch("/api/IngredientType/SubItems/" + query).then(async response => {
                    var result = null;
                    result = await response.json();
                    this.clear();

                 //   console.log(this.#path.children[this.#path.children.length - 1].innerText);
                    // checks if the paths has been added before.
                    // when there is delay to recieve the data, user maight click more than once.

                    if (this.#path.children[this.#path.children.length - 1].innerText !== query) {
                        this.addPath(query);
                       
                    }
                    this.initItems(result);
                }
                );
            }, false);
            itemElement.appendChild(itemButton);
        }
        // adds events to the item to chnge the classes of the selected item
        itemTitle.addEventListener("click", event => {

            // find the current element type to see where user has clicked  
            let currentElement = event.target.parentElement;

            // if there is no item selected
            // tempItem == null is true when the user selects an item for the first time
            if (this.#tempItem === null) {

                currentElement.className = "group-body-item active-group-body-item selected";
                this.#tempItem = currentElement;

            } else {
                this.#tempItem.className = "group-body-item unselected";
                currentElement.className = "group-body-item active-group-body-item selected";
                this.#tempItem = currentElement;
            }

        }, false);

        return itemElement;
    }
    /**
     * Getter
     * Returns the item selected by user (clicked on the save change button).
     * selectedIetemValue
     * @return {string} value 
     * 
     * */

    get selectedItemValue() {
        if (this.#seletedElement !== null) {
            return this.#seletedElement.children[0].innerText;
        }
        else {
            return null;
        }
    }
/**
* Getter
* Returns the item selected by user (clicked on the save change button).
* selectedItem
* @return {HTMLLIElement} selectedItem
*
* */
    get selectedItem() {
        // discard the temperory selected item(tempItem)

        if (this.#seletedElement !== null) {
            // this.#seletedElement.className = "group-body-item unselected";
            return this.#seletedElement;
        }
        return null;
    }
    /**
     * Checks the selected item and initialises the selected item
     * @return void
     * */
    initSelectedItem() {

        // first time : user has not clicked on any item yet.
        // Resul:this.#tempItem is null; this.selectedItem is null;
        // There is nothing to do

        // User clicked, but did not click on the save change button
        // Result: this.#tempItem is not null and this.selectedItem is null;
        // Set this.#tempItem.className = "group-body-item unselected";

        if (this.#tempItem !== null && this.selectedItemValue === null) {
            this.#tempItem.className = "group-body-item unselected";
        }

        // User clicked and saved changes, but changed his mind and selected an other item and forgot to save the change.
        // Result: this.selectedItemValue cannot be empty and  this.selectedItemValue is not equal to this.#tempItem.children[0].innerText(tempItem value)
        // Set this.#tempItem.className = "group-body-item unselected"; this.selectedItem.className = "group-body-item active-group-body-item selected";  this.#tempItem = this.selectedItem;

        if (this.selectedItemValue !== null) {
            if (this.selectedItemValue !== this.#tempItem.children[0].innerText) {
                this.#tempItem.className = "group-body-item unselected";
                this.selectedItem.className = "group-body-item active-group-body-item selected";
                this.#tempItem = this.selectedItem;
            }
        }
    }
    /**
     * Clears all the GroupDialog body items
     * @return void
     * */
    clear() {
        this.#body.innerHTML = null;
    }
}

export default GroupDialog