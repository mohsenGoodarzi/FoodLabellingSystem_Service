
import AdvanceTable from "./AdvanceTable.js";

var filterSection = document.getElementById("filter-section");
var HTMLTable = document.getElementsByTagName("table")[0];
var advanceTable = new AdvanceTable(HTMLTable, filterSection);

let messages = [
    "Select a filter from select box.",
    "Please enter an ingredient name. E.g., Beef.",
    "Please describe the ingredient.",
    "Enter amount of the ingredient. E.g., 100.",
    "Enter amount of the fats. E.g., 100",
    "Enter amount of the carbohydrate. E.g., 100.",
    "Enter amount of the Protein. E.g., 100.",
    "Enter amount of the Calory. E.g., 100.",
    "Enter amount of the Ingredient type. E.g., Red-Meat.",
    "Enter a unit name. E.g., gram.",
    "Enter a warning name. E.g., Allergic."
    
];
advanceTable.sePlaceHolder(messages);
advanceTable.setWarning();
advanceTable.activateSort();
advanceTable.initEvents();