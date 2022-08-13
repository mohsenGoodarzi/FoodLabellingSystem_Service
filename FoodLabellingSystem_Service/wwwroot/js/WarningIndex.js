import AdvanceTable from "./AdvanceTable.js";

var filterSection = document.getElementById("filter-section");
var HTMLTable = document.getElementsByTagName("table")[0];
var advanceTable = new AdvanceTable(HTMLTable, filterSection);

let messages = [
    "Select a filter from select box.",
    "Please enter a name. E.g., Milk.",
    "Please enter a part of the message. E.g., attention"
];
advanceTable.sePlaceHolder(messages);
advanceTable.setWarning();
advanceTable.activateSort();
advanceTable.initEvents();