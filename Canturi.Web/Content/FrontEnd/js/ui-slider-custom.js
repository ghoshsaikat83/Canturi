
   function getUrlParams() {
        var result = {};
        var params = window.location.href.toLowerCase().substring(window.location.href.toLowerCase().indexOf('/loose-diamonds'), window.location.href.Length).replace('/loose-diamonds/', '').replace('/loose-diamonds', '').split('/');
        if (params[0] != null && params[0] != "") { result["shape"] = params[0].toLowerCase(); }
        return result;
    }

    function SelectFilters(filterVal) {
        if (filterVal != null && filterVal != '') {
            var filterArr = new Array();
            if (filterVal.indexOf('-or-') > -1) {
                filterArr = filterVal.split('-or-');
            }
            else {
                filterArr[0] = filterVal;
            }


            /* Check in the shape */
            for (var s = 0; s < filterArr.length; s++) {
                $('#shapediv ul li').each(function() {
                    var jId = $(this).context.id;
                    if (jId == GetStyleLi[s]) {
                        document.getElementById(jId).className += "active";
                    }
                });
            }

            /* Check in the shape */
        }
    }

    function SetFilterValues() {
        document.getElementById("arprice").className = 'active-asc';
        /* For the diamond search */
        $('#shapediv ul li').each(function() {
            if ($(this).attr('class') == "active") {
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = $(this).context.id.replace('li', '');
            }
        });
    }


    function Create_Url() {

        // for shape
        var selectedS = new Array();
        $('#shapediv ul li').each(function() {
            if ($(this).attr('class') == "active") {
                selectedS.push($(this).context.id.replace('li', '').toLowerCase());
            }
        });


        var shaperurl = GetShapeURL(selectedS);
      

        var createurl = window.location.href.toLowerCase().substring(0, window.location.href.toLowerCase().indexOf('/loose-diamonds')) + '/loose-diamonds';
        if (shaperurl != null && shaperurl != '') {
            createurl += '/' + shaperurl;
        }
       

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf('MSIE ');
        var trident = ua.indexOf('Trident/');

        if (msie <= 0 || trident <= 0) {
            //var retval = history.replaceState(statedata, title, url);
            window.history.replaceState({}, "", createurl);
        }
        else {
            // Handle the IE as this functionality is not supported in IE.
        }
    }

    function GetShapeURL(val) {
        var newVal = '';
        for (var i = 0; i < val.length; i++) {
            newVal += val[i];
            if (i < (val.length - 1)) {
                newVal += '-OR-';
            }
        }
        return newVal;
    }

    function Create_UrlRecommended() {

        // for shape
        var selectedS = new Array();
        $('#shapedivRecommended ul li').each(function() {
            if ($(this).attr('class') == "active") {
                selectedS.push($(this).context.id.replace('li', '').replace('Recommended','').toLowerCase());
            }
        });


        var shaperurl = GetShapeURL(selectedS);


        var createurl = window.location.href.toLowerCase().substring(0, window.location.href.toLowerCase().indexOf('/loose-diamonds')) + '/loose-diamonds';
        if (shaperurl != null && shaperurl != '') {
            createurl += '/' + shaperurl;
        }


        var ua = window.navigator.userAgent;
        var msie = ua.indexOf('MSIE ');
        var trident = ua.indexOf('Trident/');

        if (msie <= 0 || trident <= 0) {
            //var retval = history.replaceState(statedata, title, url);
            window.history.replaceState({}, "", createurl);
        }
        else {
            // Handle the IE as this functionality is not supported in IE.
        }
        // SetSearchUrlAnchor();
    }

    function RefreshInternetExplorerData(part) {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf('MSIE ');
        var trident = ua.indexOf('Trident/');
       
        if (msie > 0 || trident > 0) {
            window.location = Create_Anchor_Url('', '', part);
        }
    }

    function Create_Anchor_Url(val, type, part) {

        // for shape
        var selectedD = new Array();

        if (part == 'S') {
            $('#shapediv ul li').each(function() {
                var jId = $(this).context.id;
                if ($(this).attr('class') == "active") {

                    if (jId != val) {

                        selectedD.push(jId.replace('li', '').toLowerCase());
                    }
                }
            });

            if (type == 'shape') {
                selectedD = new Array();
                selectedD.push(val.replace('li', '').toLowerCase());
            }
        }

        if (part == 'R') {
            $('#shapedivRecommended ul li').each(function() {
                var jId = $(this).context.id;
                if ($(this).attr('class') == "active") {
                    if (jId != val) {
                        selectedD.push(jId.replace('li', '').replace('Recommended', '').toLowerCase());
                    }
                }
            });

            if (type == 'shape') {
                selectedD = new Array();
                selectedD.push(val.replace('li', '').replace('Recommended', '').toLowerCase());
            }
        }



        var shapeurl = GetShapeURL(selectedD);

        var createurl = window.location.href.toLowerCase().substring(0, window.location.href.toLowerCase().indexOf('/loose-diamonds')) + '/loose-diamonds';
        if (shapeurl != null && shapeurl != '') {
            createurl += '/' + shapeurl;
        }

        return createurl;
    }


    $(document).ready(function() {

        var intCurrentPage = 0;
        var intCurrentPageRecommended = 0;

        $('#RecommendedDiamond').click(function() {

            document.getElementById("arpriceRecommended").className = 'active-asc';

            /* For the recommended diamond search */
            $('#shapedivRecommended ul li').each(function() {
                if ($(this).attr('class') == "active") {
                    $(this).removeClass();
                }
            });

            var selectedShapes = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value;

            if (selectedShapes != 'Nolist' && selectedShapes != '') {

                var selectedRecommended = selectedShapes.split(",");

                for (var i = 0; i < selectedRecommended.length; i++) {
                    document.getElementById('li' + selectedRecommended[i] + 'Recommended').className += "active";
                }
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelistRecommended").value = "";
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelistRecommended").value = selectedRecommended;
            }
            else {
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelistRecommended").value = "";
            }


          
            BindDataRecommended(1);

        });


        $('#DiamondSearch').click(function() {

            document.getElementById("arprice").className = 'active-asc';

            /* For the diamond search */
            $('#shapediv ul li').each(function() {
                if ($(this).attr('class') == "active") {
                    $(this).removeClass();
                }
            });

            var selectedShapes = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelistRecommended").value;
            if (selectedShapes != 'Nolist' && selectedShapes != '') {

                var selectedDiamondSearch = selectedShapes.split(",");

                for (var i = 0; i < selectedDiamondSearch.length; i++) {
                    document.getElementById('li' + selectedDiamondSearch[i]).className += "active";
                }
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = "";
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = selectedDiamondSearch;
            }
            else {
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = "";
            }

            BindData(1);

        });
    });

    function scrollbar() {
        var oScrollbar5 = $('#scrollbar1');
        oScrollbar5.tinyscrollbar();
        if (intCurrentPage > 1) {

            oScrollbar5.tinyscrollbar_update(parseInt(intCurrentPage * 1200));
        }
        else {

            oScrollbar5.tinyscrollbar_update();
        }

        //        var totalScrollOffsetH = oScrollbar5.height();
        //        oScrollbar5Offset:totalScrollOffsetH;
        // oScrollbar5.tinyscrollbar_update();


    }
    function scrollbarRecommended() {
        var oScrollbar6 = $('#scrollbar1Recommended');
        oScrollbar6.tinyscrollbar();
        if (intCurrentPageRecommended > 1) {

            oScrollbar6.tinyscrollbar_update(parseInt(intCurrentPageRecommended * 1200));
           
        }
        else {

            oScrollbar6.tinyscrollbar_update();
        }
        //oScrollbar6.tinyscrollbar_update();
    }


    var lastScrollTop = 0;
    //var oldPage = 1
    $(document).ready(function() {
        $("#divDiamondTestResultScroll").on("scrollstop", function() {
            var totalRecords = document.getElementById("hdtotalcount").value;
            var recordPerPage = 50;
            var totalPage = 0;

            totalPage = totalRecords / recordPerPage;

            if ((totalPage - Math.floor(totalPage)) != 0) {
                totalPage = totalPage.before() + 1;
            }
            else {
                totalPage = totalPage.before();
            }

            var dRecord = parseFloat(totalRecords) * 43.54;
            var totalSliderHeight = dRecord; // it is fixed (total div height)
            //634631  1657
            var callAfterThePage = 0;

            callAfterThePage = totalSliderHeight / totalPage ;
            //var callAfterThePage1 = (totalSliderHeight / totalPage) - (totalSliderHeight / oldPage);

            var st = $("#divDiamondTestResultScroll").scrollTop();
            if (st > lastScrollTop) {
                var scrollDifference = st - lastScrollTop;
                if (scrollDifference >= callAfterThePage) {
                    // Here we have to call ajax method for fetch the data
                    var newPageNumber = 1;
                    newPageNumber = st / callAfterThePage;

                    if ((newPageNumber - Math.floor(newPageNumber)) != 0) {
                        newPageNumber = newPageNumber.before() + 1;
                    }
                    else {
                        newPageNumber = newPageNumber.before();
                    }
                    // $("#divDiamondTestResultScroll").on("scrollstop", function() {
                    $("#divScrollCounter").html("Vertical: " + st + "px <br/> Page : " + newPageNumber);
                    // });


                    BindData(newPageNumber);
                  
                }
            } else {
                // upscroll code
            }
            lastScrollTop = st;
            //oldPage = newPageNumber;

        });
    });

    Number.prototype.before = function() {
        var value = parseInt(this.toString().split(".")[0], 10);
        return value ? value : 0;
    }

    function FindDivHeight(div) {
        $("#divScrollCounter").html("Vertical: " + div.scrollTop + "px");
    }
   
  

    
       function GetXmlHttpObject(handler) {
           var objXmlHttp = null
           if (navigator.userAgent.indexOf("MSIE") >= 0) {
               var strName = "Msxml2.XMLHTTP"
               if (navigator.appVersion.indexOf("MSIE 5.5") >= 0) {
                   strName = "Microsoft.XMLHTTP"
               }
               try {
                   objXmlHttp = new ActiveXObject(strName)
                   objXmlHttp.onreadystatechange = handler
                   return objXmlHttp
               }
               catch (e) {
                   alert("Error. Scripting for ActiveX might be disabled")
                   return
               }
           }
           if (navigator.userAgent.indexOf("Mozilla") >= 0) {
               objXmlHttp = new XMLHttpRequest()
               objXmlHttp.onload = handler
               objXmlHttp.onerror = handler
               return objXmlHttp
           }
       }



       var WaitingDiv = "<div id=\"loader_bgback\"></div><div id=\"divLoader\" style=\"width:780px; text-align:center;\"><img src=\"" + rootPath + "Themes/Default/Images/loader.gif\" /></div>";
       function BindData(pCurrentPage) 
       { 
          document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCurrentPage").value = pCurrentPage;
          intCurrentPage = pCurrentPage;
          
            
             document.getElementById("loader_bgback").style.display="block";
             document.getElementById("divLoader").style.display="block";
            $("#loader_bgback").show();
            $("#divLoader").show();
//            var shapeval = document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist').value;
//            if (shapeval.indexOf('engsku-') > -1) {
//                BindShapeSizeDiamonds(shapeval);
//            }




//            alert('shapesort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value);
//            alert('caratsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value);
//            alert('cutsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value);
//            alert('colorsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value);
//            alert('claritysort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value);
//            alert('polishsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value);
//            alert('pricesort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value);
//            alert('cutmin=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmin').value);
//            alert('cutmax=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmax').value);
            

            setTimeout(function(){
            var url = rootPath + "Controls/Default/DiamondSearch/Ajax.aspx?method=allDiamond&shapes=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist').value + "&caratmin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin').value + "&caratmax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax').value + "&colormin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormin').value + "&colormax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormax').value + "&cutmin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmin').value + "&cutmax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmax').value + "&claritymin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymin').value + "&claritymax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymax').value + "&pricemin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin').value + "&pricemax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax').value + "&shapesort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value + "&caratsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value + "&cutsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value + "&colorsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value + "&claritysort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value + "&polishsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value + "&pricesort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value + "&Cuurentpage=" + pCurrentPage + "&UID=" + new Date().getTime();
               xmlHttp = GetXmlHttpObject(stateChanged);
               xmlHttp.open("GET", url, false);
               xmlHttp.send(null);
           },100);
           

       }

      
       function stateChanged() {            
           if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
               if(document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCurrentPage").value=="1"){
                     $( "#divDiamondResult" ).html('');
                      $( "#divDiamondResult" ).append(xmlHttp.responseText);
                     
               }
               else
               {
               $( "#divDiamondResult" ).append(xmlHttp.responseText);
               }
               //var tRecord = parseFloat(document.getElementById("hdtotalcount").value) * 43.54;
               //$('#divDiamondResult').css({'height':tRecord+'px'});
               xmlHttp=null;
               scrollbar();
                document.getElementById("spnTotalDiamond").innerHTML=document.getElementById("hdtotalcount").value;
                CalcPages(eval(document.getElementById("divTotalRecord1").innerHTML), eval(document.getElementById("divPageSize1").innerHTML));
               $("#loader_bgback").hide();
                $("#divLoader").hide();
               //  document.getElementById("divLoader_new").style.display="none";
           }
       }


       function BindShapeSizeDiamonds(sku) {
           $.ajax({
               type: "POST",
               url: rootPath + "Contest.aspx/GetDiamondProductData",
               data: '{"Sku":"' + sku + '"}',
               contentType: "application/json; charset=utf-8",
               timeout: 1000, //timeout 1 sec
               async: false,
               dataType: "json",
               processdata: true,
               success: function(result) {
                   if (result.d.Shape != null && result.d.Shape != "") {
                       document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = '';
                       document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = result.d.Shape;
                   }

                   if (result.d.CaratMin != null && result.d.CaratMin != "") {
                       document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value = '';
                       document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value = result.d.CaratMin;

                       document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value = '';
                       document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value = result.d.CaratMax;
                   }
               }
           });
       }
       
     
       
       
 function FetchMore() {
    
    $('#divFetchNewData').detach();
    $('#divTotalRecord1').detach();
    $('#divPageSize1').detach();
    $("#divDiamondResult").children().each(function (e) {

       // $(this).removeData();
       $(this).removeAttr(jQuery.expando);
    });
   
    intCurrentPage++;
    document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCurrentPage").value = intCurrentPage;
    
    // setTimeout(function(){ GetSearchData(intSortOrder, intCurrentPage);},1000);
    return BindData(intCurrentPage);

}

 

//function to show the paging
function CalcPages(pintTotalRecord, pintPageSize) {
    
    var totalPages = 1;
    totalPages = pintTotalRecord / pintPageSize;
    totalPages = Math.ceil(totalPages);

    if (totalPages > 1) {

        if (totalPages == intCurrentPage) {
            // Do not run the function to get the new data
            $('#divFetchNewData').detach();
        }
        else {
               
            // run the function to get the new data once the scrolling end
            $('#divFetchNewData').appear(function () { FetchMore(); });
        }


    }
    else {
        // Do not run the function to get the new data
        $('#divFetchNewData').detach();
    }

}

function GetMinCaratSlider() {
	      
	          if (document.getElementById("carat-min").value == "") {
	          document.getElementById("carat-min").value=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value;
	              return;
	          }
	          if(parseFloat(document.getElementById("carat-min").value)< parseFloat("0.21"))
	          {
	               document.getElementById("carat-min").value="0.21";
	          }
	          if(parseFloat(document.getElementById("carat-min").value)> parseFloat("19.5"))
	          {
	               document.getElementById("carat-min").value="0.21";
	          }
	          if(parseFloat(document.getElementById("carat-min").value)> parseFloat(document.getElementById("carat-max").value))
	          {
	              document.getElementById("carat-min").value="0.21";
	          }
	          document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value = document.getElementById("carat-min").value;
	          document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value = document.getElementById("carat-max").value;
              var minval = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value;
	          var maxval = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value;
	           $("#carat-min").val(minval);
	           $("#carat-max").val(maxval);
	           var $slider = $("#slider-carat");
               $slider.slider("values", 0, minval);
               $slider.slider("values", 1, maxval);

	          BindData(1);
	      
	  }


	  function GetMAXCaratSlider() {
	      
	          if (document.getElementById("carat-max").value == "") {
	               document.getElementById("carat-max").value=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value
	              return;
	          }
	          if(parseFloat( document.getElementById("carat-max").value)>parseFloat("19.5"))
	          {
	              document.getElementById("carat-max").value="19.5";
	          }
	          if(parseFloat( document.getElementById("carat-max").value)<parseFloat("0.21"))
	          {
	              document.getElementById("carat-max").value="19.5";
	          }
	          if(parseFloat( document.getElementById("carat-max").value)< parseFloat(document.getElementById("carat-min").value))
	          {
	             document.getElementById("carat-max").value="19.5";
	          }
	          document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value = document.getElementById("carat-min").value;
	          document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value = document.getElementById("carat-max").value;
	          var minval = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value;
	          var maxval = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value;
	          $("#carat-min").val(minval)
	          $("#carat-max").val(maxval);
	          var $slider = $("#slider-carat");
              $slider.slider("values", 0, minval);
              $slider.slider("values", 1, maxval);
	          BindData(1);
	  }


       
	$(function() {
	   
	    var minval=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value;
	    var maxval=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value;
		$( "#slider-carat" ).slider({
			range: true,
			min: .21,
			max: 19.5,
			step:.01,
			values: [ minval, maxval ],
			stop: function( event, ui ) {
			document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin").value = ui.values[0];
				document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax").value =ui.values[1];
				BindData(1);
			},
			slide: function( event, ui ) {
				$( "#carat-min" ).val(ui.values[ 0 ]);
				$( "#carat-max" ).val( ui.values[ 1 ] );
			}
		});
		$( "#carat-min" ).val( $( "#slider-carat" ).slider( "values", 0 ));
		$( "#carat-max" ).val( $( "#slider-carat" ).slider( "values", 1 ));
		$('.Range a:last-child').addClass('handle-2');
		
		
		
	});
	
	
	$('#carat-min').live("keypress", function(e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            GetMinCaratSlider();
        }
    });

    $('#carat-max').live("keypress", function(e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            GetMAXCaratSlider();
        }
    });
	
	
$(function() {
	    var realvalues = [0, 100, 200, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000, 6500, 7000, 7500, 8000, 8500, 9000, 9500, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 55000, 60000, 65000, 70000, 75000, 80000, 85000, 90000, 95000, 100000, 105000, 110000, 115000, 120000, 125000, 130000, 135000, 140000, 145000, 150000, 155000, 160000, 165000, 170000, 175000, 180000, 185000, 190000, 195000, 200000, 205000, 210000, 215000, 220000, 225000, 230000, 235000, 240000, 245000, 250000, 255000, 260000, 265000, 270000, 275000, 280000, 285000, 290000, 295000, 300000, 310000, 320000, 330000, 340000, 350000, 360000, 370000, 380000, 390000, 400000, 450000, 500000, 550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 1000000, 1500000, 2000000,2500000,30000000,3500000,4000000,4500000,5000000,5500000,6000000,6500000,7000000,7500000,8000000,8500000,9000000,9500000,9999999];
		$( "#slider-price" ).slider({
			range: true,
			min: 0,
			max: 132,
			step: 1,
			values: [0, 132],
			stop:function( event, ui ) {
			  document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value =realvalues[ui.values[0]];
				document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value =realvalues[ui.values[1]];
				BindData(1);
			},
			slide: function( event, ui ) {
                $("#price-min").val("$"+realvalues[ui.values[0]]);
                $("#price-max").val("$"+realvalues[ui.values[1]]);
			}
		});
		$('.Range a:last-child').addClass('handle-2');
		  $("input.inputxtlarge").change(function() {
             var $this = $(this);
             
             
            var maxVal = $("#price-max").val().replace("$", '');
            var minVal = $("#price-min").val().replace("$", '');

            // set maximum value index
            var maxValueindex =132;
            for(index in realvalues)
            {
                if($("#price-max").val().replace("$", '')==realvalues[index])
                {
                    maxValueindex=index;
                    break;
                }
            }

            // set minimum value index
            var minValueindex =0;
            for(index in realvalues)
            {
                if($("#price-min").val().replace("$", '')==realvalues[index])
                {
                    minValueindex=index;
                    break;
                }
            }
             
             
             if($this.data("index")==0)
             {
                if($this.val()==0)
                {
                  $("#slider-price").slider("values", 0,0);
                  document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value="0";
                }
                else
                {
                   if(parseInt(minVal)> parseInt(maxVal))
                   {
                        $("#slider-price").slider("values", 0,maxValueindex);
                        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value=maxVal;
                   }
                   else
                   {
                        $("#slider-price").slider("values", 0,minValueindex);
                        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value=$this.val().replace("$", '');
                   }
                }
             }
             if($this.data("index")==1)
             {
                if($this.val()==0)
                {
                  $("#slider-price").slider("values", 1, 9999999);
                  document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value ="9999999";
                }
                else 
                {
                     if(parseInt(maxVal)< parseInt(minVal))
                     {
                        $("#slider-price").slider("values", 1,minValueindex);
                        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value =minVal;
                     }
                     else
                     {
                        $("#slider-price").slider("values", 1,maxValueindex);
                        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value =$this.val().replace("$", '');
                     }
                }
             }
             BindData(1);
             if($this.data("index")==0)
             {
               if($this.val()==0)
               {
                  $("input.inputxtlarge[data-index=0]").val("$0");
               }
               else
               {
                    if(parseInt(minVal)> parseInt(maxVal))
                    {
                        $("input.inputxtlarge[data-index=0]").val("$"+maxVal);
                    }
                    else
                    {
                        $("input.inputxtlarge[data-index=0]").val("$"+$this.val().replace("$", ''));
                    }
               }
             }
             
             if($this.data("index")==1)
             {
               if($this.val()==0)
               {
                  $("input.inputxtlarge[data-index=1]").val("$9999999");
               }
               else
               {
                     if(parseInt(maxVal)< parseInt(minVal))
                     {
                        $("input.inputxtlarge[data-index=1]").val("$"+minVal);
                     }
                     else 
                     {
                        $("input.inputxtlarge[data-index=1]").val("$"+$this.val().replace("$", ''));
                     }
               }
             }
             
         });
         
         
          // Start - on the back button click on next page (This will set the slider values)
		$(document).ready(function(){    
		    var sliderMinPrice=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value;
            var sliderMaxPrice=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value;
           
                if(sliderMinPrice!=''){
                    if(parseInt(sliderMinPrice)>0){
                        var sliderMinIndex =0;
                        for(index in realvalues)
                        {
                            if(sliderMinPrice==realvalues[index])
                            {
                                sliderMinIndex=index;
                                break;
                            }
                        }
                        $("#slider-price").slider("values", 0, sliderMinIndex);
                    }
                }
                
                 if(sliderMaxPrice!=''){
                    if(parseInt(sliderMaxPrice)>0){
                        var sliderMaxindex =132;
                        for(index in realvalues)
                        {
                            if(sliderMaxPrice==realvalues[index])
                            {
                                sliderMaxindex=index;
                                break;
                            }
                        }
                        $("#slider-price").slider("values", 1, sliderMaxindex);
                    }
                }
		});
		// End - on the back button click on next page (This will set the slider values)
         
         
         
	});
	
	
	 $('#price-min').live("keypress", function(e) {
        if (e.keyCode == 13) {
            e.preventDefault();

            var realvalues = [0, 100, 200, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000, 6500, 7000, 7500, 8000, 8500, 9000, 9500, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 55000, 60000, 65000, 70000, 75000, 80000, 85000, 90000, 95000, 100000, 105000, 110000, 115000, 120000, 125000, 130000, 135000, 140000, 145000, 150000, 155000, 160000, 165000, 170000, 175000, 180000, 185000, 190000, 195000, 200000, 205000, 210000, 215000, 220000, 225000, 230000, 235000, 240000, 245000, 250000, 255000, 260000, 265000, 270000, 275000, 280000, 285000, 290000, 295000, 300000, 310000, 320000, 330000, 340000, 350000, 360000, 370000, 380000, 390000, 400000, 450000, 500000, 550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 1000000, 1500000, 2000000, 2500000, 30000000, 3500000, 4000000, 4500000, 5000000, 5500000, 6000000, 6500000, 7000000, 7500000, 8000000, 8500000, 9000000, 9500000, 9999999];

            var maxVal = $("#price-max").val().replace("$", '');
            var minVal = $("#price-min").val().replace("$", '');

            // set maximum value index
            var maxValueindex =132;
            for(index in realvalues)
            {
                if($("#price-max").val().replace("$", '')==realvalues[index])
                {
                    maxValueindex=index;
                    break;
                }
            }

            // set minimum value index
            var minValueindex =0;
            for(index in realvalues)
            {
                if($("#price-min").val().replace("$", '')==realvalues[index])
                {
                    minValueindex=index;
                    break;
                }
            }

            if (parseInt(minVal) > parseInt(maxVal)) 
            {
                $('#price-min').val("$"+maxVal);
            }



            if ($('#price-min').val().replace("$", '') == 0) 
            {
                $("#slider-price").slider("values", 0, 0);
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value = "0";
            }
            else 
            {
                if (parseInt(minVal) > parseInt(maxVal)) 
                {
                    $("#slider-price").slider("values", 0, maxValueindex);
                    document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value = maxVal;
                }
                else
                {
                    $("#slider-price").slider("values", 0, minValueindex);
                    document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin").value = minVal;
                }
            }

            BindData(1);
            setTimeout(function() {
                if ($('#price-min').val().replace("$", '') == 0) 
                {
                    $('#price-min').val("$0");
                }
                else 
                {
                    if (parseInt(minVal) > parseInt(maxVal)) 
                    {
                        $('#price-min').val("$"+ maxVal);
                    }
                    else
                    {
                        $('#price-min').val("$"+ minVal);
                    }
                }
            }, 100);

        }
    });

    $('#price-max').live("keypress", function(e) {
        if (e.keyCode == 13) {
            e.preventDefault();

            var realvalues = [0, 100, 200, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000, 6500, 7000, 7500, 8000, 8500, 9000, 9500, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 55000, 60000, 65000, 70000, 75000, 80000, 85000, 90000, 95000, 100000, 105000, 110000, 115000, 120000, 125000, 130000, 135000, 140000, 145000, 150000, 155000, 160000, 165000, 170000, 175000, 180000, 185000, 190000, 195000, 200000, 205000, 210000, 215000, 220000, 225000, 230000, 235000, 240000, 245000, 250000, 255000, 260000, 265000, 270000, 275000, 280000, 285000, 290000, 295000, 300000, 310000, 320000, 330000, 340000, 350000, 360000, 370000, 380000, 390000, 400000, 450000, 500000, 550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 1000000, 1500000, 2000000, 2500000, 30000000, 3500000, 4000000, 4500000, 5000000, 5500000, 6000000, 6500000, 7000000, 7500000, 8000000, 8500000, 9000000, 9500000, 9999999];

            var maxVal = $("#price-max").val().replace("$", '');
            var minVal = $("#price-min").val().replace("$", '');

            // set maximum value index
            var maxValueindex =132;
            for(index in realvalues)
            {
                if($("#price-max").val().replace("$", '')==realvalues[index])
                {
                    maxValueindex=index;
                    break;
                }
            }

            // set minimum value index
            var minValueindex =0;
            for(index in realvalues)
            {
                if($("#price-min").val().replace("$", '')==realvalues[index])
                {
                    minValueindex=index;
                    break;
                }
            }


            if (parseInt(maxVal) < parseInt(minVal)) 
            {
                $('#price-max').val("$"+minVal);
            }


            if ($('#price-max').val().replace("$", '') == "0") 
            {
                $("#slider-price").slider("values", 1, "9999999");
                document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value = "9999999";
            }
            else 
            {
                if(parseInt(maxVal)< parseInt(minVal))
                {
                    $("#slider-price").slider("values", 1, minValueindex);
                    document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value = minVal;
                }
                else
                {
                    $("#slider-price").slider("values", 1, maxValueindex);
                    document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax").value = maxVal;
                }
            }

            BindData(1);
            setTimeout(function() {
                if ($('#price-max').val().replace("$", '') == "0") 
                {
                    $('#price-max').val("$9999999");
                }
                else 
                {
                    if(parseInt(maxVal)< parseInt(minVal))
                    {
                        $('#price-max').val("$" + minVal);
                    }
                    else
                    {
                        $('#price-max').val("$" + maxVal);
                    }
                }
            }, 100);
        }
    });
    
	
	$(function() {
	    var minvalue=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormin").value;
	    var maxvalue=document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormax").value;
	    $("#slider-step").slider({
	        range: true,
	        min: 0,
	        max: 500,
	        step: 72,
	        values: [minvalue, maxvalue],
	        change: function(event, ui) {
	            document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormin").value = ui.values[0];
	            document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormax").value = ui.values[1];
	            BindData(1);
	        }
	    });
	    $('.Range a:last-child').addClass('handle-2');
	});

	$(function() {
	    var minvalue = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmin").value;
	    var maxvalue = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmax").value;
	    $("#slider-cut").slider({
	        range: true,
	        min: 0,
	        max: 500,
	        step: 100,
	        values: [minvalue, maxvalue],
	        change: function(event, ui) {
	        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmin").value = ui.values[0];
	        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmax").value = ui.values[1];
	        BindData(1);
	        }
	    });
	    $('.Range a:last-child').addClass('handle-2');
	});
		$(function() {
		    var minvalue = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymin").value;
		    var maxvalue = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymax").value;
		    $("#slider-clarity").slider({
		        range: true,
		        min: 0,
		        max: 500,
		        step: 72,
		        values: [minvalue, maxvalue],
		        change: function(event, ui) {
		        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymin").value = ui.values[0];
		        document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymax").value = ui.values[1];
		        BindData(1);
		        }
		    });

		    $('.Range a:last-child').addClass('handle-2');
		});
	
	
	 function BindCompare(val1,val2,val3)
	 {
	       
	       if(parseInt(document.getElementById("hdnCheckcount").value)<5 && document.getElementById(val1).checked==true)
	       {
	          document.getElementById(val1).checked=true;
	          var url = rootPath + "Controls/Default/DiamondSearch/Ajax.aspx?method=compare&CompareId=" + val2 + "&Type=" + val3+ "&UID=" + new Date().getTime();
              xmlHttp = GetXmlHttpObject(stateChanged1);
              xmlHttp.open("GET", url, false);
              xmlHttp.send(null);
           }
           else if(parseInt(document.getElementById("hdnCheckcount").value)<=5 && document.getElementById(val1).checked==false)
	       {
                 document.getElementById(val1).checked=false;
	             var url = rootPath +  "Controls/Default/DiamondSearch/Ajax.aspx?method=compare&CompareId=" + val2 + "&Type=" + val3+ "&UID=" + new Date().getTime();
                 xmlHttp = GetXmlHttpObject(stateChanged1);
                 xmlHttp.open("GET", url, false);
                 xmlHttp.send(null);
           }
           else
           {
              document.getElementById(val1).checked=false;
              alert('You can select maximum 5 diamonds to compare at one time.');
           }
           

	 }
	 function RemoveCompare(val1,val2,val3)
	 {
	       var checkmian='chkmain'+val2+val3;
	       if(document.getElementById(checkmian)!=null)
	       {
	         document.getElementById(checkmian).checked=false;
	       }
	       var url = rootPath +  "Controls/Default/DiamondSearch/Ajax.aspx?method=deletecompare&CompareId=" + val2 + "&Type=" + val3+ "&UID=" + new Date().getTime();
           xmlHttp = GetXmlHttpObject(stateChanged1);
           xmlHttp.open("GET", url, false);
           xmlHttp.send(null);

	 }
	 function stateChanged1() {
           if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
               document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_divCompareResult').innerHTML = xmlHttp.responseText;
               xmlHttp=null;
           }
       }







       function DiamondSearch(objCheck) {
           $('#shapediv ul li').each(function() {
               if ($(this).attr('class') == "active") {
                   if ($(this).context.id != objCheck) {
                       $(this).removeClass();
                   }
               }
           });
           if (document.getElementById(objCheck).className == "") {
               document.getElementById(objCheck).className += "active";
           }
           else {
               document.getElementById(objCheck).className = "";
           }
         
           
	    
	      var selected = new Array();
	      $('#shapediv ul li').each(function() {
	          if ($(this).attr('class') == "active") {
	              selected.push($(this).attr('title'));
	          }
	      });

	      RefreshInternetExplorerData('S');
	      document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = "";
	      document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value = selected;
	      BindData(1);
	      Create_Url();
	  }
	  
	    // Start - on the back button click on next page (This will set the slider values)
    $(document).ready(function(){
        var ringStyle = document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelist").value;
        if(ringStyle!='' && ringStyle != 'Nolist'){
            
            var myArray = ringStyle.split(',');
            for(var i=0;i<myArray.length;i++){
                if (document.getElementById(GetStyleLi(myArray[i].replace("'",""))).className == "") 
                {
                document.getElementById(GetStyleLi(myArray[i].replace("'",""))).className += "active";
                }
                else {
                document.getElementById(GetStyleLi(myArray[i].replace("'",""))).className = "";
                }
            }
        }
    });
    
    function GetStyleLi(val){
        var returnVal='';
        if (val.toLowerCase() == 'round') { returnVal = 'liRound'; }
        if (val.toLowerCase() == 'princess') { returnVal = 'liPrincess'; }
        if (val.toLowerCase() == 'emerald') { returnVal = 'liEmerald'; }
        if (val.toLowerCase() == 'asscher') { returnVal = 'liAsscher'; }
        if (val.toLowerCase() == 'marquise') { returnVal = 'liMarquise'; }
        if (val.toLowerCase() == 'oval') { returnVal = 'liOval'; }
        if (val.toLowerCase() == 'radiant') { returnVal = 'liRadiant'; }
        if (val.toLowerCase() == 'pear') { returnVal = 'liPear'; }
        if (val.toLowerCase() == 'heart') { returnVal = 'liHeart'; }
        if (val.toLowerCase() == 'cushion') { returnVal = 'liCushion'; }
        return returnVal;
    }
    // End - on the back button click on next page (This will set the slider values)






   

	  var numb = "0123456789";
	  function res(t, v) {
	      var w = "";
	      for (i = 0; i < t.value.length; i++) {
	          x = t.value.charAt(i);
	          if (v.indexOf(x, 0) != -1)
	              w += x;


	      }
	      t.value = w;
	  }
	  var numb1 = ".0123456789";

	  function res1(t, v) {
	      var w = "";
	      for (i = 0; i < t.value.length; i++) {
	          x = t.value.charAt(i);
	          if (v.indexOf(x, 0) != -1)
	              w += x;


	      }
	      t.value = w;
	  }

	  function SortBySahpe() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "ASC";
	          document.getElementById("arshape").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "DESC";
	       document.getElementById("arshape").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "ASC";
	      document.getElementById("arshape").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "";
	      document.getElementById("arcarat").className = "";
	      document.getElementById("arcut").className = "";
	      document.getElementById("arcolor").className = "";
	      document.getElementById("arclarity").className = "";
	      document.getElementById("arpolish").className = "";
	      document.getElementById("arprice").className = "";
	      BindData(1);
	  }
	  function SortByCarat() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "ASC";
	          document.getElementById("arcarat").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "DESC";
	      document.getElementById("arcarat").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "ASC";
	      document.getElementById("arcarat").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "";
	      document.getElementById("arshape").className = "";
	      document.getElementById("arcut").className = "";
	      document.getElementById("arcolor").className = "";
	      document.getElementById("arclarity").className = "";
	      document.getElementById("arpolish").className = "";
	      document.getElementById("arprice").className = "";
	      BindData(1);
	  }
	  function SortByCut() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "ASC";
	          document.getElementById("arcut").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "DESC";
	      document.getElementById("arcut").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "ASC";
	      document.getElementById("arcut").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "";
	      document.getElementById("arshape").className = "";
	      document.getElementById("arcarat").className = "";
	      document.getElementById("arcolor").className = "";
	      document.getElementById("arclarity").className = "";
	      document.getElementById("arpolish").className = "";
	      document.getElementById("arprice").className = "";
	      BindData(1);
	  }
	  function SortByColor() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "ASC";
	          document.getElementById("arcolor").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "DESC";
	      document.getElementById("arcolor").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "ASC";
	      document.getElementById("arcolor").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "";
	      document.getElementById("arshape").className = "";
	      document.getElementById("arcarat").className = "";
	      document.getElementById("arcut").className = "";
	      document.getElementById("arclarity").className = "";
	      document.getElementById("arpolish").className = "";
	      document.getElementById("arprice").className = "";
	      BindData(1);
	  }
	  function SortByClarity() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "ASC";
	          document.getElementById("arclarity").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "DESC";
	      document.getElementById("arclarity").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "ASC";
	      document.getElementById("arclarity").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "";
	      document.getElementById("arshape").className = "";
	      document.getElementById("arcarat").className = "";
	      document.getElementById("arcut").className = "";
	      document.getElementById("arcolor").className = "";
	      document.getElementById("arpolish").className = "";
	      document.getElementById("arprice").className = "";
	      BindData(1); 
	  }
	  function SortByPolish() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "ASC";
	          document.getElementById("arpolish").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "DESC";
	      document.getElementById("arpolish").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "ASC";
	      document.getElementById("arpolish").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "";
	      document.getElementById("arshape").className = "";
	      document.getElementById("arcarat").className = "";
	      document.getElementById("arcut").className = "";
	      document.getElementById("arcolor").className = "";
	      document.getElementById("arclarity").className = "";
	      document.getElementById("arprice").className = "";
	      BindData(1);
	  }
	  function SortByPrice() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "ASC";
	          document.getElementById("arprice").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "DESC";
	      document.getElementById("arprice").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value = "ASC";
	      document.getElementById("arprice").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value = "";
	      document.getElementById("arshape").className = "";
	      document.getElementById("arcarat").className = "";
	      document.getElementById("arcut").className = "";
	      document.getElementById("arcolor").className = "";
	      document.getElementById("arclarity").className = "";
	      document.getElementById("arpolish").className = "";
	      BindData(1); 
	  }



	  // For Recommended Diamond

	  function DiamondSearchRecommended(objCheck) {

	      $('#shapedivRecommended ul li').each(function() {
	          if ($(this).attr('class') == "active") {
	              if ($(this).context.id != objCheck) {
	                  $(this).removeClass();
	              }
	          }
	      });
	      
	      if (document.getElementById(objCheck).className == "") {
	          document.getElementById(objCheck).className += "active";
	      }
	      else {
	          document.getElementById(objCheck).className = "";
	      }
	      var selected = new Array();
	      $('#shapedivRecommended ul li').each(function() {
	          if ($(this).attr('class') == "active") {
	              selected.push($(this).attr('title'));
	          }
	      });
	      RefreshInternetExplorerData('R');
	      document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelistRecommended").value = "";
	      document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelistRecommended").value = selected;
	      Create_UrlRecommended();
	      BindDataRecommended(1);
	  }

	  function SortBySahpeRecommended() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "ASC";
	          document.getElementById("arshapeRecommended").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "DESC";
	      document.getElementById("arshapeRecommended").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "ASC";
	      document.getElementById("arshapeRecommended").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "";
	      document.getElementById("arcaratRecommended").className = "";
	      document.getElementById("arcutRecommended").className = "";
	      document.getElementById("arcolorRecommended").className = "";
	      document.getElementById("arclarityRecommended").className = "";
	      document.getElementById("arpolishRecommended").className = "";
	      document.getElementById("arpriceRecommended").className = "";
	       BindDataRecommended(1);
	  }

//	  function SortByCutRecommended() {	  
//	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value == "") {
//	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "ASC";
//	          document.getElementById("arcutRecommended").className = 'active-asc';
//	      }
//	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value == "ASC") {
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "DESC";
//	      document.getElementById("arcutRecommended").className = 'active-desc';
//	      }
//	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value == "DESC") {
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "ASC";
//	      document.getElementById("arcutRecommended").className = 'active-asc';
//	      }
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "";
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "";
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "";
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "";
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "";
//	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "";
//	      document.getElementById("arshapeRecommended").className = "";
//	      document.getElementById("arcaratRecommended").className = "";
//	      document.getElementById("arcolorRecommended").className = "";
//	      document.getElementById("arclarityRecommended").className = "";
//	      document.getElementById("arpolishRecommended").className = "";
//	      document.getElementById("arpriceRecommended").className = "";
//	      BindDataRecommended(1);
//	  }

	  function SortByCaratRecommended() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "ASC";
	          document.getElementById("arcaratRecommended").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "DESC";
	      document.getElementById("arcaratRecommended").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "ASC";
	      document.getElementById("arcaratRecommended").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "";
	      document.getElementById("arshapeRecommended").className = "";
	      document.getElementById("arcutRecommended").className = "";
	      document.getElementById("arcolorRecommended").className = "";
	      document.getElementById("arclarityRecommended").className = "";
	      document.getElementById("arpolishRecommended").className = "";
	      document.getElementById("arpriceRecommended").className = "";
	      BindDataRecommended(1);
	  }

	  function SortByCutRecommended() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "ASC";
	          document.getElementById("arcutRecommended").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "DESC";
	      document.getElementById("arcutRecommended").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "ASC";
	          document.getElementById("arcutRecommended").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "";
	      document.getElementById("arshapeRecommended").className = "";
	      document.getElementById("arcaratRecommended").className = "";
	      document.getElementById("arcolorRecommended").className = "";
	      document.getElementById("arclarityRecommended").className = "";
	      document.getElementById("arpolishRecommended").className = "";
	      document.getElementById("arpriceRecommended").className = "";
          BindDataRecommended(1);
	  }
	  function SortByColorRecommended() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "ASC";
	          document.getElementById("arcolorRecommended").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "DESC";
	      document.getElementById("arcolorRecommended").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "ASC";
	      document.getElementById("arcolorRecommended").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "";
	      document.getElementById("arshapeRecommended").className = "";
	      document.getElementById("arcaratRecommended").className = "";
	      document.getElementById("arcutRecommended").className = "";
	      document.getElementById("arclarityRecommended").className = "";
	      document.getElementById("arpolishRecommended").className = "";
	      document.getElementById("arpriceRecommended").className = "";
	      BindDataRecommended(1);
	  }
	  function SortByClarityRecommended() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "ASC";
	          document.getElementById("arclarityRecommended").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "DESC";
	      document.getElementById("arclarityRecommended").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "ASC";
	      document.getElementById("arclarityRecommended").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "";
	      document.getElementById("arshapeRecommended").className = "";
	      document.getElementById("arcaratRecommended").className = "";
	      document.getElementById("arcutRecommended").className = "";
	      document.getElementById("arcolorRecommended").className = "";
	      document.getElementById("arpolishRecommended").className = "";
	      document.getElementById("arpriceRecommended").className = "";
	      BindDataRecommended(1);
	  }
	  function SortByPolishRecommended() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "ASC";
	          document.getElementById("arpolishRecommended").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "DESC";
	      document.getElementById("arpolishRecommended").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "ASC";
	      document.getElementById("arpolishRecommended").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "";
	      document.getElementById("arshapeRecommended").className = "";
	      document.getElementById("arcaratRecommended").className = "";
	      document.getElementById("arcutRecommended").className = "";
	      document.getElementById("arcolorRecommended").className = "";
	      document.getElementById("arclarityRecommended").className = "";
	      document.getElementById("arpriceRecommended").className = "";
	      BindDataRecommended(1);
	  }
	  function SortByPriceRecommended() {
	      if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value == "") {
	          document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "ASC";
	          document.getElementById("arpriceRecommended").className = 'active-asc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value == "ASC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "DESC";
	      document.getElementById("arpriceRecommended").className = 'active-desc';
	      }
	      else if (document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value == "DESC") {
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value = "ASC";
	      document.getElementById("arpriceRecommended").className = 'active-asc';
	      }
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value = "";
	      document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value = "";
	      document.getElementById("arshapeRecommended").className = "";
	      document.getElementById("arcaratRecommended").className = "";
	      document.getElementById("arcutRecommended").className = "";
	      document.getElementById("arcolorRecommended").className = "";
	      document.getElementById("arclarityRecommended").className = "";
	      document.getElementById("arpolishRecommended").className = "";
	      BindDataRecommended(1);
	  }
	  
	  
	  
	var WaitingDivRecommended = "<div id=\"loader_bgbackRecommended\"></div><div id=\"divLoaderRecommended\" style=\"width:780px; text-align:center;\"><img src=\"" + rootPath + "Themes/Default/Images/loader.gif\" /></div>";
       function BindDataRecommended(pCurrentPageRecommended) 
       {
           document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCurrentPageRecommended").value = pCurrentPageRecommended;
          intCurrentPageRecommended = pCurrentPageRecommended;
          
          
           
//         if ($("#divDiamondResultRecommended").find("loader_bgbackRecommended").length <= 0) {
//               $("#divDiamondResultRecommended").html( WaitingDivRecommended + $("#divDiamondResultRecommended").html());
//             }
             
             
             document.getElementById("loader_bgbackRecommended").style.display="block";
             document.getElementById("divLoaderRecommended").style.display="block";
              $("#loader_bgbackRecommended").show();
              $("#divLoaderRecommended").show();


              //            alert('shapesort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSort').value);
              //            alert('caratsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSort').value);
              //            alert('cutsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSort').value);
              //            alert('colorsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSort').value);
              //            alert('claritysort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySort').value);
              //            alert('polishsort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSort').value);
              //            alert('pricesort=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSort').value);
              //            alert('cutmin=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmin').value);
              //            alert('cutmax=' + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmax').value);


      
           setTimeout(function(){
               var url = rootPath +  "Controls/Default/DiamondSearch/Ajax.aspx?method=searchRecomnded&shapes=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapelistRecommended').value + "&caratmin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmin').value + "&caratmax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratmax').value + "&colormin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormin').value + "&colormax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColormax').value + "&cutmin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmin').value + "&cutmax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutmax').value + "&claritymin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymin').value + "&claritymax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritymax').value + "&pricemin=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemin').value + "&pricemax=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPricemax').value+ "&shapesort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnShapeSortRecommended').value + "&caratsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCaratSortRecommended').value + "&cutsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnCutSortRecommended').value + "&colorsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnColorSortRecommended').value + "&claritysort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnClaritySortRecommended').value + "&polishsort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPolishSortRecommended').value + "&pricesort=" + document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_hdnPriceSortRecommended').value + "&Cuurentpage=" + pCurrentPageRecommended + "&UID=" + new Date().getTime();
               xmlHttp = GetXmlHttpObject(stateChangedRecommended);
               xmlHttp.open("GET", url, false);
               xmlHttp.send(null);
            },100);
           
           
          
       }

      
       function stateChangedRecommended() {
           if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
               //document.getElementById('divDiamondResultRecommended').innerHTML = xmlHttp.responseText;
               
               if(document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCurrentPageRecommended").value=="1"){
                    $("#divDiamondResultRecommended" ).html('');
                    $("#divDiamondResultRecommended" ).append(xmlHttp.responseText);
               }
               else {
                    $("#divDiamondResultRecommended" ).append(xmlHttp.responseText);
               }
               
               xmlHttp=null;
               scrollbarRecommended();
                document.getElementById("spnTotalDiamondRecommended").innerHTML=document.getElementById("hdtotalcountRecommended").value;
                CalcPagesRecommended(eval(document.getElementById("divTotalRecord1Recommended").innerHTML), eval(document.getElementById("divPageSize1Recommended").innerHTML));
                $("#loader_bgbackRecommended").hide();
                $("#divLoaderRecommended").hide();
                
             
              
           }
       } 
       
       
       
        function BindCompareRecommended(val1,val2,val3)
	 {
	       
	       if(parseInt(document.getElementById("hdnCheckcountrecomnded").value)<5 && document.getElementById(val1).checked==true)
	       {
	          document.getElementById(val1).checked=true;
	          var url = rootPath +  "Controls/Default/DiamondSearch/Ajax.aspx?method=comparerecomnded&CompareId=" + val2 + "&Type=" + val3+ "&UID=" + new Date().getTime();
              xmlHttp = GetXmlHttpObject(stateChanged1Recommended);
              xmlHttp.open("GET", url, false);
              xmlHttp.send(null);
           }
           else if(parseInt(document.getElementById("hdnCheckcountrecomnded").value)<=5 && document.getElementById(val1).checked==false)
	       {
                 document.getElementById(val1).checked=false;
	             var url = rootPath +  "Controls/Default/DiamondSearch/Ajax.aspx?method=comparerecomnded&CompareId=" + val2 + "&Type=" + val3+ "&UID=" + new Date().getTime();
                 xmlHttp = GetXmlHttpObject(stateChanged1Recommended);
                 xmlHttp.open("GET", url, false);
                 xmlHttp.send(null);
           }
           else
           {
              document.getElementById(val1).checked=false;
              alert('You can select maximum 5 diamonds to compare at one time.');
           }
           

	 }
	 function RemoveCompareRecommended(val1,val2,val3)
	 {
	       var checkmian='chkmainrecommended'+val2+val3;
	       if(document.getElementById(checkmian)!=null)
	       {
	         document.getElementById(checkmian).checked=false;
	       }
	       var url = rootPath +  "Controls/Default/DiamondSearch/Ajax.aspx?method=deleterecomndedcompare&CompareId=" + val2 + "&Type=" + val3+ "&UID=" + new Date().getTime();
           xmlHttp = GetXmlHttpObject(stateChanged1Recommended);
           xmlHttp.open("GET", url, false);
           xmlHttp.send(null);

	 }
	 function stateChanged1Recommended() {
           if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
               document.getElementById('ctl00_ctl00_MainContent_UxDiamondSearch_divCompareResultRecommended').innerHTML = xmlHttp.responseText;
               xmlHttp=null;
           }
       }
       
       
 function FetchMoreRecommended() {
     $('#divFetchNewDataRecommended').detach();
     $('#divTotalRecord1Recommended').detach();
     $('#divPageSize1Recommended').detach();
     $("#divDiamondResultRecommended").children().each(function(e) {
         // $(this).removeData();
         $(this).removeAttr(jQuery.expando);
     });
     intCurrentPageRecommended++;
     document.getElementById("ctl00_ctl00_MainContent_UxDiamondSearch_hdnCurrentPageRecommended").value=intCurrentPageRecommended;
     // setTimeout(function(){ GetSearchData(intSortOrder, intCurrentPage);},1000);
     return BindDataRecommended(intCurrentPageRecommended);
 }


        //function to show the paging
	  function CalcPagesRecommended(pintTotalRecordRecommended, pintPageSizeRecommended) {
	      var totalPagesRecommended = 1;
	      totalPagesRecommended = pintTotalRecordRecommended / pintPageSizeRecommended;
	      totalPagesRecommended = Math.ceil(totalPagesRecommended);
	      if (totalPagesRecommended > 1) {
	          if (totalPagesRecommended == intCurrentPageRecommended) {
	              // Do not run the function to get the new data
	              $('#divFetchNewDataRecommended').detach();
	          }
	          else {
	              // run the function to get the new data once the scrolling end
	              $('#divFetchNewDataRecommended').appear(function() { FetchMoreRecommended(); });
	          }
	      }
	      else {
	          // Do not run the function to get the new data
	          $('#divFetchNewDataRecommended').detach();
	      }

	  }
	  
