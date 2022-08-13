

function initSidebar(sidebar,bars,closeButton,isVisible){

    sidebar.addEventListener("animationend", event=>{
        if (!isVisible){
            sidebar.setAttribute("class"," sidebar");
            isVisible=true;
        }else{
            sidebar.setAttribute("class","sidebar slideOutLeft");
           
            isVisible=false;
            sidebar.setAttribute("class","sidebar hide");
        }
     }, false);


     bars.addEventListener("click",event=>{
   if(!isVisible){
    sidebar.setAttribute("class","sidebar slideInLeft");
   }else{
    sidebar.setAttribute("class","sidebar slideOutLeft");
   // sidebar.setAttribute("class","sidebar hide");
   }
       
     
     },false);

     

  closeButton.addEventListener("click",event=>{sidebar.setAttribute("class","sidebar slideOutLeft");
},false);

  /* ul sub menu*/
  var subMenues = document.querySelectorAll("li.sidebar-item  ul.sub-menu ");

 for (index=0;index<subMenues.length;index++){
   let menuItem = subMenues[index].parentNode.children[0];
   let isVisible = false;
   menuItem.addEventListener("click",event=>{
       if (!isVisible){
           event.path[0].nextElementSibling.setAttribute("class","sub-menu");
           isVisible=true;
       }else{
           event.path[0].nextElementSibling.setAttribute("class","sub-menu hide");
           isVisible=false;
       }
  },false);
 }

}
var isVisible = false;
document.addEventListener("DOMContentLoaded",event=>{
    
    var closeButton =document.getElementById("close"); 
    var sidebar = document.getElementsByClassName("sidebar")[0];
    var bars = document.getElementsByClassName("bars")[0];

    if (bars !== undefined) {
        initSidebar(sidebar, bars, closeButton, isVisible);
    }
    
},false);
