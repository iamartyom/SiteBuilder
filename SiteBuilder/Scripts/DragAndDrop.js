'use strict';
/**
// ||||||||||||||||||||||||||||||| \\
//	Global Object $: Generic controls
// ||||||||||||||||||||||||||||||| \\
**/
(function(){
	// http://stackoverflow.com/questions/4083351/what-does-jquery-fn-mean
	var $ = function( elem ){
		if (!(this instanceof $)){
      return new $(elem);
		}
		this.el = document.getElementById( elem );
	};
	window.$ = $;
	$.prototype = {
	    onChange: function (callback) {
            if (this.el != undefined) {
                this.el.addEventListener('change', callback );
            }
			return this;
		}
	};
})();

/**
// ||||||||||||||||||||||||||||||| \\
//	Drag and Drop code for Upload
// ||||||||||||||||||||||||||||||| \\
**/
var dragdrop = {
	init : function( elem ){
		elem.setAttribute('ondrop', 'dragdrop.drop(event)');
		elem.setAttribute('ondragover', 'dragdrop.drag(event)' );
	},
	drop : function(e){
		e.preventDefault();
		var file = e.dataTransfer.files[0];
		runUpload( file );
	},
	drag : function(e){
		e.preventDefault();
	}
};

/**
// ||||||||||||||||||||||||||||||| \\
//	Code to capture a file (image) 
//  and upload it to the browser
// ||||||||||||||||||||||||||||||| \\
**/
function runUpload( file ) {
	// http://stackoverflow.com/questions/12570834/how-to-preview-image-get-file-size-image-height-and-width-before-upload
	if( file.type === 'image/png'  || 
			file.type === 'image/jpg'  || 
		  file.type === 'image/jpeg' ||
			file.type === 'image/gif'  ||
			file.type === 'image/bmp'  ){
		var reader = new FileReader(),
				image = new Image();
		reader.readAsDataURL( file );
		reader.onload = function( _file ){
			$('imgPrime').el.src = _file.target.result;
//			console.log( _file.target.result );
			$('imgPrime').el.style.display = 'inline';
			sendAjax();
		} // END reader.onload()
	} // END test if file.type === image
}

/**
// ||||||||||||||||||||||||||||||| \\
//	window.onload fun
// ||||||||||||||||||||||||||||||| \\
**/
window.onload = function(){
	if( window.FileReader ){
		// Connect the DIV surrounding the file upload to HTML5 drag and drop calls
		dragdrop.init( $('userActions').el );
		//	Bind the input[type="file"] to the function runUpload()
		$('fileUpload').onChange(function () { runUpload(this.files[0]); });
		
	}else{
		// Report error message if FileReader is unavilable
		var p   = document.createElement( 'p' ),
				msg = document.createTextNode( 'Sorry, your browser does not support FileReader.' );
		p.className = 'error';
		p.appendChild( msg );
		$('userActions').el.innerHTML = '';
		$('userActions').el.appendChild( p );
	}
};

function sendAjax() {

    jQuery.ajax({
        type: "POST",
        url: "/User/UpdateImage",
        data: JSON.stringify({ data: getImage() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

function getImage() {
    var image =document.getElementById("imgPrime").src;
    return image;
}