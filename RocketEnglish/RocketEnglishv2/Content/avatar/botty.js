var canvas;
var context;
var x = 180;
var y = 200;
var images = {};
var totalResources = 5;
var numResourcesLoaded = 0;

var numJumps = 7;
var voice = new Audio("../../Content/audio/talk.wav");

var happiness = 0;
var happinessInc = 0;
var currHappiness = 0;

var up = -1;
var pos = 0;
var max = 8;
var mov = 0.2;
var mood = 1;

var switchCount = 8;
var talking = 0;

var idleMov = setInterval(updateFloat, 1000/60);
var happinessInterval = setInterval(updateHappiness, 1000/60);
var mouthInterval = setInterval(updateMouth, 1000/10);
var sadnessInterval = setInterval(updateSadness, 1000/60);

var redMouth = new Image();
redMouth.src = "../../Content/avatar/redMouth.png";
var normalMouth = new Image();
normalMouth.src = "../../Content/avatar/mouth.png";

var sadnessInc = 0;
var sadness = 0;

function prepareCanvas(canvasDiv, canvasWidth, canvasHeight)
{
    // Create the canvas (Neccessary for IE because it doesn't know what a canvas element is)
    canvas = document.createElement('canvas');
    canvas.setAttribute('width', canvasWidth);
    canvas.setAttribute('height', canvasHeight);
    canvas.setAttribute('id', 'canvas');
    canvasDiv.appendChild(canvas);
    
    if(typeof G_vmlCanvasManager != 'undefined') {
        canvas = G_vmlCanvasManager.initElement(canvas);
    }
    context = canvas.getContext("2d"); // Grab the 2d canvas context
    // Note: The above code is a workaround for IE 8and lower. Otherwise we could have used:
    //     context = document.getElementById('canvas').getContext("2d");
    
    loadImage("head");
    loadImage("leftEyebrow");
    loadImage("rightEyebrow")
    loadImage("mouth");
    loadImage("torso");
}

function loadImage(name) {

  images[name] = new Image();
  images[name].onload = function() { 
      resourceLoaded();
  }
  images[name].src = "../../Content/avatar/" + name + ".png";
}

function changeTorso(name){
    var newImage = new Image();
    newImage.src = "../../Content/avatar/" + name + ".png";

    images["torso"] = newImage;
}

function changeHead(name){
    var newImage = new Image();
    newImage.src = "../../Content/avatar/" + name + ".png";

    images["head"] = newImage;
}

function resourceLoaded() {

  numResourcesLoaded += 1;
  if(numResourcesLoaded === totalResources) {
  
    setInterval(redraw, 1000 / 60);
  }
}

function greet(){
    //talk();
    numJumps = 0;
    mov = 5;
    max = 20;
}

function updateFloat() { 

          
  if (up === 1) {  
    pos -= mov;
    if (pos < -max) {
        if(numJumps <= 6){
            numJumps++;
            if(numJumps >6){
                mov = 0.2;
                max = 8;
            }
        }

      up = -1;

    }
  } else {  
    pos += mov;
    if(pos > max) {

        if(numJumps <= 6){
            numJumps++;
            if(numJumps >6){
                mov = 0.2;
                max = 8;
            }
        }

      up = 1;
    }
  }

}

function makeHappy(){
    mood = 1;
    happiness = 10;
    happinessInc = 0.8;
}

function makeNeutral(){
    mood = 0;
    happiness = 0;
    happinessInc = 0.8;
    sadnessInc = 0.1;
}


function talk(){
    switchCount = 0;
    voice.play();
}

function updateMouth(){
    if(switchCount < 8){
        if(talking === 1){
            talking = 0;
            images["mouth"] = normalMouth;
        }
        else{
            var im = new Image();
            talking = 1;
            images["mouth"] = redMouth;
        
        }
        switchCount++;
    }
    
}

function updateHappiness(){
    if (mood === 1) {  
        if(currHappiness > happiness)
            happinessInc = 0;
        else{
            sadnessInc = 0.1;
            currHappiness += happinessInc;
        }
    }
    else {  
        if(currHappiness < happiness)
            happinessInc = 0;
        else
            currHappiness -= happinessInc;
    }
}


function makeSad(){
    mood = -1;
    sadnessInc = 0.1;
}

function updateSadness(){
    if(mood === -1)
        if(sadness >=1)
            sadnessInc = 0;
        else{
            happiness = 0;
            happinessInc = 0.8;
            sadness += sadnessInc;
        }
    else{
        if(sadness <=0)
            sadnessInc = 0;
        else
            sadness -= sadnessInc;
    }
}


function redraw() {
                
  canvas.width = canvas.width; // clears the canvas 


  context.drawImage(images["head"], (x-20)*0.4, (y-120-pos)*0.4, 200*0.4, 200*0.4);

  context.drawImage(images["mouth"], (x-17)*0.4 , (y-80-pos-2*currHappiness-10*sadness)*0.4, 200*0.4, 200*0.4 );


  context.drawImage(images["torso"], (x-75)*0.4, (y+37-pos)*0.4, 300*0.4, 300*0.4);



  context.translate( (x-15)*0.4,( y-180)*0.4);
  context.rotate(-0.3*sadness);
  context.drawImage(images["leftEyebrow"],(-30*sadness)*0.4,(15*sadness-pos-currHappiness)*0.4, 130*0.4, 130*0.4);



  context.translate((65)*0.4, (-112+115)*0.4);
  context.rotate(0.6*sadness);
  context.drawImage(images["rightEyebrow"], 30*sadness*0.4,(-5*sadness-pos-currHappiness)*0.4, 130*0.4, 130*0.4);
}