class Table {

    #header = null;
    #body = null;
    #footer = null;
    #orginalRows = null;
    #clickedColumn = null;
    #orderType = Table.OrderType.NONE;
 
    constructor(HtmlTableElement) {
        this.#header = HtmlTableElement.getElementsByTagName("thead")[0];
        this.#body = HtmlTableElement.getElementsByTagName("tbody")[0];
        this.#orginalRows = [...this.#body.children];
    }
    /**
     * Static getter
     * used to identify the order of the sorted data
     * OrderType
     * @return {Object} OrderType 
     * */
    static get OrderType() {
        return { ASC: 1, DESC: 2, NONE: 3 };
    }
    /**
     * Static getter
     * Used to identify the data type sorted data
     * DataType
     * @return {Object} DataType
     * */
    static get DataType() {
        return { STRING: "string", NUMBER: "number", DATE: "date", MAIL: "mail", PHONE: "phone" };
    }

    /**
     * Getter
     * Returns an array of table head columns
     * HTMLTableHeader
     * @return {[HTMLTableColElement]} children  
     * */
    get HTMLTableHeader() {
        return this.#header.children[0];
    }
    /**
     *Getter
     * Returns <tbody></tbody> tag in the given table
     * HTMLTableBody
     * @return {HTMLTableBody} body
     * */
    get HTMLTableBody() {
        return this.#body;
    }
    /**
     * Getter
     * Returns an array of HTMLCollections; unfiltered data
     * HTMLTableBodyAllData
     * @return {[HTMLTableRowElement]} orginalRows 
     * */
    get HTMLTableBodyAllData() {

        return this.#orginalRows;
    }
/**
 * Getter
* Returns an array of HTMLCollections; filtered data
 * HTMLTableBodyArray
 * @return {[HTMLTableRowElement]} children
* */
    get HTMLTableBodyArray() {
        
        return [...this.#body.children];
    }
    /**
     * Getter
     * Returns the footer of the table.
     * HTMLTableFooter
     * @return {HTMLElement} tableFooter
     * */
    get HTMLTableFooter() {
        return this.#footer;
    }

    /**
     * sorts the given data(HTMLCollection) by given column and sort type. 
     * @param {Number} columnIndex
     * @param {OrderType} sortType
     * @return {void}
     */
    sort(columnIndex, sortType) {
        // fix the data type. read data-target attribute to get data type.

        let dataType = this.#header.children[0].children[columnIndex].getAttribute("data-target");
       
        var parentNode = this.#body;
    
        switch (dataType) {
            case Table.DataType.STRING: {
                [].slice.call(this.HTMLTableBodyArray).sort((secondRow, firstRow) => {
                    let firstValue = firstRow.children[columnIndex].innerText.toLowerCase();
                    let secondValue = secondRow.children[columnIndex].innerText.toLowerCase();
                   
                    let result = 0;
                    if (firstValue <= secondValue) {
                        result = 1;
                    }
                    else {
                        result = -1;
                    }
                    if (sortType === Table.OrderType.DESC) {
                        result *= -1;
                    }
                    return result;

                }).forEach(val => {
                    parentNode.appendChild(val);
                });
            }
                break;
            case Table.DataType.NUMBER: {
              
                [].slice.call(this.HTMLTableBodyArray).sort((secondRow, firstRow) => {
                    let firstValue = Number(firstRow.children[columnIndex].innerText);
                    let secondValue = Number(secondRow.children[columnIndex].innerText);
                    let result = secondValue - firstValue;
                  

                    if (sortType === Table.OrderType.DESC) {
                        result *= -1;
                    }
                    return result;
                }).forEach(val => {
                    parentNode.appendChild(val);
                });
                   
            }

                break;
            case Table.DataType.DATE: {
                [].slice.call(this.HTMLTableBodyArray).sort((secondRow, firstRow) => {
                    let result = new Date(firstRow.children[columnIndex].innerText) - new Date(secondRow.children[columnIndex].innerText);

                    if (sortType === Table.OrderType.DESC) {
                        result *= -1;
                    }
                    return result;
                }).forEach(val => {
                    parentNode.appendChild(val);
                });
            }
                break;
        }
    }

    /**
     * Activates the sort functionality.
     * activateSort()
     * @return void
     * */
    activateSort() {
    
        var columnIndex = -1;
        for (let child of this.#header.children) {
           
            child.addEventListener("click", event => {
                columnIndex = event.target.cellIndex;
                // if the user clickes more than one time in a row on the culomn
                if (this.#clickedColumn === this.#header.children[0].children[columnIndex]) {
                   
                    if (this.#orderType === Table.OrderType.ASC) {
                        this.#orderType = Table.OrderType.DESC;
                        this.sort(columnIndex, Table.OrderType.DESC);
                    }
                    else {
                        this.sort(columnIndex, Table.OrderType.ASC);
                        this.#orderType = Table.OrderType.ASC;
                       
                    }
                   
                    // If there the user clicks  once in a row on the column. 
                } else {
                    this.#clickedColumn = this.#header.children[0].children[columnIndex];
                    this.#orderType = Table.OrderType.ASC;
                    this.sort(columnIndex, Table.OrderType.ASC);
                }
               
               
            }, false);
        }

    }
    /**
     * Returns a cell by the given coordination
     * getCell(rowNumber, columnNumber)
     * @param {Number} rowNumber
     * @param {Number} columnNumber
     * @return {HTMLTableCellElement} cell
     */
    getCell(rowNumber, columnNumber) {
        return this.#body.children[rowNumber].children[columnNumber];

    }
    /**
     * Returns a row by the given row number
     * getRow(rowNumber)
     * @param {Number} rowNumber
     * @return {HTMLTableRowElement} row
     */
    getRow(rowNumber) {
        return this.#body.children[rowNumber];

    }
    /**
     * Sets the object of attributes for the given element
     * @param {HTMLElement} element
     * @param {Object} attributes
     * Example: let attributes = {name:"",id:""};
     * @return void
     */
    setAttributes(element, attributes) {
        for (let attribute in attributes) {
            element.setAttribute(attribute, attributes[attribute]);
        }
    }
    /**
     * Sets the object of events for the given element
     * @param {HTMLElement} element
     * @param {Object} events
     * @retun void
     */
    setEvents(element, events) {
        for (let event in events) {
            element.addEventListener(event, events[event]);
        }
    }
}
export default Table