import AdvanceTable from "./AdvanceTable.js";

var filterSection = document.getElementById("filter-section");
var HTMLTable = document.getElementsByTagName("table")[0];
var advanceTable = new AdvanceTable(HTMLTable, filterSection);

let messages = [
    "Select a filter from select box.",
    "Please enter an ingredient group name. E.g., Ingredients.",
    "Please enter a group name of an ingredient group. E.g., Ingredients."
];
advanceTable.sePlaceHolder(messages);
advanceTable.setWarning();
advanceTable.activateSort();
advanceTable.initEvents();
