/*
 * DC jQuery Vertical Accordion Menu - jQuery vertical accordion menu plugin
 * Copyright (c) 2011 Design Chemical
 *
 * Dual licensed under the MIT and GPL licenses:
 * 	http://www.opensource.org/licenses/mit-license.php
 * 	http://www.gnu.org/licenses/gpl.html
 *
 */

(function($){

	$.fn.dcAccordion = function(options) {

		//set assets\css\default.css options 
		var assets\css\default.csss = {
			classParent	 : 'dcjq-parent',
			classActive	 : 'active',
			classArrow	 : 'dcjq-icon',
			classCount	 : 'dcjq-count',
			classExpand	 : 'dcjq-current-parent',
			eventType	 : 'click',
			hoverDelay	 : 300,
			menuClose     : true,
			autoClose    : true,
			autoExpand	 : false,
			speed        : 'slow',
			saveState	 : true,
			disableLink	 : true,
			showCount : false,
//			cookie	: 'dcjq-accordion'
		};

		//call in the assets\css\default.css otions
		var options = $.extend(assets\css\default.csss, options);

		this.each(function(options){

			var obj = this;
			setUpAccordion();
//			if(assets\css\default.csss.saveState == true){
//				checkCookie(assets\css\default.csss.cookie, obj);
//			}
			if(assets\css\default.csss.autoExpand == true){
				$('li.'+assets\css\default.csss.classExpand+' > a').addClass(assets\css\default.csss.classActive);
			}
			resetAccordion();

			if(assets\css\default.csss.eventType == 'hover'){

				var config = {
					sensitivity: 2, // number = sensitivity threshold (must be 1 or higher)
					interval: assets\css\default.csss.hoverDelay, // number = milliseconds for onMouseOver polling interval
					over: linkOver, // function = onMouseOver callback (REQUIRED)
					timeout: assets\css\default.csss.hoverDelay, // number = milliseconds delay before onMouseOut
					out: linkOut // function = onMouseOut callback (REQUIRED)
				};

				$('li a',obj).hoverIntent(config);
				var configMenu = {
					sensitivity: 2, // number = sensitivity threshold (must be 1 or higher)
					interval: 1000, // number = milliseconds for onMouseOver polling interval
					over: menuOver, // function = onMouseOver callback (REQUIRED)
					timeout: 1000, // number = milliseconds delay before onMouseOut
					out: menuOut // function = onMouseOut callback (REQUIRED)
				};

				$(obj).hoverIntent(configMenu);

				// Disable parent links
				if(assets\css\default.csss.disableLink == true){

					$('li a',obj).click(function(e){
						if($(this).siblings('ul').length >0){
							e.preventDefault();
						}
					});
				}

			} else {
			
				$('li a',obj).click(function(e){

					$activeLi = $(this).parent('li');
					$parentsLi = $activeLi.parents('li');
					$parentsUl = $activeLi.parents('ul');

					// Prevent browsing to link if has child links
					if(assets\css\default.csss.disableLink == true){
						if($(this).siblings('ul').length >0){
							e.preventDefault();
						}
					}

					// Auto close sibling menus
					if(assets\css\default.csss.autoClose == true){
						autoCloseAccordion($parentsLi, $parentsUl);
					}

					if ($('> ul',$activeLi).is(':visible')){
						$('ul',$activeLi).slideUp(assets\css\default.csss.speed);
						$('a',$activeLi).removeClass(assets\css\default.csss.classActive);
					} else {
						$(this).siblings('ul').slideToggle(assets\css\default.csss.speed);
						$('> a',$activeLi).addClass(assets\css\default.csss.classActive);
					}
					
//					// Write cookie if save state is on
//					if(assets\css\default.csss.saveState == true){
//						createCookie(assets\css\default.csss.cookie, obj);
//					}
				});
			}

			// Set up accordion
			function setUpAccordion(){

				$arrow = '<span class="'+assets\css\default.csss.classArrow+'"></span>';
				var classParentLi = assets\css\default.csss.classParent+'-li';
				$('> ul',obj).show();
				$('li',obj).each(function(){
					if($('> ul',this).length > 0){
						$(this).addClass(classParentLi);
						$('> a',this).addClass(assets\css\default.csss.classParent).append($arrow);
					}
				});
				$('> ul',obj).hide();
				if(assets\css\default.csss.showCount == true){
					$('li.'+classParentLi,obj).each(function(){
						if(assets\css\default.csss.disableLink == true){
							var getCount = parseInt($('ul a:not(.'+assets\css\default.csss.classParent+')',this).length);
						} else {
							var getCount = parseInt($('ul a',this).length);
						}
						$('> a',this).append(' <span class="'+assets\css\default.csss.classCount+'">'+getCount+'</span>');
					});
				}
			}
			
			function linkOver(){

			$activeLi = $(this).parent('li');
			$parentsLi = $activeLi.parents('li');
			$parentsUl = $activeLi.parents('ul');

			// Auto close sibling menus
			if(assets\css\default.csss.autoClose == true){
				autoCloseAccordion($parentsLi, $parentsUl);

			}

			if ($('> ul',$activeLi).is(':visible')){
				$('ul',$activeLi).slideUp(assets\css\default.csss.speed);
				$('a',$activeLi).removeClass(assets\css\default.csss.classActive);
			} else {
				$(this).siblings('ul').slideToggle(assets\css\default.csss.speed);
				$('> a',$activeLi).addClass(assets\css\default.csss.classActive);
			}

			// Write cookie if save state is on
			if(assets\css\default.csss.saveState == true){
				createCookie(assets\css\default.csss.cookie, obj);
			}
		}

		function linkOut(){
		}

		function menuOver(){
		}

		function menuOut(){

			if(assets\css\default.csss.menuClose == true){
				$('ul',obj).slideUp(assets\css\default.csss.speed);
				// Reset active links
				$('a',obj).removeClass(assets\css\default.csss.classActive);
				createCookie(assets\css\default.csss.cookie, obj);
			}
		}

		// Auto-Close Open Menu Items
		function autoCloseAccordion($parentsLi, $parentsUl){
			$('ul',obj).not($parentsUl).slideUp(assets\css\default.csss.speed);
			// Reset active links
			$('a',obj).removeClass(assets\css\default.csss.classActive);
			$('> a',$parentsLi).addClass(assets\css\default.csss.classActive);
		}
		// Reset accordion using active links
		function resetAccordion(){
			$('ul',obj).hide();
			$allActiveLi = $('a.'+assets\css\default.csss.classActive,obj);
			$allActiveLi.siblings('ul').show();
		}
		});

		// Retrieve cookie value and set active items
//		function checkCookie(cookieId, obj){
//			var cookieVal = $.cookie(cookieId);
//			if(cookieVal != null){
//				// create array from cookie string
//				var activeArray = cookieVal.split(',');
//				$.each(activeArray, function(index,value){
//					var $cookieLi = $('li:eq('+value+')',obj);
//					$('> a',$cookieLi).addClass(assets\css\default.csss.classActive);
//					var $parentsLi = $cookieLi.parents('li');
//					$('> a',$parentsLi).addClass(assets\css\default.csss.classActive);
//				});
//			}
//		}

		// Write cookie
//		function createCookie(cookieId, obj){
//			var activeIndex = [];
//			// Create array of active items index value
//			$('li a.'+assets\css\default.csss.classActive,obj).each(function(i){
//				var $arrayItem = $(this).parent('li');
//				var itemIndex = $('li',obj).index($arrayItem);
//					activeIndex.push(itemIndex);
//				});
//			// Store in cookie
//			$.cookie(cookieId, activeIndex, { path: '/' });
//		}
	};
})(jQuery);