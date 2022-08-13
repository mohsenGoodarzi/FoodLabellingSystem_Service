import Table from "./Table.js";

/**
 *AdvanceTable class extends Table class and facilates with filter functionality 
 * */

class AdvanceTable extends Table {

    #filterSection = null;
    #addButton = null;
    #selectBox = null;
    #searchBox = null;
    #criteriaBox = null;
    #filteredData = null;
    /**
     * Constructor
     * AdvanceTable(HTMLTableElement,filterSection)
     * @param {HTMLTableElement} HTMLTableElement
     * @param {HTMLDivElement} filterSection
     * returns an instance of the class
     */
    constructor(HTMLTableElement, filterSection) {

        super(HTMLTableElement);

        this.#filterSection = filterSection;
        this.#addButton = filterSection.querySelector("button#button-addon");
        this.#selectBox = filterSection.querySelector("select#select-box");
        this.#searchBox = filterSection.querySelector("input#search-box");
        this.#criteriaBox = filterSection.querySelector("div#criteria-box");
        this.#filteredData = this.HTMLTableBodyAllData;

    }
/**
* Sets the place holder of search box regards to the item selected from select box
* setPlaceHolder(messages)
 * The messages need to be in the same order as select box items are. 
* example: let messages = ["Please enter a food name"];
* @return void
*
* */
    sePlaceHolder(messages) {
        this.selectBox.addEventListener("change", event => {
            let index = Number(event.target.value);
            this.#searchBox.setAttribute("placeholder", messages[index]);
        }, false);
       
    }

    /**
     * Set the warning message box for an empty select box(drop down) and
     * an empty search box (input)
     * setWarning()
     * @return void
     * */
    setWarning() {

        this.#searchBox.addEventListener("focus", event => {

            if (this.#selectBox.value === "0") {
                this.#searchBox.setAttribute("data-toggle", "modal");
                let modalBody = document.getElementsByClassName("modal-body")[0];
                modalBody.innerText = "You have not selected any filter option yet.";
                this.#selectBox.style.background = '#e9ecef';
                this.#searchBox.value = "";
            } else {
                this.#searchBox.setAttribute("data-toggle", "");
                this.#selectBox.style.background = 'white';
            }

        }, false);

        this.#searchBox.addEventListener("input", event => {

            if (this.#selectBox.value === "0") {

                event.target.value = "";
            }
        }, false);
    }

    /**
     * Getter
     * Returns an input element 
     * searchBox
     * @return {HTMLInputElement}
     *
     * */

    get searchBox() {
        return this.#searchBox;
    }
/**
 * Getter
 * returns a select box 
 * seletBox
 * @return {HTMLSelectElement}
 *
 * */

    get selectBox() {
        return this.#selectBox;
    }
   
    /**
     * initialises all the events
     * initEvents()
     * @return void
     * */
    initEvents() {

        this.#addButton.addEventListener('click', event => {

            // checks if there is something to check.
            if (this.#searchBox.value.length === 0) {
                this.#searchBox.setAttribute("data-toggle", "modal");
                let modalBody = document.getElementsByClassName("modal-body")[0];
                modalBody.innerText = "You have not specify what you are looking for.";
                this.#searchBox.style.background = '#e9ecef';
                this.#searchBox.click();

                // validates the search box and creates a criterion

            } else {
                this.#searchBox.setAttribute("data-toggle", "");
                this.#searchBox.style.background = 'white';

                let criterionObject = { text: this.#searchBox.value, columnIndex: this.#selectBox.value };
                this.createCriterion(criterionObject);

                // reads the last critrion value.
                let value = this.criteriaBox.children[this.criteriaBox.childElementCount - 1].children[0].innerText;

                // reads the column index of the last critrion added
                let columnIndex = this.criteriaBox.children[this.criteriaBox.childElementCount - 1].children[0].getAttribute("column-index");


                // Apply the las critrion.

                this.#filteredData = this.filterData(this.#filteredData, columnIndex, value);

                // initialize table body after filtering the data.
                this.initTableBody(this.#filteredData);
            }

        }, false);

    }
    /**
     * loads all the added criteria and applies them on the table.
     * applyFilter()
     * @return {[HTMLTableRowElement]}filtredData
     * */
    applyFilter() {

        // loads criterion and applies them on the data
        let filteredData = this.HTMLTableBodyAllData;
        for (let i = 0; i < this.criterionLength; i++) {
            // the text of the critrionBox

            let value = this.criteriaBox.children[i].children[0].innerText;
            // the column of the data that the filter needs to be applied 

            let columnIndex = this.criteriaBox.children[i].children[0].getAttribute("column-index");
            // filters the data

            filteredData = this.filterData(filteredData, columnIndex, value);
        }
        return filteredData;
    }

    /**
     * clears the table body elements and adds the filtered array of HTMLTableRowElement to it.
     * initTableBody(filteredData)
     * @param {HTMLTableRowElement} filteredData
     * @return void
     */
    initTableBody(filteredData) {
        this.HTMLTableBody.innerHTML = "";
        filteredData.forEach(row => {
            this.HTMLTableBody.appendChild(row);
        });
    }

    /**
     * Creates criterion and adds to criteria box.
     * createCriterion(criterionObject)
     * @param {Object} criterionObject
     * Example: let criterionObject = {text:string,columnIndex:Number}
     * @return void
     */
    createCriterion(criterionObject) {

        this.#criteriaBox = this.#criteriaBox;

        let criterionBox = document.createElement('div');

        criterionBox.setAttribute("class", "input-group-text mr-2 mb-2");

        let criterionText = document.createElement("span");
        criterionText.className = "mr-3";
        criterionText.innerText = criterionObject.text;


        criterionText.setAttribute("column-index", criterionObject.columnIndex);
        criterionBox.appendChild(criterionText);

        let closeButton = document.createElement("button");
        closeButton.className = "close";
        closeButton.setAttribute("type", "button");
        closeButton.setAttribute("aria-label", "Close");
        criterionBox.appendChild(closeButton);
        closeButton.innerHTML = ' <span aria-hidden="true">&times;</span>';

        closeButton.addEventListener('click', event => {

            event.target.parentElement.parentElement.remove();


            this.#filteredData = this.applyFilter();
            this.initTableBody(this.#filteredData);


        }), false;

        this.#criteriaBox.appendChild(criterionBox);

    }
    /**
     * Getter
     * criteriaBox
     * @return HTMLDivElement Criteria Box
     **/
    get criteriaBox() {
        return this.#criteriaBox;
    }
    /**
     * Getter
     * Number of criteria in the clriteria box.
     * criterionLength
     * @return Number
     * */
    get criterionLength() {
        return this.#criteriaBox.childElementCount;
    }
    /**
     * filteres the given data and returns filtered data.
     * @param {[HTMLTableRowElement]} data
     * @param {Number} columnIndex
     * @param {any} value
     * Example: data = filterData(data, columnIndex, value);
     * @return {[HTMLTableRowElement]} filteredData
     */
    filterData(data, columnIndex, value) {
        let filteredData = null;
        let regExp = null;
       
        let dataType = this.HTMLTableHeader.children[columnIndex-1].getAttribute("data-target");
        console.log(this.HTMLTableHeader.children[columnIndex-1]);
        if (dataType === Table.DataType.NUMBER) {
          
            filteredData = data.filter(row => {
                console.log(row.children[columnIndex - 1]);
                if (row.children[columnIndex - 1].innerText === value) {
                    return true;
                } else {
                    return false;
                }
            });
        } else
            // used for string types

            if (dataType === Table.DataType.STRING) {
                //use regex here
                regExp = regExp || new RegExp(value + "+", "gi");
                filteredData = data.filter(row => {
                    let arrayRow = [...row.children];
                    console.log();
                    if (arrayRow[columnIndex - 1].innerText.toLowerCase().match(regExp) !== null) {
                        return true;
                    }
                    else {
                        return false;
                    }
                });
            }

        return filteredData;
    }
}
export default AdvanceTable