/**
 *  jQuery Very Simple Context Menu Plugin
 *  @requires jQuery v1.3 or 1.4
 *  http://intekhabrizvi.wordpress.com/
 *
 *  Copyright (c)  Intekhab A Rizvi (intekhabrizvi.wordpress.com)
 *  Licensed under GPL licenses:
 *  http://www.gnu.org/licenses/gpl.html
 *
 *  Version: 1.1
 *  Dated : 28-Jan-2010
 *  Version 1.1 : 2-Feb-2010 : Some Code Improvment
 *  Modified for the ModuleActionsMenu
 */

(function($){
    jQuery.fn.vscontext = function(options){
        var defaults = {
            menuBlock: null,
            offsetX : 8,
            offsetY : 8,
            speed : 'slow'
        };
        var options = $.extend(defaults, options);
        var menu_item = options.menuBlock;
        return this.each(function(){
            	$(this).bind("contextmenu",function(e){
				return false;
		});
		
		
		$(options.menuBlock + " li").bind('mouseenter', function(e){ //show sub UL when mouse moves over parent LI
			
		    var $parentli=$(options.menuBlock + " li");
			var parentlioffset=$parentli.offset();
			var x=$(options.menuBlock + " li").width();
			var y=0;
			
			var docrightedge=$(document).scrollLeft()+$(window).width();
            var docbottomedge=$(document).scrollTop()+$(window).height();
			
			var extraH;
			
			if ($(options.menuBlock + " ul li").size() <= 4)
			{
				extraH = 0;
			}
			else if ($(options.menuBlock + " li").size() > 4)
			{
				extraH = 40;
			}
			
			x=(parentlioffset.left+x+$(options.menuBlock).width() > docrightedge)? x-$(options.menuBlock + " li").width()-$(options.menuBlock).width() : x;
			y=(parentlioffset.top+$(options.menuBlock).height() + $(options.menuBlock + " ul").height() > docbottomedge)? y-$(options.menuBlock).height()+$(this).children("ul").height() - extraH : y;
						
		   $(this).children("ul").css({left:x, top:y});
		   
	       $(this).children("ul").show()
         })
         $(options.menuBlock + " li").bind('mouseleave', function(e){ //show sub UL when mouse moves over parent LI
	        $(this).children("ul").hide()
          })
		
		
            	$(this).mousedown(function(e){
                        var offsetX = e.pageX  + options.offsetX;
                        var offsetY = e.pageY + options.offsetY;
						
						var docrightedge=$(document).scrollLeft()+$(window).width()-40
                  		var docbottomedge=$(document).scrollTop()+$(window).height()-40
						
						
                        offsetX=(offsetX+$(options.menuBlock).width() > docrightedge)? docrightedge-$(options.menuBlock).width() : offsetX //if not enough horizontal room to the ridge of the cursor
                        offsetY=(offsetY+$(options.menuBlock).height() > docbottomedge)? docbottomedge-$(options.menuBlock).height() : offsetY
						
						
			if(e.button == "2"){
                            
                            $(menu_item).show();
                            $(menu_item).css('display','block');
                            $(menu_item).css('top',offsetY);
                            $(menu_item).css('left',offsetX);
			}else {
                            $(menu_item).hide();
                        }
		});
                $(menu_item).hover(function(){}, function(){$(menu_item).hide();})
                
        });
    };
})(jQuery);