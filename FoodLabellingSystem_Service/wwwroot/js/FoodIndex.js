import AdvanceTable from "./AdvanceTable.js";

var filterSection = document.getElementById("filter-section");
var HTMLTable = document.getElementsByTagName("table")[0];
var advanceTable = new AdvanceTable(HTMLTable, filterSection);

let messages = [
    "Select a filter from select box.",
    "Please enter a food name. E.g., Fish and chips.",
    "Please describe the food.",
    "Enter the food type., E.g., vegetarian.",
    "Enter the cuisine type., E.g., British cuisine.",
    "Enter the dish type., E.g., Main Dish.",
    "Enter the warning., E.g., Milk."
];
advanceTable.sePlaceHolder(messages);
advanceTable.setWarning();
advanceTable.activateSort();
advanceTable.initEvents();